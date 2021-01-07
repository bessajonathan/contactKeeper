using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.User.Commands;
using ContactKeeperApi.Application.User.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace ContactKeeperAPI.Controllers
{
    [Route("v1/user")]
    public class UserController : Controller
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [OpenApiTag("Usuarios")]
        [ProducesResponseType(typeof(IViewModel<UserViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (command is null)
                BadRequest();

            return Ok(await mediator.Send(command));
        }
    }
}
