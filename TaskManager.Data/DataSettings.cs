using System;
using System.Configuration;
using TaskManager.ServiceBus;

namespace TaskManager.Data
{
    public class DataSettings : IConnectionSettings
    {
        public virtual string UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["RABBIT_MQ_CONNECTION_USERNAME"];
            }
        }

        public virtual string Password { get
            {
                return ConfigurationManager.AppSettings["RABBIT_MQ_CONNECTION_PASSWORD"];
            }
        }
        public virtual string VirtualHost { get
            {
                return ConfigurationManager.AppSettings["RABBIT_MQ_CONNECTION_VIRTUAL_HOST"];
            }
        }
        public virtual string HostName { get
            {
                return ConfigurationManager.AppSettings["RABBIT_MQ_CONNECTION_HOST_NAME"];
            }
        }
        public virtual int Port {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["RABBIT_MQ_CONNECTION_PORT"]);
            }
        }
    }
}