using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Application.Users.GetUserById;
using PayrollManagement.Application.Users.Login;
using PayrollManagement.Application.Users.Register;

namespace PayrollManagement.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public sealed class UserController : ApiController
    {
        private readonly ISender _mediator;

        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }

        
        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var userQueryResult = await _mediator.Send(new GetUserByIdQuery(userId));

            return userQueryResult.Match(
                userFounded => Ok(userFounded),
                errors => Problem(errors)
                );
        }
        

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([
            FromBody] RegisterUserCommand command,
            CancellationToken cancellationToken)
        {
            var registerUserResult = await _mediator.Send(command, cancellationToken);

            return registerUserResult.Match(
                userCreatedToken => Created(nameof(userCreatedToken), userCreatedToken),
                errors => Problem(errors)
                );
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser(
            [FromBody] LoginUserCommand command,
            CancellationToken cancellationToken)
        {
            var tokenResult = await _mediator.Send(
                command,
                cancellationToken
                );

            return tokenResult.Match(
                userToken => Ok(userToken),
                errors => Problem(errors)
                );
        }
        
        //TODO: Terminate the CRUD...

    }
}
