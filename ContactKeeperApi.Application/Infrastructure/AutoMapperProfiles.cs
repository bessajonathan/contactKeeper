using AutoMapper;
using ContactKeeperApi.Application.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace ContactKeeperApi.Application.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();

            var maps = (
                   from type in types
                   from instance in type.GetInterfaces()
                   where
                       typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                       !type.IsAbstract &&
                       !type.IsInterface
                   select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            foreach (var map in maps)
            {
                map.CreateMappings(this);
            }
        }
    }
}
