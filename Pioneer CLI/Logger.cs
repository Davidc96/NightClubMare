using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI
{
    public class Logger
    {
        public static UdpClient udpClient;
        public static int PORT = 9000;
        
        public enum LOG_TYPE
        {
            INFO,
            DEBUG,
            WARNING,
            ERROR
        }

        public enum PRINT_MODE
        {
            HEX,
            STRING
        }

        public static void WriteMessage(byte[] message, LOG_TYPE log_type = LOG_TYPE.INFO, PRINT_MODE print_mode = PRINT_MODE.STRING)
        {
            udpClient = new UdpClient();
            udpClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT));
            udpClient.Send(Encoding.UTF8.GetBytes(log_type + "|" + print_mode + "|").Concat(message).ToArray(), Encoding.UTF8.GetBytes(log_type + "|" + print_mode + "|").Concat(message).ToArray().Length);
            udpClient.Close();
        }
    }
}
