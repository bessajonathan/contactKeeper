namespace ContactKeeperApi.Application.Interfaces
{
    public interface IViewModel<T>
    {
        T Data { get; set; }
    }
}
