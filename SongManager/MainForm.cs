using ProLinkLib;
using ProLinkLib.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SongManager
{
    public partial class MainForm : Form
    {
        private MetadataDB metadataDB;
        private DataTable dt_tracks;
        private Track selectedTrack;

        public MainForm()
        {
            InitializeComponent();
            metadataDB = new MetadataDB();
            dt_tracks = new DataTable();

            metadataDB.ImportTrackListJson(InitDB());

            // Init datatable
            dt_tracks.Columns.Add("TrackID", typeof(uint));
            dt_tracks.Columns.Add("Track Name", typeof(string));
            dt_tracks.Columns.Add("Year", typeof(string));
            dt_tracks.Columns.Add("BPM", typeof(int));
            dt_tracks.Columns.Add("Track Path", typeof(string));

            //Thread.Sleep(1000);
            //System.IO.File.Delete("rekordbox_track.db"); // delete the intermediate file
        }

        private string InitDB()
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(System.IO.File.ReadAllText("rekordbox_track.db")));
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            var tracks = metadataDB.GetTracks();

            foreach (var track in tracks)
            {
                if (track.TrackName != null)
                {
                    dt_tracks.Rows.Add(new object[] { track.RekordboxID,
                                                      track.TrackName,
                                                      track.Year == 0 ? "N/A" : track.Year.ToString(),
                                                      track.Tempo / 100,
                                                      track.TrackPath });

                }
            }

            dgv_tracklist.DataSource = dt_tracks;

        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            dt_tracks.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "Track Name", tb_search.Text);
        }

        private void dgv_tracklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var track = metadataDB.GetTrackByRBId((uint)dgv_tracklist.Rows[e.RowIndex].Cells[0].Value);
            if (track != null)
            {
                selectedTrack = track;
            }
            else
            {
                MessageBox.Show("Track is null!");
                return;
            }

            if (track.cdj_location == null)
            {
                btnDownload.Enabled = false;
                btn_load.Enabled = false;
            }
            else
            {
                btnDownload.Enabled = true;
                btn_load.Enabled = true;
            }

            lbl_name.Text = track.TrackName;
            lbl_bpm.Text = (track.Tempo / 100).ToString();
            
            if (track.Year == 0)
            {
                lbl_year.Text = "N/A";
            }
            else
            {
                lbl_year.Text = track.Year.ToString();
            }

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (selectedTrack != null)
            {
                metadataDB.DownloadTrack(selectedTrack, "music");
                MessageBox.Show("Track downloaded successfully!");
            }
            else
            {
                btnDownload.Enabled = false;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            if (selectedTrack != null)
            {
                metadataDB.DownloadTrack(selectedTrack, "music");
                MessageBox.Show("Track downloaded successfully!");
            }
            else
            {
                btn_load.Enabled = false;
            }
        }
    }
}
