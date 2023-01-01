﻿using ProLinkLib.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.UDP.StatusServer
{
    public class StatusServer
    {
        private UdpClient udpClient;
        private PacketBuilder packetBuilder;
        private StatusPacketParser packetParser;
        private int PORT = 50002;
        public string BroadcastAddress = "169.254.255.255";
        public Func<int, ICommand, bool> OnRecvPacketFunc;

        public StatusServer()
        {
            udpClient = new UdpClient();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));
            packetBuilder = new PacketBuilder();
            packetParser = new StatusPacketParser();
            OnRecvPacketFunc = OnRecvPacket;
        }

        // Create an empty function to avoid null pointers
        private bool OnRecvPacket(int packet_id, ICommand command)
        {
            return true;
        }

        public void initServer()
        {
            // Task to receive all the UDP packets
            var receiveTask = Task.Run(() =>
            {
                OnPacketReceivedTask();
            });
            //receiveTask.Wait();


            Console.WriteLine("[INFO] Status Server started at " + PORT);

        }
        private void OnPacketReceivedTask()
        {
            var from = new IPEndPoint(0, 0);
            while (true)
            {
                var recvBuffer = udpClient.Receive(ref from);
                packetParser.readPacket(recvBuffer, OnRecvPacketFunc);
            }
        }

        public void SendPacketBroadcast(ICommand packet)
        {
            var packet_b = packetBuilder.BuildPacket(packet);
            //udpClient.Send(packet_b, packet_b.Length, "192.168.1.255", PORT);
            udpClient.Send(packet_b, packet_b.Length, BroadcastAddress, PORT);
        }

        public void SendPacketToClient(string dstIp, ICommand packet)
        {
            var packet_b = packetBuilder.BuildPacket(packet);
            udpClient.Send(packet_b, packet_b.Length, dstIp, PORT);
        }
    }
}
