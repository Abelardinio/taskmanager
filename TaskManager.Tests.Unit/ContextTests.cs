using FluentAssertions;
using Xunit;
using TaskManager.DbConnection;

namespace TaskManager.Tests.Unit
{
    public class ContextTests
    {
        [Fact]
        public void ContextIsDisposedPropertyTest()
        {
            var context = new Context(string.Empty, false);
            context.IsDisposed.Should().BeFalse();
            context.Dispose();
            context.IsDisposed.Should().BeTrue();
        }
    }
}