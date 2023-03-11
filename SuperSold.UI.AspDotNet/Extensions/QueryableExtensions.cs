using Microsoft.EntityFrameworkCore;

namespace SuperSold.UI.AspDotNet.Extensions;

public static class QueryableExtensions {

    /// <summary>
    /// Used to enumerate the queryable into a list.
    /// <br/>Checks if the <see cref="IQueryable{T}"/> implements <see cref="IAsyncEnumerable{T}"/>. 
    /// If it doesn't, it fall-back to the synchronous <see cref="IQueryable{T}.ToList()"/> with a <see cref="Task.FromResult{TResult}(TResult)"/> wrapper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public static Task<List<T>> ToListAsyncSafe<T>(this IQueryable<T> queryable) {

        ArgumentNullException.ThrowIfNull(queryable);

        if(queryable is not IAsyncEnumerable<T>) {
            return Task.FromResult(queryable.ToList());
        }

        return queryable.ToListAsync();

    }

    /// <summary>
    /// Shorthand to skip ((<paramref name="page"/> ?? 0) * <paramref name="pageLength"/>) elements and take up to the remaining <paramref name="pageLength"/> elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="page"></param>
    /// <param name="pageLength"></param>
    /// <returns></returns>
    public static IQueryable<T> SkipToPage<T>(this IQueryable<T> queryable, int? page, int pageLength) => queryable.Skip((page ?? 0) * pageLength).Take(pageLength);

}