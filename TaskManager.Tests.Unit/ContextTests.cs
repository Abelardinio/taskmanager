using FluentAssertions;
using NUnit.Framework;
using TaskManager.DbConnection;

namespace TaskManager.Tests.Unit
{
    [TestFixture]
    public class ContextTests
    {
        [Test]
        public void ContextIsDisposedProperyTest()
        {
            var context = new Context(false);
            context.IsDisposed.Should().BeFalse();
            context.Dispose();
            context.IsDisposed.Should().BeTrue();
        }
    }
}