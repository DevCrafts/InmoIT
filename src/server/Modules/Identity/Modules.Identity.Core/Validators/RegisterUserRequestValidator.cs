﻿// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterUserRequestValidator.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using FluentValidation;
using InmoIT.Modules.Identity.Core.Abstractions;
using InmoIT.Shared.Dtos.Identity.Users;
using Microsoft.Extensions.Localization;

namespace InmoIT.Modules.Identity.Core.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterUserRequestValidator(IIdentityService identityService, IStringLocalizer<RegisterUserRequestValidator> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .EmailAddress().WithMessage(localizer["{PropertyName} must be a valid email accounts."])
                .MustAsync(async (email, _) => !await identityService.ExistsWithEmailAsync(email))
                    .WithMessage((_, email) => string.Format(localizer["Email {0} is already registered."], email));

            RuleFor(x => x.EmailConfirmed)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."]);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MinimumLength(8)
                .MustAsync(async (name, _) => !await identityService.ExistsWithNameAsync(name))
                    .WithMessage((_, name) => string.Format(localizer["Username {0} is already taken."], name));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .Length(8, 16).WithMessage(localizer["{PropertyName} must have between 8 and  16 characters."])
                .Must(IsOnlyNumber).WithMessage(localizer["{PropertyName} should be all numbers."])
                .MustAsync(async (phone, _) => !await identityService.ExistsWithPhoneNumberAsync(phone!))
                    .WithMessage((_, phone) => string.Format(localizer["Phone number {0} is already registered."], phone))
                    .Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));

            RuleFor(x => x.PhoneNumberConfirmed)
               .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."]);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .Length(10, 100).WithMessage(localizer["{PropertyName} must have between 10 and 100 characters."])
                .Must(IsOnlyLetter).WithMessage(localizer["{PropertyName} should be all letters."])
                .Equal(x => x.LastName).WithMessage(localizer["{PropertyName} cannot be equal to LastName."]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .Length(10, 100).WithMessage(localizer["{PropertyName} must have between 10 and 100 characters."])
                .Must(IsOnlyLetter).WithMessage(localizer["{PropertyName} should be all letters."])
                .Equal(x => x.FirstName).WithMessage(localizer["{PropertyName} cannot be equal to FirstName."]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .NotEqual(x => x.Password).WithMessage(localizer["{PropertyName} do not match."]);
        }

        private bool IsOnlyLetter(string value) => value.All(char.IsLetter);

        private bool IsOnlyNumber(string value) => value.All(char.IsNumber);
    }
}