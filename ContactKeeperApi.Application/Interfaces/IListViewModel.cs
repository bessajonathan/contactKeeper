using System.Collections.Generic;

namespace ContactKeeperApi.Application.Interfaces
{
    public interface IListViewModel<T>
    {
        IEnumerable<T> Data { get; set; }
        bool HasNext { get; set; }
        int TotalItemCount { get; set; }
    }
}
