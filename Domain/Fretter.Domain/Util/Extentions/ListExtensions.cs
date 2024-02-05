using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class ListExtensions
    {
        public static IList<T> ShuffleList<T>(this IEnumerable<T> list) => list.ToList().ShuffleList();

        public static IList<T> ShuffleList<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> callback)
        {
            foreach (var item in enumerable) callback(item);
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> callback)
        {
            foreach (var item in enumerable)
                await callback(item);
        }
    }
}
