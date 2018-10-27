using Moq;
using NUnit.Framework;
using TaskManager.Core.ConnectionContext;
using TaskManager.Data;

namespace TaskManager.Tests.Unit
{
    [TestFixture]
    public class ConnectionContextTests
    {
        private IConnectionContext _connectionContext;
        private Mock<IConnectionScopeFactory> _factoryMock;
        private Mock<IDatabaseScope> _databaseScopeMock;

        [SetUp]
        public void SetUp()
        {
            _factoryMock = new Mock<IConnectionScopeFactory>();
            _databaseScopeMock = new Mock<IDatabaseScope>();
            _factoryMock.Setup(x => x.Create(It.IsAny<bool>())).Returns(_databaseScopeMock.Object);
            _connectionContext = new ConnectionContext(_factoryMock.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void NewConnectionWillBeCreatedIfItIsNull(bool isInTransactionScope)
        {
           ExecuteScope(isInTransactionScope);

            _factoryMock.Verify(x=>x.Create(isInTransactionScope), Times.Once());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void NewConnectionWillBeCreatedIfItWasDisposed(bool isInTransactionScope)
        {
            ExecuteScope(isInTransactionScope);

            _databaseScopeMock.SetupGet(x => x.IsDisposed).Returns(true);

            ExecuteScope(isInTransactionScope);
            _factoryMock.Verify(x => x.Create(isInTransactionScope), Times.Exactly(2));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void NewConnectionWillNotBeCreatedIfItWasNotDisposed(bool isInTransactionScope)
        {
            ExecuteScope(isInTransactionScope);

            _databaseScopeMock.SetupGet(x => x.IsDisposed).Returns(false);

            ExecuteScope(isInTransactionScope);
            _factoryMock.Verify(x => x.Create(isInTransactionScope), Times.Exactly(1));
        }

        private void ExecuteScope(bool isInTransactionScope)
        {
            if (isInTransactionScope)
            {
                _connectionContext.TransactionScope();
            }
            else
            {
                _connectionContext.Scope();
            }
        }
    }
}