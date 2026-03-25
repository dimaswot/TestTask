using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrencyUpdater.Models;
using CurrencyUpdater.Options;
using CurrencyUpdater.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CurrencyUpdater.Workers
{
    public class CurrencyUpdaterWorker:BackgroundService
    {
        private readonly ILogger<CurrencyUpdaterWorker> _logger;
        private readonly ICbrXmlParser _xmlParser;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CbrApiOptions _cbrOptions;
        private readonly HttpClient _httpClient;

        public CurrencyUpdaterWorker(
            ILogger<CurrencyUpdaterWorker> logger,
            ICbrXmlParser xmlParser,
            IServiceScopeFactory scopeFactory,
            IOptions<CbrApiOptions> cbrOptions)
        {
            _logger = logger;
            _xmlParser = xmlParser;
            _scopeFactory = scopeFactory;
            _cbrOptions = cbrOptions.Value;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(_cbrOptions.TimeoutSeconds)
            };
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await RunUpdateAsync(cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                // каждую минуту
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                await RunUpdateAsync(cancellationToken);
            }
        }

        private async Task RunUpdateAsync(CancellationToken cancellationToken)
        {
            try
            {
                // вынужденная мера, потому что не поддерживается win-1251
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var xmlContent = await _httpClient.GetStringAsync(_cbrOptions.BaseUrl, cancellationToken);

                var currencies = await _xmlParser.ParseAsync(xmlContent);

                // создаем скоуп для работы БД, создается единожды для каждого запроса
                using var scope = _scopeFactory.CreateScope();
                var updateService = scope.ServiceProvider.GetRequiredService<ICurrencyUpdateService>();

                await updateService.UpdateCurrenciesAsync(currencies);

            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                // логируем ошибку
            }
        }

        public override void Dispose()
        {
            _httpClient.Dispose();
            base.Dispose();
        }
    }
}
