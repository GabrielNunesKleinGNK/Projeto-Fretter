using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class QueryFilter
    {
        public QueryFilter()
        {
            this.Filters = new List<FilterContainer>();
        }
        public class FilterContainer
        {
            public string Property { get; set; }
            public object Value { get; set; }
            public bool TypeFilterAnd { get; set; }
        }
        public class SorterContainer
        {
            public string Property { get; set; }
            public string Direction { get; set; }
        }

        public List<FilterContainer> Filters { get; set; }
        public List<SorterContainer> Sorts { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }

        public void AddFilter(string property, object value, bool typeFilterAnd = true)
        {
            if (Filters == null)
                Filters = new List<FilterContainer>();

            Filters.Add(new FilterContainer()
            {
                Property = property,
                Value = value,
                TypeFilterAnd = typeFilterAnd
            });
        }
    }
}
