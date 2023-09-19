﻿using PayrollManagement.BuildingBlocks.Domain.Abstractions;

namespace PayrollManagement.Users.Domain.UserRegistration.Rules
{
    public class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IBusinessRule
    {
        private readonly UserRegistrationStatus _actualRegistrationStatus;

        internal UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(UserRegistrationStatus actualRegistrationStatus)
        {
            _actualRegistrationStatus = actualRegistrationStatus;
        }

        public bool IsBroken() => _actualRegistrationStatus != UserRegistrationStatus.Confirmed;

        public string Message => "User cannot be created when registration is not confirmed";

    }
}