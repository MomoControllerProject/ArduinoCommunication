using ArduinoCommunication.Base;
using ArduinoCommunication.EventDispatchers;
using ArduinoCommunication.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoCommunication.Receives
{
    public class JsonEventReceiver : IReceive
    {
        private CancellationTokenSource cancelTokenSource;
        private bool isProcessing = false;
        private Stream inputStream = null;

        public bool IsPrecessing => isProcessing;
        public Stream InputStream { get => inputStream; set => SetupInputStream(inputStream); }
        public IEventDispatcher EventDispatcher { get; set; }

        public void Begin()
        {
            if (isProcessing)
            {
                return;
            }

            cancelTokenSource = new CancellationTokenSource();

            isProcessing = true;
            OnProcessing();
        }

        private async void OnProcessing()
        {
            var stack = 0;
            char prevCh = (char)0;
            var sb = new StringBuilder();

            var reader = new StreamReader(InputStream);
            var buffer = new char[1];

            while (!(reader.EndOfStream || cancelTokenSource.IsCancellationRequested))
            {
                var read = await reader.ReadAsync(buffer, cancelTokenSource.Token);
                if (read == 0)
                    continue;
                var ch = buffer[0];
                switch (ch)
                {
                    case '{':
                        if (prevCh == '\\')
                            break;
                        sb.Append(ch);
                        stack++;
                        break;
                    case '}':
                        if (prevCh == '\\')
                            break;
                        sb.Append(ch);
                        stack = Math.Max(0, stack - 1);
                        if (stack == 0 && sb.Length > 0)
                        {
                            var jsonStr = sb.ToString();
                            var o = JsonSerializer.Deserialize<JsonEventBase>(jsonStr);
                            sb.Clear();
                            var d = JsonEventConverter.Convert(o);
                            EventDispatcher?.FireEvent(d);
                        }
                        break;
                    default:
                        if (stack > 0)
                            sb.Append(ch);
                        break;
                }
                prevCh = ch;
            }
        }

        public void SetupEventDispatcher(IEventDispatcher eventDispatcher) => EventDispatcher = eventDispatcher;

        public void SetupInputStream(Stream inputStream)
        {
            if (isProcessing)
            {
                throw new Exception("can't set input stream when object is procssing!");
            }
            this.inputStream = inputStream;
        }

        public void Stop()
        {
            if (!isProcessing)
            {
                return;
            }

            cancelTokenSource.Cancel();

            isProcessing = false;
        }
    }
}
