using System;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace WpfKolekcjaZdjec.Business
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Generate next id for workaround SQL CE and EF 4 bug.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="table">The table.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>ID value.</returns>
        public static TResult NextId<TSource, TResult>(this ObjectSet<TSource> table, Expression<Func<TSource, TResult>> selector) where TSource : class
        {
            TResult lastId = table.Any() ? table.Max(selector) : default(TResult);

            if (lastId is int)
            {
                lastId = (TResult)(object)(((int)(object)lastId) + 1);
            }
            return lastId;
        }
    }
}