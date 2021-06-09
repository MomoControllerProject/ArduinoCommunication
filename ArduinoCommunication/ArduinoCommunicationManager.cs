using RJCP.IO.Ports;
using System;
using System.Threading.Tasks;

namespace ArduinoCommunication
{
    public class ArduinoCommunicationManager
    {
        private static ArduinoCommunicationManager instance;
        public static ArduinoCommunicationManager Instance => instance ?? (instance = new ArduinoCommunicationManager());
    
        public string[] GetAvaliablePorts()
        {
            return SerialPortStream.GetPortNames();
        }

        public ValueTask<SerialPortStream> ConnectSerialPort(string serialPortName)
        {
            var src = new SerialPortStream(serialPortName);
            return ValueTask.FromResult(src);
        }
    }
}
