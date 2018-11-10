using Moq;
using NUnit.Framework;
using TaskManager.Core.ConnectionContext;
using TaskManager.Data;
using TaskManager.Tests.Unit.Enums;

namespace TaskManager.Tests.Unit
{
    [TestFixture]
    public class ConnectionContextTests
    {
        private IConnectionContext _connectionContext;
        private Mock<IConnectionScopeFactory> _factoryMock;
        private Mock<IEventScopeFactory> _eventScopeFactoryMock;
        private Mock<IDatabaseScope> _databaseScopeMock;
        private Mock<IEventConnectionScope> _eventScopeMock;

        [SetUp]
        public void SetUp()
        {
            _factoryMock = new Mock<IConnectionScopeFactory>();
            _databaseScopeMock = new Mock<IDatabaseScope>();
            _eventScopeFactoryMock = new Mock<IEventScopeFactory>();
            _eventScopeMock = new Mock<IEventConnectionScope>();
            _factoryMock.Setup(x => x.Create(It.IsAny<bool>())).Returns(_databaseScopeMock.Object);
            _eventScopeFactoryMock.Setup(x => x.Create()).Returns(_eventScopeMock.Object);
            _connectionContext = new ConnectionContext(_factoryMock.Object, _eventScopeFactoryMock.Object);
        }

        [TestCase(ScopeType.DataBase, true)]
        [TestCase(ScopeType.DataBase, false)]
        [TestCase(ScopeType.Event, false)]
        public void NewConnectionWillBeCreatedIfItIsNull(ScopeType type, bool isInTransactionScope)
        {
           ExecuteScope(type, isInTransactionScope);

            if (type == ScopeType.DataBase)
                 _factoryMock.Verify(x=>x.Create(isInTransactionScope), Times.Once());

            if (type == ScopeType.Event)
                _eventScopeFactoryMock.Verify(x=>x.Create(), Times.Once());
        }

        [TestCase(ScopeType.DataBase, true)]
        [TestCase(ScopeType.DataBase, false)]
        [TestCase(ScopeType.Event, false)]
        public void NewConnectionWillBeCreatedIfItWasDisposed(ScopeType type, bool isInTransactionScope)
        {
            ExecuteScope(type, isInTransactionScope);

            _databaseScopeMock.SetupGet(x => x.IsDisposed).Returns(true);
            _eventScopeMock.SetupGet(x => x.IsDisposed).Returns(true);

            ExecuteScope(type, isInTransactionScope);

            if (type == ScopeType.DataBase)
                _factoryMock.Verify(x => x.Create(isInTransactionScope), Times.Exactly(2));

            if (type == ScopeType.Event)
                _eventScopeFactoryMock.Verify(x => x.Create(), Times.Exactly(2));
        }

        [TestCase(ScopeType.DataBase, true)]
        [TestCase(ScopeType.DataBase, false)]
        [TestCase(ScopeType.Event, false)]
        public void NewConnectionWillNotBeCreatedIfItWasNotDisposed(ScopeType type, bool isInTransactionScope)
        {
            ExecuteScope(type, isInTransactionScope);

            _databaseScopeMock.SetupGet(x => x.IsDisposed).Returns(false);
            _eventScopeMock.SetupGet(x => x.IsDisposed).Returns(false);

            ExecuteScope(type, isInTransactionScope);

            if (type == ScopeType.DataBase)
                _factoryMock.Verify(x => x.Create(isInTransactionScope), Times.Exactly(1));
            if (type == ScopeType.Event)
                _eventScopeFactoryMock.Verify(x => x.Create(), Times.Exactly(1));
        }

        private void ExecuteScope(ScopeType type, bool isInTransactionScope)
        {
            if (type == ScopeType.Event)
            {
                _connectionContext.EventScope();
                return;
            }

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