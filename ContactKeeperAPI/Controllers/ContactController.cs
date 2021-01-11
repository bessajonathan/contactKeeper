using ContactKeeperApi.Application.Commands.Contact.Create;
using ContactKeeperApi.Application.Contact.Commands.Delete;
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
    [Authorize("Bearer")]
    [Route("v1/contacts")]
    public class ContactController : BaseController
    {
        /// <summary>
        /// Cria novo contato
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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

            return Created("",await Mediator.Send(command));
        }

        /// <summary>
        /// Lista todos os contatos do usuário logado
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
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

        /// <summary>
        /// Busca o contato pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType(typeof(IViewModel<ContactViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetContactQuery(id,UserId)));
        }

        /// <summary>
        /// Remove o contato pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteContactCommand(id,UserId));

            return NoContent();
        }

        /// <summary>
        /// Atualiza o contato 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [OpenApiTag("Contatos")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactCommand command)
        {
            if (command is null)
                return BadRequest();

            command.UserId = UserId;
            command.Id = id;

            return Ok(await Mediator.Send(command));
        }
    }
}
