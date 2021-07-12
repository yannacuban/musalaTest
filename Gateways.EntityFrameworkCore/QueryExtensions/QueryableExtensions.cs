using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GateWays.Common.Pagination;
using Microsoft.EntityFrameworkCore.Query;

namespace Gateways.EntityFrameworkCore.QueryExtensions
{
    public static class QueryableExtensions
    {
        public static (IQueryable<T> pagedQuery, Func<int> countTotal) QueryPage<T>(
          this IQueryable<T> baseQuery,
          PaginationParams pagination,
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
        ) where T : class
        {
            if (pagination.PageNumber < 0)
                throw new ArgumentNullException(nameof(pagination.PageNumber));

            if (pagination.PageSize < 0)
                throw new ArgumentNullException(nameof(pagination.PageSize));

            var filteredAndIncludedQuery = FilterAndInclude(baseQuery, filter, includes);

            var pagedFilteredAndIncludedQuery =
              Paginate(filteredAndIncludedQuery, pagination.PageSize, (pagination.PageNumber - 1) * pagination.PageSize);

            int countTotal() => filteredAndIncludedQuery.Count();

            return (pagedQuery: pagedFilteredAndIncludedQuery, countTotal);
        }

        public static (IQueryable<ProjectT> pagedQuery, Func<int> countTotal) QueryPage<T, ProjectT>(
          this IQueryable<T> baseQuery,
          PaginationParams pagination,
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
          Expression<Func<T, ProjectT>> project = null
        ) where T : class
        {
            var (pagedQuery, countTotal) =
            QueryPage(baseQuery, pagination, filter, includes);

            return (pagedQuery.Select(project), countTotal);
        }

        public static (IQueryable<ProjectT> pagedQuery, Func<int> countTotal) QueryPage<T, ProjectT>(
          this IQueryable<T> baseQuery,
          PaginationParams pagination,
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
          Expression<Func<T, ProjectT>> project = null,
          params KeyValuePair<Expression<Func<T, object>>, ESortOrder>[] orderBys
        ) where T : class
        {
            var sortedQuery = Sort(baseQuery, orderBys);

            var (sortedPagedFilteredAndIncludedQuery, countTotal) =
            QueryPage(sortedQuery, pagination, filter, includes);

            return (pagedQuery: sortedPagedFilteredAndIncludedQuery.Select(project), countTotal);
        }

        private static IQueryable<T> Paginate<T>(IQueryable<T> baseQuery, int size, int skipCount) =>
          skipCount == 0 ?
          baseQuery.Take(size) :
          baseQuery.Skip(skipCount).Take(size);

        private static IQueryable<T> FilterAndInclude<T>(
          IQueryable<T> baseQuery,
          Expression<Func<T, bool>> filter,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) where T : class
        {
            var queryIncluded = Include(baseQuery, includes);

            return filter != null ?
              queryIncluded.Where(filter) :
              queryIncluded;
        }

        private static IQueryable<T> Sort<T>(
          IQueryable<T> baseQuery,
          KeyValuePair<Expression<Func<T, object>>, ESortOrder>[] orderBys)
        {
            if (orderBys != null && orderBys.Length > 0)
            {
                foreach (var ordering in orderBys)
                {
                    baseQuery = ordering.Value == ESortOrder.Ascending ?
                      baseQuery.OrderBy(ordering.Key) :
                      baseQuery.OrderByDescending(ordering.Key);
                }
            }

            return baseQuery;
        }

        private static IQueryable<T> Include<T>(
            IQueryable<T> baseQuery,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) =>
          includes?.Invoke(baseQuery) ?? baseQuery;
    }
}
