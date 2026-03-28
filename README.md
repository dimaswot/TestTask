База PG:
Таблица User
- Id
- Name
- PasswordHach

Таблица Currency:
- Id
- Name
- Rate

Таблица UserFavorite
- UserId
- CurrencyName
______________________________________________________________

MigrationService -сервис миграции .Подготовка базы данных перед запуском основных микросервисов. Подключается к БД, проверяет наличией миграций, применяет миграции

CurrencyService -  фоновый сервис, который обращается по адресу http://www.cbr.ru/scripts/XML_daily.asp и полученными данными заполняет таблицу Сurrency. Выполняется при запуске проекта и каждую минуту после запуска. Парсит получаемую xmlку из cbr.ru, с сохраняется в БД.

UserService - микросервис для управления пользователем, регистрации, авторизации и аутентификации.
Реализован через Clean Architecture и CQRS 

UserService и FinanceService:
- API - HTTP API
- Application - Бизнесовая логика (CQRS command/request, validation) 
- Domain - сущности и интеофейсы
- Infrastructure - Реализация (EF core, крипта, rep)
- Test.Unit - юнит тесты

User Api:
- /api/auth/register - регистрация юзера
- /api/auth/login - авторизация (получаем токен)
- /api/auth/logout - разлогиниться
- /api/finance/rates - получить валюты юзера (GET, добавлен для наглядности)

Finance Api:
- /api/Currency/rates - получить избранные валюты авторизованного пользователя
- /api/Currency/all - получить все валюты
- /api/Favorites - получить валюты юъера
- /api/Favorites/{currencyName}(ADD) - добавить избранную валюту юзеру
- /api/Favorites/{currencyName}(DELETE) - удалить избранную валюту юзеру

Написан на .NET 8
БД: PGSQL
Аутентификация: JWT
CQRS: Mediatr
хеширование: BCrypr

