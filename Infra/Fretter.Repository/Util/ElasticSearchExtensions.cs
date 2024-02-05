using FastMember;
using Microsoft.Data.SqlClient;
using Nest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Fretter.Repository.Util
{
    public static class ElasticSearchExtensions
    {
        public static SearchDescriptor<object> GetSearchDescriptor(this SearchDescriptor<object> s, string indexName, Dictionary<string, string> parameters,
            object filter, int? size, int? skip, string fieldAggregate, DateInterval? dateInterval)
        {
            if (string.IsNullOrEmpty(fieldAggregate))
                return s.Index(indexName).TotalHitsAsInteger(true).Size(size).Skip(skip)
                        .Query(q => q.Bool(b => b.Must(q.MappingQuery(parameters, filter))));
            if (dateInterval != null)
                return s.Index(indexName).TotalHitsAsInteger(true).Size(size).Skip(skip)
                        .Aggregations(a => a.DateHistogram("item", d => d.Field(fieldAggregate).CalendarInterval(dateInterval.Value)))
                        .Query(q => q.Bool(b => b.Must(q.MappingQuery(parameters, filter))));

            return s.Index(indexName).TotalHitsAsInteger(true).Size(size).Skip(skip)
                        .Aggregations(a => a.Terms("item", t => t.Field(fieldAggregate)))
                        .Query(q => q.Bool(b => b.Must(q.MappingQuery(parameters, filter))));
        }
        public static QueryContainer MappingQuery(this QueryContainerDescriptor<object> queryContainerDescriptor, Dictionary<string, string> parameters, object filter)
        {
            var queryContainer = new QueryContainer();
            parameters.ForEach(f =>
            {
                var item = filter.GetPropertyValue(f.Key);
                if (item != null && !string.IsNullOrEmpty(item.ToString()))
                {
                    if (item.GetType() == typeof(DateTime))
                        queryContainer = queryContainer && +MappingDateTime(queryContainerDescriptor, f,
                            (DateTime)item);
                    else if (f.Value.Contains("Exists_") && item.GetType() == typeof(bool) && (bool)item)
                        queryContainer = queryContainer && +queryContainerDescriptor.Exists(d => d.Field(f.Value.Replace("Exists_", "")));
                    else
                        queryContainer = queryContainer && +queryContainerDescriptor.Match(m => m.Field(f.Value).Query(item.ToString()));
                }
            });

            return queryContainer && +queryContainerDescriptor.MatchAll();
        }

        private static QueryContainer MappingDateTime(QueryContainerDescriptor<object> queryContainerDescriptor, KeyValuePair<string, string> f, DateTime item)
        {
            if (f.Value.Contains(">="))
                return queryContainerDescriptor
                    .DateRange(d => d.Field(f.Value.Replace(">=", ""))
                    .GreaterThanOrEquals(DateMath.Anchored(item)));
            else if (f.Value.Contains(">"))
                return queryContainerDescriptor
                    .DateRange(d => d.Field(f.Value.Replace(">", ""))
                    .GreaterThan(DateMath.Anchored(item)));
            else if (f.Value.Contains("<="))
                return queryContainerDescriptor
                    .DateRange(d => d.Field(f.Value.Replace("<=", ""))
                    .LessThanOrEquals(DateMath.Anchored(item)));
            else if (f.Value.Contains("<"))
                return queryContainerDescriptor
                    .DateRange(d => d.Field(f.Value.Replace("<", ""))
                    .LessThanOrEquals(DateMath.Anchored(item)));
            else
                return null;
        }
    }

}
