using PayrollManagement.Users.Application.Abstractions;
using PayrollManagement.BuildingBlocks.Domain.DomainErrors;
using PayrollManagement.Users.Domain.Primitives;
using PayrollManagement.Users.Domain.Users;
using PayrollManagement.Users.Domain.ValueObjects;

namespace PayrollManagement.Users.Application.Users.Register
{
    public sealed class RegisterCommandHandler :
        IRequestHandler<RegisterCommand, ErrorOr<string>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtProvider jwtProvider, 
            IUnitOfWork unitOfWork, 
            IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<string>> Handle(
            RegisterCommand command, 
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

            if (Role.Create(command.Role) is not Role role)
            {

                return Errors.User.RoleWithBadFormat;
            }

            var userId = new UserId(Guid.NewGuid());

            var user = new User(userId, userName, email, password, role);

            await _userRepository.CreateUserAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            string token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
