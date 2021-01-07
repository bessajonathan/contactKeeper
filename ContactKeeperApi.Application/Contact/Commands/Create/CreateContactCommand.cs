using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using MediatR;
using Newtonsoft.Json;
using NSwag.Annotations;

namespace ContactKeeperApi.Application.Commands.Contact.Create
{
    public class CreateContactCommand : IRequest<IViewModel<ContactViewModel>>
    {
        public string Number { get; set; }

        [JsonIgnore]
        [OpenApiIgnore]
        public int UserId { get; set; }
    }
}
