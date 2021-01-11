using AutoMapper;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.User.Commands;
using ContactKeeperApi.Domain.Enum;
using System;

namespace ContactKeeperApi.Application.Contact.ViewModel
{
    public class ContactViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public EContactType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserViewModel User { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Contact, ContactViewModel>()
               .ForMember(vm => vm.Id, ent => ent.MapFrom(x => x.Id))
               .ForMember(vm => vm.Number, ent => ent.MapFrom(x => x.Number))
               .ForMember(vm => vm.User, ent => ent.MapFrom(x => x.User))
               .ForMember(vm => vm.Type, ent => ent.MapFrom(x => x.Type))
               .ForMember(vm => vm.CreatedAt, ent => ent.MapFrom(x => x.CreatedAt))
               .ForMember(vm => vm.UpdatedAt, ent => ent.MapFrom(x => x.UpdatedAt));
        }
    }
}
