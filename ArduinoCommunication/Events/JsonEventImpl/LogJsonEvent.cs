using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication.Events.JsonEventImpl
{
    public record LogJsonEvent : JsonEventBase
    {
        public static LogJsonEvent Cast(JsonEventBase o) => o as LogJsonEvent;
    }
}
