using Moq;
using TaskManager.Common.Data;
using Xunit;
using TaskManager.Core.ConnectionContext;
using TaskManager.Data;
using TaskManager.Tests.Unit.Enums;

namespace TaskManager.Tests.Unit
{
    public class ConnectionContextTests
    {
        private readonly IConnectionContext _connectionContext;
        private readonly Mock<IConnectionScopeFactory> _factoryMock;
        private readonly Mock<IEventScopeFactory> _eventScopeFactoryMock;
        private readonly Mock<IDatabaseScope> _databaseScopeMock;
        private readonly Mock<IEventConnectionScope> _eventScopeMock;
        
        public ConnectionContextTests()
        {
            _factoryMock = new Mock<IConnectionScopeFactory>();
            _databaseScopeMock = new Mock<IDatabaseScope>();
            _eventScopeFactoryMock = new Mock<IEventScopeFactory>();
            _eventScopeMock = new Mock<IEventConnectionScope>();
            _factoryMock.Setup(x => x.Create(It.IsAny<bool>())).Returns(_databaseScopeMock.Object);
            _eventScopeFactoryMock.Setup(x => x.Create()).Returns(_eventScopeMock.Object);
            _connectionContext = new ConnectionContext(_factoryMock.Object, _eventScopeFactoryMock.Object);
        }

        [Theory]
        [InlineData(ScopeType.DataBase, true)]
        [InlineData(ScopeType.DataBase, false)]
        [InlineData(ScopeType.Event, false)]
        public void NewConnectionWillBeCreatedIfItIsNull(ScopeType type, bool isInTransactionScope)
        {
           ExecuteScope(type, isInTransactionScope);

            if (type == ScopeType.DataBase)
                 _factoryMock.Verify(x=>x.Create(isInTransactionScope), Times.Once());

            if (type == ScopeType.Event)
                _eventScopeFactoryMock.Verify(x=>x.Create(), Times.Once());
        }

        [Theory]
        [InlineData(ScopeType.DataBase, true)]
        [InlineData(ScopeType.DataBase, false)]
        [InlineData(ScopeType.Event, false)]
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

        [Theory]
        [InlineData(ScopeType.DataBase, true)]
        [InlineData(ScopeType.DataBase, false)]
        [InlineData(ScopeType.Event, false)]
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