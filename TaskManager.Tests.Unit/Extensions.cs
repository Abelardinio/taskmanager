using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TaskManager.Tests.Unit
{
    public class Extensions
    {
        public static Mock<DbSet<TEntity>> GetDbSetMock<TEntity>(IQueryable<TEntity> entities) where TEntity : class
        {
            var mockSetUsers = new Mock<DbSet<TEntity>>();

            mockSetUsers.As<IAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(entities.GetEnumerator()));

            mockSetUsers.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TEntity>(entities.Provider));

            mockSetUsers.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSetUsers.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSetUsers.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());

            return mockSetUsers;
        }
    }
}