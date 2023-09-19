using PayrollManagement.Users.Application.Users.Common;

namespace PayrollManagement.Users.Application.Users.GetById
{
    public sealed class GetUserByIdQueryHandler
        : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponseDto>>
    {
        public Task<ErrorOr<UserResponseDto>> Handle(
            GetUserByIdQuery request, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
