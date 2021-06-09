using ArduinoCommunication.Events;
using ArduinoCommunication.Events.JsonEventImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication.Base
{
    public static class JsonEventConverter
    {
        private static Dictionary<string, Func<JsonEventBase, JsonEventBase>> convertMap = new();

        static JsonEventConverter()
        {
            RegisterConvertMap("log", json => LogJsonEvent.Cast(json));
        }

        public static void RegisterConvertMap<T>(string eventName, Func<JsonEventBase, T> convertFunc) where T : JsonEventBase
        {
            convertMap[eventName] = convertFunc;
            Log.Info($"已注册json事件转换的方法: {eventName}");
        }

        public static JsonEventBase Convert(JsonEventBase jsonEvent)
        {
            if (convertMap.TryGetValue(jsonEvent?.Name, out var convertFunc))
            {
                return convertFunc(jsonEvent);
            }

            Log.Warn($"JsonEventConverter无法转换处理此json事件: {jsonEvent.Name}");
            return null;
        }
    }
}
