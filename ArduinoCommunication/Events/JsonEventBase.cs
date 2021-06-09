using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication.Events
{
    public record JsonEventBase<T> : EventBase
    {
        public string Name { get; init; }
        public T Param { get; set; }
    }

    public record JsonEventBase : JsonEventBase<object>;
}
