using AutoMapper;
using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.User.Commands;
using ContactKeeperApi.Domain.Entities;

namespace ContactKeeperApi.Infra.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<User, UserViewModel>();
            CreateMap<Contact, ContactViewModel>();
        }
    }
}
