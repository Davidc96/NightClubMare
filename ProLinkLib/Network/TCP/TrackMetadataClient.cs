using ProLinkLib.Commands.MetadataCommands;
using ProLinkLib.Commands.MetadataCommands.Messages;
using ProLinkLib.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.TCP
{
    public class TrackMetadataClient
    {
        private int INITIAL_PORT = 12523;
        private string IP;
        private int request_port = 0;

        private TcpClient request_client;
        private PacketBuilder pb;
        private uint PacketCounter = 1;
        private int TrackID;
        
        public TrackMetadataClient()
        {
            request_client = new TcpClient();
            pb = new PacketBuilder();
            TrackID = 1;
        }
        public void ConnectToDB(string IP)
        {
            byte[] response = null;
            try
            {
                if (request_port != 0)
                {
                    // We already know the port so skip it
                    return;
                }

                TcpClient initial_client = new TcpClient();
                this.IP = IP;

                initial_client.Connect(IP, INITIAL_PORT);
                if (!initial_client.Connected)
                {
                    Console.WriteLine("[ERROR] Failed to connect to the DBServer");
                    return;
                }

                NetworkStream nw_st = initial_client.GetStream();

                // Send HELLO Message
                DBServerQueryPacketCommand q_server_cmd = new DBServerQueryPacketCommand();
                DBServerQueryPacketResponseCommand q_server_resp_cmd = new DBServerQueryPacketResponseCommand();
                response = new byte[q_server_resp_cmd.GetSize()];

                nw_st.Write(q_server_cmd.ToBytes(), 0, q_server_cmd.GetSize());

                // Receive the RemoteDB PORT
                nw_st.Read(response, 0, response.Length);
                q_server_resp_cmd.FromBytes(response);

                request_port = q_server_resp_cmd.Port;

                Console.WriteLine("[DEBUG] Port Received: " + request_port);

                initial_client.Close();
            }
            catch(Exception ex)
            {
                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.ERROR, "TrackMetadataClient.cs-ConnectToDB: Exception error\n" + ex.Message + "\nDBQueryResponse:\n" + Hex.Dump(response));
            }
        }

        public void SendHello(byte ID, bool isRekordbox = false)
        {
            DBConnectionSetupPacket db_setup = new DBConnectionSetupPacket();
            DBConnectionSetupPacketResponse db_setup_response = new DBConnectionSetupPacketResponse();

            byte[] response = new byte[db_setup_response.GetSize()];
            byte[] setup_response = new byte[0x20];
            try
            {
                request_client = new TcpClient();

                request_client.Connect(IP, request_port);

                if (!request_client.Connected)
                {
                    Console.WriteLine("[ERROR] Failed to connect to the DBServer");
                    return;
                }

                NetworkStream nw_st = request_client.GetStream();

                // Send HELLO message
                response = new byte[db_setup_response.GetSize()];

                nw_st.Write(db_setup.ToBytes(), 0, db_setup.GetSize());

                // Receive the HELLO response message
                nw_st.Read(response, 0, response.Length);

                // If everything is correct let's send the Query Setup Message
                QuerySetupMessage q_setup = new QuerySetupMessage();
                QuerySetupResponseMessage q_setup_res = new QuerySetupResponseMessage();

                q_setup.Type = 0x0;
                q_setup.FieldType = 0x0F;
                q_setup.NumberOfFields = 0x01;

                q_setup.ArgumentInformation = new byte[0xC];
                q_setup.ArgumentInformation[0] = 0x06;

                q_setup.Arguments = new byte[0x5];
                q_setup.Arguments[0] = 0x11;
                q_setup.Arguments[4] = ID;

                byte[] builded_packet = pb.BuildMessage(0xFFFFFFFE, q_setup);
                setup_response = new byte[0x20];

                nw_st.Write(builded_packet, 0, builded_packet.Length);

                // Receive the Response
                nw_st.Read(setup_response, 0, setup_response.Length);

                q_setup_res.ParseBytes(setup_response, isRekordbox);
                Console.WriteLine("[DEBUG] Received ChannelID: " + q_setup_res.ChannelID[3]);
            }
            catch(Exception ex)
            {
                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.ERROR, " TrackMetadataClient.cs-SendHello(): Exception error\n" + ex.Message + 
                    "\nHello_Response:\n" + Hex.Dump(response) + "\nSetup Response packet:\n" + Hex.Dump(setup_response));     
            }
        }

        public void Disconnect()
        {
            DisconnectMessage disconnect = new DisconnectMessage();
            NetworkStream nw_st = request_client.GetStream();
            disconnect.Type = 0x0100;
            disconnect.FieldType = 0x0F;
            disconnect.NumberOfFields = 0x00;
            disconnect.ArgumentInformation = new byte[0xC];
            disconnect.Arguments = null;

            var disconnect_bytes = pb.BuildMessage(0xFFFFFFFE, disconnect);

            nw_st.Write(disconnect_bytes, 0, disconnect_bytes.Length);
            
            request_client.Client.Disconnect(true);
            request_client.Close();
        }

        public Dictionary<int, Track> GetAllTracks(byte ID, byte TrackChannelID, byte TrackPhysicallyLoacted, byte TrackType, bool isRekordbox = false)
        {
            Dictionary<int, Track> tracks = new Dictionary<int, Track>();
            byte[] track_response = new byte[0x20];
            byte[] render_response = new byte[0x200];
            uint offset = 0x00;

            for (int i = 0; i < 100; i++) // Let's limit the number of tracks just in case it brakes
            {
                try
                {
                    RequestAllTracksMessage req_tracks = new RequestAllTracksMessage();
                    NetworkStream nw_st = request_client.GetStream();
                    req_tracks.Type = 0x1004;
                    req_tracks.FieldType = 0xF;
                    req_tracks.NumberOfFields = 0x02;
                    req_tracks.ChannelID = ID;
                    req_tracks.TrackPhysicallyLocated = TrackPhysicallyLoacted;
                    req_tracks.TrackType = TrackType;
                    req_tracks.BuildArguments();

                    var request_bytes = pb.BuildMessage(PacketCounter, req_tracks);

                    // Send REQUEST ALL TRACKS Message
                    nw_st.Write(request_bytes, 0, request_bytes.Length);

                    // Response, should be 4000
                    TrackMetadataAvailableResponseMessage trackmetadata = new TrackMetadataAvailableResponseMessage();
                    track_response = new byte[0x20];

                    nw_st.Read(track_response, 0, track_response.Length);
                    trackmetadata.ParseBytes(track_response, isRekordbox);
                    // Console.WriteLine("[DEBUG] Number of tracks: " + trackmetadata.TrackCounter);

                    PacketCounter += 1;


                    // Send Render Menu to retrieve the tracks
                    RenderMenuRequestMessage render_message = new RenderMenuRequestMessage();
                    render_message.Type = 0x3000;
                    render_message.FieldType = 0x0F;
                    render_message.NumberOfFields = 0x06;
                    render_message.ChannelID = ID;
                    render_message.TrackPhysicallyLocated = TrackPhysicallyLoacted;
                    render_message.TrackType = TrackType;
                    render_message.Offset = offset;
                    render_message.Limit = 1;
                    render_message.Total = trackmetadata.TrackCounter;

                    render_message.BuildArguments();
                    var render_request = pb.BuildMessage(PacketCounter, render_message);
                    nw_st.Write(render_request, 0, render_request.Length);

                    // Get All the responses and parse them
                    render_response = new byte[0x200];


                    nw_st.Read(render_response, 0, render_response.Length);
                    // Console.WriteLine(Hex.Dump(render_response));

                    byte[] header_response = new byte[0x20];
                    byte[] music_response = new byte[0x200];
                    byte[] footer_response = new byte[0x14];

                    using (BinaryReader bin = new BinaryReader(new MemoryStream(render_response)))
                    {
                        header_response = bin.ReadBytes(0x20);
                        music_response = bin.ReadBytes((int)(bin.BaseStream.Length - 0x20));
                    }

                    RenderMenuResponseMessage render_resp = new RenderMenuResponseMessage();
                    render_resp.ParseBytes(music_response);

                    Track track = new Track();
                    track.RekordboxID = render_resp.RekordboxTrackID;
                    track.TitleName = render_resp.TitleName;
                    track.TrackPhysicallyLocated = TrackPhysicallyLoacted;
                    track.TrackChannelID = TrackChannelID;

                    // If a song is duplicated in one or more lists, rekordbox shows it as an independent track so let's remove it if we already have the title
                    if (!Utils.IsTrackDuplicated(tracks, track.TitleName))
                    {
                        tracks.Add(TrackID, track);
                        TrackID += 1;
                    }

                    PacketCounter += 1;
                    offset += 1;
                }
                catch(Exception ex)
                {
                    Logger.WriteLogFile("app_client", Logger.LOG_TYPE.ERROR, "TrackMetadataClient.cs-GetAllTracks(): Exception while parsing\n" + ex.Message + 
                        "\nTrack_Response:\n" + Hex.Dump(track_response) + "\nRender_response:\n" + Hex.Dump(render_response));
                }


                Task.Delay(1000);
            }
            
            return tracks;
        }
       
    }
}
