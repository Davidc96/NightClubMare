﻿using ProLinkLib.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.UDP.SyncServer
{
    public class SyncServer
    {
        private UdpClient udpClient;
        private UdpClient udpClient_ow;
        private PacketBuilder packetBuilder;
        private SyncPacketParser packetParser;
        private int PORT = 50001;
        public string BroadcastAddress = "169.254.255.255";
        public byte[] IP;
        public Func<int, ICommand, bool> OnRecvPacketFunc;

        public SyncServer()
        {
            udpClient = new UdpClient();
            udpClient_ow = new UdpClient();
            packetBuilder = new PacketBuilder();
            packetParser = new SyncPacketParser();
            OnRecvPacketFunc = OnRecvPacket;
        }

        // Create an empty function to avoid null pointers
        private bool OnRecvPacket(int packet_id, ICommand command)
        {
            return true;
        }

        public void initServer()
        {
            // Init Bind connection
            //udpClient.Client.Bind(new IPEndPoint(new IPAddress(IP), PORT));
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));
            
            // Task to receive all the UDP packets
            var receiveTask = Task.Run(() =>
            {
                OnPacketReceivedTask();
            });
            //receiveTask.Wait();

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Discover Server started at " + PORT);
            Console.WriteLine("[INFO] Sync Server started at " + PORT);

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

        public void SendPacketBroadcast(byte[] packet)
        {
            //udpClient.Send(packet_b, packet_b.Length, "192.168.1.255", PORT);
            udpClient.Send(packet, packet.Length, BroadcastAddress, PORT);
        }


        public void SendPacketToClient(string dstIp, ICommand packet)
        {
            var packet_b = packetBuilder.BuildPacket(packet);
            udpClient_ow.Connect(new IPEndPoint(IPAddress.Parse(dstIp), PORT));
            udpClient_ow.Send(packet_b, packet_b.Length);
        }

        public void SendPacketToClient(string dstIp, byte[] packet)
        {
            udpClient_ow.Connect(new IPEndPoint(IPAddress.Parse(dstIp), PORT));
            udpClient_ow.Send(packet, packet.Length);
        }
    }
}
