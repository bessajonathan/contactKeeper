using ContactKeeperApi.Application.Interfaces;

namespace ContactKeeperApi.Application.ViewModels
{
    public class ViewModel<T> : IViewModel<T>
    {
        public T Data { get; set; }
    }
}
