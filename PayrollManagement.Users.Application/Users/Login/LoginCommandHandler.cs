using ErrorOr;
using MediatR;
using PayrollManagement.Users.Application.Abstractions;
using PayrollManagement.Users.Application.Users.Common;
using PayrollManagement.BuildingBlocks.Domain.DomainErrors;
using PayrollManagement.Users.Domain.Users;
using PayrollManagement.Users.Domain.ValueObjects;

namespace PayrollManagement.Users.Application.Users.Login
{
    public sealed class LoginCommandHandler
        : IRequestHandler<LoginCommand, ErrorOr<TokenResponseDto>>
    {

        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<TokenResponseDto>> Handle(
            LoginCommand command, 
            CancellationToken cancellationToken)
        {
            var email = Email.Create(command.Email)!;

            User? user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            if (user is null)
            {
                return Errors.User.UserNotFound;
            }

            string token = _jwtProvider.Generate(user);

            TokenResponseDto response = new(token);

            return response;
        }
    }
}
