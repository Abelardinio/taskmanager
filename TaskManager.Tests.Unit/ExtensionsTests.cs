using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TaskManager.Core;

namespace TaskManager.Tests.Unit
{
    [TestFixture]
    public class ExtensionsTests
    {
        private readonly IQueryable<int> _collection = Enumerable.Range(1, 95).AsQueryable();
        private readonly Mock<IFilter<int>> _filterMock = new Mock<IFilter<int>>();

        [SetUp]
        public void SetUp()
        {
            _filterMock.Setup(x => x.Sort(_collection)).Returns(_collection);
            _filterMock.Setup(x => x.Filter(_collection)).Returns(_collection);
        }

        [TestCase(1, 20, 5, 1, 20)]
        [TestCase(2, 50, 2, 51, 44)]
        [TestCase(2, 100, 1, 0, 0)]
        public async Task GetPagedResultTest(int pageNumber, int pageSize, int pagesCount, int rangeFrom, int rangeCount)
        {
            _filterMock.SetupGet(x => x.PageNumber).Returns(pageNumber);
            _filterMock.SetupGet(x => x.PageSize).Returns(pageSize);

            var result = await _collection.GetPagedResultAsync(_filterMock.Object);
            result.PagesCount.Should().Be(pagesCount);
            if (!result.Items.Any() && rangeCount == 0) return;
            result.Items.Should().Contain(Enumerable.Range(rangeFrom, rangeCount));
        }
    }
}