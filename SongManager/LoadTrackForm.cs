using ProLinkLib;
using ProLinkLib.Commands.StatusCommands;
using ProLinkLib.Database.Objects;
using ProLinkLib.Devices;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongManager
{
    public partial class LoadTrackForm : Form
    {
        Track current_track;
        UdpClient udpClient;
        public LoadTrackForm()
        {
            InitializeComponent();
        }

        public LoadTrackForm(Track current_track)
        {
            InitializeComponent();
            this.current_track = current_track;
        }

        private void LoadTrackForm_Load(object sender, EventArgs e)
        {

        }

        private void SendLoadTrackCommand(int cdj_id)
        {
            LoadTrackCommand ld_command = new LoadTrackCommand();
            var track = current_track;

            if (track == null)
            {
                Console.WriteLine("Cannot find the track with the specified id");
                return;
            }
  
            ld_command.ChannelID = ProLinkLib.ProLinkLib.VIRTUALCDJ_CHANNELID;
            ld_command.ChannelID2 = ProLinkLib.ProLinkLib.VIRTUALCDJ_CHANNELID;
            ld_command.DeviceName = Utils.NameToBytes(ProLinkLib.ProLinkLib.VIRTUALCDJ_DEVICENAME, 0x14);
            ld_command.DeviceToLoad = (byte)cdj_id;
            ld_command.TrackID = Utils.SwapEndianesss(BitConverter.GetBytes(track.RekordboxID));
            ld_command.TrackType = 0x01;
            ld_command.DeviceTrackListLocatedID = (byte)track.TrackChannelID;
            ld_command.DeviceTracklistLocation = (byte)track.TrackPhysicallyLocated;
            ld_command.Length = 0x34;

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, $"User load {track.TrackName}(0x{track.RekordboxID:X}) to DeviceID: {cdj_id}");
            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.DEBUG, "User sent LOAD_TRACKCOMMAND with this properties:\n" +
                                $"TrackID: 0x{track.RekordboxID:X}\n" +
                                $"DeviceToLoad: {ld_command.DeviceToLoad}\n" +
                                $"DeviceTrackListLocatedID: {ld_command.DeviceTrackListLocatedID}\n" +
                                $"DeviceTrackListLocation: {ld_command.DeviceTracklistLocation}\n");
            
            udpClient.Send(new PacketBuilder().BuildPacket(ld_command), new PacketBuilder().BuildPacket(ld_command).Length, track.cdj_location.BroadcastAddress, 50002);

            MessageBox.Show("Track sent to CDJ");

        }

        private void btn_cdj1_Click(object sender, EventArgs e)
        {
            SendLoadTrackCommand(1);
        }

        private void btn_cdj2_Click(object sender, EventArgs e)
        {
            SendLoadTrackCommand(2);
        }

        private void btn_cdj3_Click(object sender, EventArgs e)
        {
            SendLoadTrackCommand(3);
        }

        private void btn_cdj4_Click(object sender, EventArgs e)
        {
            SendLoadTrackCommand(4);
        }
    }
}
