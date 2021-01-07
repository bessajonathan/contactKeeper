using AutoMapper;

namespace ContactKeeperApi.Application.Interfaces
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
