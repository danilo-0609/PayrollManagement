namespace PayrollManagement.Application.Users.Login
{
    public record LoginCommandHandler :
        IRequestHandler<LoginCommand, ErrorOr<string>>
    {
        public Task<ErrorOr<string>> Handle(
            LoginCommand request, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //Implement the actual logic...
        }
    }
}
