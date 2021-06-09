using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication.Events.JsonEventImpl
{
    public record ChangeButtonKeyboardValueParam
    {
        public string TargetButton { get; set; }
        public uint Keycode { get; set; }
    }

    public record ChangeButtonKeyboardValueEvent : JsonEventBase<ChangeButtonKeyboardValueParam>;
}
