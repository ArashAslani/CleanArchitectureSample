using MediatR;
using Microsoft.AspNetCore.Mvc;
using UM.Application.UserApplication.Register;

namespace UM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
