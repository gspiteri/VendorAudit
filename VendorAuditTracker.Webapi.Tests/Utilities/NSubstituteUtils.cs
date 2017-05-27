using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;

namespace VendorAuditTracker.Webapi.Tests.Utilities
{
    public static class NSubstituteUtils
    {
        public static DbSet<T> CreateMockDbSet<T>(IEnumerable<T> data = null, string tableToInclude = "Projects")
            where T : class
        {
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>, IDbAsyncEnumerable<T>>();

            if (data == null) return mockSet;

            var queryable = data.AsQueryable();

            // setup all IQueryable and IDbAsyncEnumerable methods using what you have from "data"
            // the setup below is a bit different from the test above
            ((IDbAsyncEnumerable<T>)mockSet).GetAsyncEnumerator()
                .Returns(new TestDbAsyncEnumerator<T>(queryable.GetEnumerator()));
            ((IQueryable<T>)mockSet).Provider.Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
            ((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(queryable.ElementType);
            ((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());

            // The following line bypasses the Include call.
            ((IQueryable<T>)mockSet).Include(tableToInclude).Returns(mockSet);

            ((IQueryable<T>)mockSet).AsNoTracking().Returns(mockSet);

            return mockSet;
        }

        public static DbSet<T> CreateMockDbSetWithIncludes<T>(IEnumerable<T> data = null, List<string> propertiesToInclude = null)
           where T : class
        {
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>, IDbAsyncEnumerable<T>>();

            if (data == null) return mockSet;
            var queryable = data.AsQueryable();

            // setup all IQueryable and IDbAsyncEnumerable methods using what you have from "data"
            // the setup below is a bit different from the test above
            ((IDbAsyncEnumerable<T>)mockSet).GetAsyncEnumerator()
                .Returns(new TestDbAsyncEnumerator<T>(queryable.GetEnumerator()));
            ((IQueryable<T>)mockSet).Provider.Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
            ((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(queryable.ElementType);
            ((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());

            // The following line bypasses the Include call.
            if (propertiesToInclude != null)
            {
                foreach (var item in propertiesToInclude)
                {
                    ((IQueryable<T>)mockSet).Include(item).Returns(mockSet);
                }
            }
            ((IQueryable<T>)mockSet).AsNoTracking().Returns(mockSet);

            return mockSet;
        }

        public static void IgnoreAwaitForNSubstituteAssertion(this Task task)
        {

        }
    }
}
