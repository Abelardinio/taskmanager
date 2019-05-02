using System.Collections.Generic;

namespace TaskManager.ServiceBus
{
    public static class EventExchangeMapping
    {
        public static IDictionary<EventLookup, ExchangeLookup> Dictionary = new Dictionary<EventLookup, ExchangeLookup>
        {
            {EventLookup.TaskStatusUpdated, ExchangeLookup.Task },
            {EventLookup.PermissionsUpdated, ExchangeLookup.Permissions }
        };
    }
}