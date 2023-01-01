using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PCLILogger
{
    public class LoggerServer
    {
        private UdpClient udpClient;
        private int PORT = 9000;

        public LoggerServer()
        {
            udpClient = new UdpClient();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));
        }

        public void initServer()
        {
            // Task to receive all the UDP packets
            var receiveTask = Task.Run(() =>
            {
                OnRecvPacket();
            });
        }

        public void OnRecvPacket()
        {
            var from = new IPEndPoint(0, 0);
            while (true)
            {
                var recvBuffer = udpClient.Receive(ref from);
                string messages = Encoding.UTF8.GetString(recvBuffer);
                string[] splitted_message = messages.Split('|');

                string log_type = splitted_message[0];
                string print_mode = splitted_message[1];
                byte[] payload = Encoding.UTF8.GetBytes(splitted_message[2]);

                Console.Write(DateTime.Now.ToString("h:mm:ss tt") + " - ");
                if(print_mode == "HEX")
                {
                    Console.WriteLine(Hex.Dump(payload));
                }

                if(print_mode == "STRING")
                {
                    Console.WriteLine("[" + log_type + "] " + Encoding.UTF8.GetString(payload));
                }

                Console.WriteLine();
                
            }
        }
    }
}
