using PayrollManagement.Application.Users.Common;
using PayrollManagement.Domain.DomainErrors;
using PayrollManagement.Domain.Users;

namespace PayrollManagement.Application.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserResponseDto>> Handle(
            GetUserByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var id = new UserId(request.Id);

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return Errors.User.UserNotFound;
            }

            return new UserResponseDto(user.UserName, user.Email);
        }
    }
}
