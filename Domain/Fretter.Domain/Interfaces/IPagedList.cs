using System.Collections.Generic;

namespace Fretter.Domain.Interfaces
{
    public interface IPagedList<T>
    {
        int Total { get; }
        IEnumerable<T> Data { get; }
    }
}
