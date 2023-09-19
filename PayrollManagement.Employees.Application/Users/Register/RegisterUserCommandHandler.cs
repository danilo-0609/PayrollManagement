using PayrollManagement.Application.Abstractions;
using PayrollManagement.Domain.DomainErrors;
using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.Users;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Application.Users.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<string>> Handle(
            RegisterUserCommand command, 
            CancellationToken cancellationToken)
        {
            var email = Email.Create(command.Email)!;

            User? userFound = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            if (userFound is not null)
            {
                return Errors.User.UserRegisteredAlready;
            }

            if (UserName.Create(command.UserName) is not UserName userName)
            {
                return Errors.User.UsernameWithBadFormat;
            }
        
            if (Password.Create(command.Password) is not Password password)
            {
                return Errors.User.PasswordWithBadFormat;
            }

            var role = Role.User;

            if (command.Role == "Admin")
            {
                 role = Role.Admin;
            }

            UserId userId = new(Guid.NewGuid());

            var user = new User(userId,userName, email, password, role);

            _userRepository.CreateUserAsync(user);
            await _unitOfWork.SaveChangesAsync();

            string token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
