using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication.EventDispatchers
{
    public interface IEventDispatcher
    {
        /// <summary>
        /// 收到消息
        /// </summary>
        public event EventHandler OnMessageRecviced;

        public void FireEvent(object message);
    }
}
