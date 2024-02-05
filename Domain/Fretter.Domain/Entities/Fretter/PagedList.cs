using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Interfaces;

namespace Fretter.Domain.Entities
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(IEnumerable<T> items)
            : this(items, items.Count())
        {
        }

        public PagedList(IEnumerable<T> items, int total)
        {
            this.Total = total;
            this.Data = items;
        }

        public int Total { get; }
        public IEnumerable<T> Data { get; }
    }
}
