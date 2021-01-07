using AutoMapper;
using ContactKeeperApi.Application.Interfaces;
using System;

namespace ContactKeeperApi.Application.User.Commands
{
    public class UserViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.User, UserViewModel>()
                .ForMember(vm => vm.Id, ent => ent.MapFrom(x => x.Id))
                .ForMember(vm => vm.UserName, ent => ent.MapFrom(x => x.UserName))
                .ForMember(vm => vm.Email, ent => ent.MapFrom(x => x.Email))
                .ForMember(vm => vm.CreatedAt, ent => ent.MapFrom(x => x.CreatedAt))
                .ForMember(vm => vm.UpdatedAt, ent => ent.MapFrom(x => x.UpdatedAt));
        }
    }
}
