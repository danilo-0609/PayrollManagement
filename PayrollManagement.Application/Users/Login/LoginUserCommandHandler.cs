using PayrollManagement.Domain.Users;
using PayrollManagement.Domain.ValueObjects;
using PayrollManagement.Domain.DomainErrors;
using PayrollManagement.Application.Abstractions;
using PayrollManagement.Domain.Primitives;

namespace PayrollManagement.Application.Users.Login
{
    public record LoginUserCommandHandler :
        IRequestHandler<LoginUserCommand, ErrorOr<string>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        }

        public async Task<ErrorOr<string>> Handle(
            LoginUserCommand request, 
            CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email)!;
            
            User? user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            if (user is null)
            {
                return Errors.User.UserNotFound;
            }
 
            string token = _jwtProvider.Generate(user);
    
            return token;
        }
    }
}

