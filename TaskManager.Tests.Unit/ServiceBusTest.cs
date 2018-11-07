using System.Threading;
using Moq;
using NUnit.Framework;
using TaskManager.Core;
using TaskManager.Core.Messages;
using TaskManager.Data;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;

namespace TaskManager.Tests.Unit
{
    [TestFixture]
    public class ServiceBusTest
    {
        [Test]
        public void IntegrationTest()
        {
            var mocksettings = new Mock<DataSettings>();
            mocksettings.SetupGet(x => x.UserName).Returns("admin");
            mocksettings.SetupGet(x => x.Password).Returns("Hello@123");
            mocksettings.SetupGet(x => x.VirtualHost).Returns("/");
            mocksettings.SetupGet(x => x.HostName).Returns("localhost");
            mocksettings.SetupGet(x => x.Port).Returns(5672);
            var connfactory = new ServiceBusConnectionFactory(mocksettings.Object);
            connfactory.Create();
            var factory = new ChannelFactory(connfactory);
            factory.Create();
            var taskEventAccessor = new TaskEventAccessor(new ServiceBusClient(new MessageSerializer(), factory));
            //taskEventAccessor.OnStatusUpdated(OnStatusUpdated);

            while (true)
            {
                taskEventAccessor.StatusUpdated(1, TaskStatus.Removed);
                Thread.Sleep(5000);
            }
        }

        public void OnStatusUpdated(ITaskStatusUpdatedMessage message)
        {
            
        }
    }
}