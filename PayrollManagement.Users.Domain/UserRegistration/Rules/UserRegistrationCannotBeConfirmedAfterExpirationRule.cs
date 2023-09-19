﻿using PayrollManagement.BuildingBlocks.Domain.Abstractions;

namespace PayrollManagement.Users.Domain.UserRegistration.Rules
{
    public class UserRegistrationCannotBeConfirmedAfterExpirationRule : IBusinessRule
    {
        private readonly UserRegistrationStatus _actualRegistrationStatus;

        internal UserRegistrationCannotBeConfirmedAfterExpirationRule(UserRegistrationStatus actualRegistrationStatus)
        {
            _actualRegistrationStatus = actualRegistrationStatus;
        }

        public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

        public string Message => "User Registration cannot be confirmed because it is expired";
    }
}