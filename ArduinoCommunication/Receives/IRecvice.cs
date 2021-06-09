using ArduinoCommunication.EventDispatchers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication.Receives
{
    public interface IReceive
    {
        public bool IsPrecessing { get; }
        public Stream InputStream { get; set; }
        public IEventDispatcher EventDispatcher { get; set; }

        public void SetupInputStream(Stream inputStream);
        public void SetupEventDispatcher(IEventDispatcher eventDispatcher);

        public void Begin();
        public void Stop();
    }
}
