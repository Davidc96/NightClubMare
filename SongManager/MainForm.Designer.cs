namespace SongManager
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDownload = new System.Windows.Forms.Button();
            this.dgv_tracklist = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.pb_artwork = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_bpm = new System.Windows.Forms.Label();
            this.lbl_year = new System.Windows.Forms.Label();
            this.btn_load = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tracklist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_artwork)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(675, 531);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(86, 54);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // dgv_tracklist
            // 
            this.dgv_tracklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tracklist.Location = new System.Drawing.Point(12, 49);
            this.dgv_tracklist.Name = "dgv_tracklist";
            this.dgv_tracklist.Size = new System.Drawing.Size(651, 536);
            this.dgv_tracklist.TabIndex = 1;
            this.dgv_tracklist.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tracklist_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(879, 531);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 54);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pb_artwork
            // 
            this.pb_artwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_artwork.Location = new System.Drawing.Point(709, 12);
            this.pb_artwork.Name = "pb_artwork";
            this.pb_artwork.Size = new System.Drawing.Size(220, 181);
            this.pb_artwork.TabIndex = 3;
            this.pb_artwork.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(691, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(691, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "BPM:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(691, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Year:";
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(104, 12);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(339, 20);
            this.tb_search.TabIndex = 7;
            this.tb_search.TextChanged += new System.EventHandler(this.tb_search_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Search by Name";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(726, 242);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(0, 13);
            this.lbl_name.TabIndex = 10;
            // 
            // lbl_bpm
            // 
            this.lbl_bpm.AutoSize = true;
            this.lbl_bpm.Location = new System.Drawing.Point(727, 290);
            this.lbl_bpm.Name = "lbl_bpm";
            this.lbl_bpm.Size = new System.Drawing.Size(0, 13);
            this.lbl_bpm.TabIndex = 11;
            // 
            // lbl_year
            // 
            this.lbl_year.AutoSize = true;
            this.lbl_year.Location = new System.Drawing.Point(726, 340);
            this.lbl_year.Name = "lbl_year";
            this.lbl_year.Size = new System.Drawing.Size(0, 13);
            this.lbl_year.TabIndex = 12;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(778, 531);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(86, 54);
            this.btn_load.TabIndex = 13;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 638);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.lbl_year);
            this.Controls.Add(this.lbl_bpm);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_search);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb_artwork);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgv_tracklist);
            this.Controls.Add(this.btnDownload);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Song Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tracklist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_artwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.DataGridView dgv_tracklist;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pb_artwork;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_bpm;
        private System.Windows.Forms.Label lbl_year;
        private System.Windows.Forms.Button btn_load;
    }
}

