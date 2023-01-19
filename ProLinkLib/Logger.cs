using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib
{
    public class Logger
    {
        public static UdpClient udpClient;
        public static int PORT = 9000;
        public static string LOG_FILE_PATH = "logs\\";
        public static bool PRINT_DEBUG = false;

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

        public static void WriteLogFile(string log_name, LOG_TYPE log_type, string content)
        {
            string full_name = DateTime.Now.Date.ToString().Replace("/", "-").Replace(" 0:00:00", "") + " - " + log_name + ".txt";
            string full_log_content = DateTime.Now.ToLocalTime().ToString() + " - " + "[" + log_type + "] " + content;

            if(log_type == LOG_TYPE.DEBUG && PRINT_DEBUG == false)
            {
               return;
            }
           
            File.AppendAllText(LOG_FILE_PATH + full_name, full_log_content + "\n");
        }
    }
}
