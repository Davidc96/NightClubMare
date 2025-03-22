namespace SongManager
{
    partial class LoadTrackForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_cdj1 = new System.Windows.Forms.Button();
            this.btn_cdj2 = new System.Windows.Forms.Button();
            this.btn_cdj3 = new System.Windows.Forms.Button();
            this.btn_cdj4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_cdj1
            // 
            this.btn_cdj1.Location = new System.Drawing.Point(12, 52);
            this.btn_cdj1.Name = "btn_cdj1";
            this.btn_cdj1.Size = new System.Drawing.Size(114, 94);
            this.btn_cdj1.TabIndex = 0;
            this.btn_cdj1.Text = "CDJ 1";
            this.btn_cdj1.UseVisualStyleBackColor = true;
            this.btn_cdj1.Click += new System.EventHandler(this.btn_cdj1_Click);
            // 
            // btn_cdj2
            // 
            this.btn_cdj2.Location = new System.Drawing.Point(132, 52);
            this.btn_cdj2.Name = "btn_cdj2";
            this.btn_cdj2.Size = new System.Drawing.Size(114, 94);
            this.btn_cdj2.TabIndex = 1;
            this.btn_cdj2.Text = "CDJ 2";
            this.btn_cdj2.UseVisualStyleBackColor = true;
            this.btn_cdj2.Click += new System.EventHandler(this.btn_cdj2_Click);
            // 
            // btn_cdj3
            // 
            this.btn_cdj3.Location = new System.Drawing.Point(252, 52);
            this.btn_cdj3.Name = "btn_cdj3";
            this.btn_cdj3.Size = new System.Drawing.Size(114, 94);
            this.btn_cdj3.TabIndex = 2;
            this.btn_cdj3.Text = "CDJ 3";
            this.btn_cdj3.UseVisualStyleBackColor = true;
            this.btn_cdj3.Click += new System.EventHandler(this.btn_cdj3_Click);
            // 
            // btn_cdj4
            // 
            this.btn_cdj4.Location = new System.Drawing.Point(372, 52);
            this.btn_cdj4.Name = "btn_cdj4";
            this.btn_cdj4.Size = new System.Drawing.Size(114, 94);
            this.btn_cdj4.TabIndex = 3;
            this.btn_cdj4.Text = "CDJ 4";
            this.btn_cdj4.UseVisualStyleBackColor = true;
            this.btn_cdj4.Click += new System.EventHandler(this.btn_cdj4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the CDJ ID you want to load the track";
            // 
            // LoadTrackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 156);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cdj4);
            this.Controls.Add(this.btn_cdj3);
            this.Controls.Add(this.btn_cdj2);
            this.Controls.Add(this.btn_cdj1);
            this.Name = "LoadTrackForm";
            this.Text = "Which CDJ?";
            this.Load += new System.EventHandler(this.LoadTrackForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cdj1;
        private System.Windows.Forms.Button btn_cdj2;
        private System.Windows.Forms.Button btn_cdj3;
        private System.Windows.Forms.Button btn_cdj4;
        private System.Windows.Forms.Label label1;
    }
}