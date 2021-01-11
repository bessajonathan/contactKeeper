using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.User.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace ContactKeeperAPI.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Cria novo usuario
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [OpenApiTag("Usuarios")]
        [ProducesResponseType(typeof(IViewModel<TokenViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (command is null)
                BadRequest();

            return Created(string.Empty,await mediator.Send(command));
        }
    }
}
