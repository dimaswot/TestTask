using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using UserService.Application.Features.Auth.Register;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using FluentValidation.TestHelper;

namespace UserService.Tests.Unit.Features.Auth.Register;

public class RegisterUserValidatorTests
{
    private readonly RegisterUserValidator _validator;

    public RegisterUserValidatorTests()
    {
        _validator = new RegisterUserValidator();
    }

    [Fact]
    public void Validate_ValidCommand_NoErrors()
    {
        var command = new RegisterUserCommand("testuser", "Password123");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("", "Password123")]
    public void Validate_ShortName_HasError(string name, string password)
    {
        var command = new RegisterUserCommand(name, password);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [InlineData("testuser", "")]
    [InlineData("testuser", "qw123")]
    public void Validate_InvalidPassword_HasError(string name, string password)
    {
        var command = new RegisterUserCommand(name, password);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}