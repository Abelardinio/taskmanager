using System;
using TaskManager.ServiceBus;
using Xunit;

namespace TaskManager.Tests.Unit.ServiceBus
{
    public class EventExchangeMappingTests
    {
        [Fact]
        public void EventExchangeMappingTest()
        {
            foreach (var e in (EventLookup[]) Enum.GetValues(typeof(EventLookup)))
            {
                var result = EventExchangeMapping.Dictionary[e];
            }
        }
    }
}