using ContactKeeperApi.Application.Commands.Contact.Create;
using ContactKeeperApi.Application.Contact.Commands.Update;
using ContactKeeperApi.Application.Contact.Queries.GetContact;
using ContactKeeperApi.Application.Contact.Queries.GetContacts;
using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace ContactKeeperAPI.Controllers
{
    [Route("v1/contact")]
    public class ContactController : BaseController
    {
        [HttpPost]
        [Authorize("Bearer")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType(typeof(IViewModel<ContactViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateContactCommand command)
        {
            if (command is null)
                BadRequest();

            command.UserId = UserId;

            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize("Bearer")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType(typeof(IListViewModel<ContactViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IListViewModel<ContactViewModel>>> GetAll([FromQuery] GetContactsQuery query)
        {
            if (query is null)
                BadRequest();

            query.UserId = UserId;

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType(typeof(IViewModel<ContactViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetContactQuery
            {
                UserId = UserId,
                Id = id
            };

            return Ok(await Mediator.Send(query));
        }

        [HttpPut("{id}")]
        [Authorize("Bearer")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactCommand command)
        {
            if (command is null)
                return BadRequest();
            command.UserId = UserId;

            return Ok(await Mediator.Send(command));
        }
    }
}
