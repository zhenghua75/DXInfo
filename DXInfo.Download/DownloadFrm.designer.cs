namespace DXInfo.Download
{
    partial class DownloadFrm
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
            this.prgDownload = new System.Windows.Forms.ProgressBar();
            this.lbUrl = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lbPath = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.lbSummary = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // prgDownload
            // 
            this.prgDownload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prgDownload.Location = new System.Drawing.Point(0, 136);
            this.prgDownload.Name = "prgDownload";
            this.prgDownload.Size = new System.Drawing.Size(704, 21);
            this.prgDownload.TabIndex = 0;
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Location = new System.Drawing.Point(12, 37);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(65, 12);
            this.lbUrl.TabIndex = 1;
            this.lbUrl.Text = "服务器地址";
            // 
            // tbURL
            // 
            this.tbURL.Enabled = false;
            this.tbURL.Location = new System.Drawing.Point(75, 33);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(623, 21);
            this.tbURL.TabIndex = 2;
            this.tbURL.Text = "http://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE" +
    "/dotNetFx40_Full_x86_x64.exe";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(244, 80);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 50);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(12, 62);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(53, 12);
            this.lbPath.TabIndex = 1;
            this.lbPath.Text = "本地路径";
            // 
            // tbPath
            // 
            this.tbPath.Enabled = false;
            this.tbPath.Location = new System.Drawing.Point(76, 58);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(622, 21);
            this.tbPath.TabIndex = 2;
            this.tbPath.Text = "D:\\DotNetFx4.exe";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(456, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(12, 121);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 12);
            this.lbStatus.TabIndex = 1;
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(350, 80);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 50);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lbSummary
            // 
            this.lbSummary.AutoSize = true;
            this.lbSummary.Location = new System.Drawing.Point(114, 121);
            this.lbSummary.Name = "lbSummary";
            this.lbSummary.Size = new System.Drawing.Size(0, 12);
            this.lbSummary.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(562, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 5;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 157);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.lbSummary);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.lbUrl);
            this.Controls.Add(this.prgDownload);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文件下载";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgDownload;
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lbSummary;
        private System.Windows.Forms.Button button1;
    }
}

