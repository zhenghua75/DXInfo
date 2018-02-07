namespace FairiesCooler
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.pushMeButton = new System.Windows.Forms.Button();
            this.noPushMeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pushMeButton
            // 
            this.pushMeButton.Location = new System.Drawing.Point(503, 405);
            this.pushMeButton.Name = "pushMeButton";
            this.pushMeButton.Size = new System.Drawing.Size(93, 21);
            this.pushMeButton.TabIndex = 0;
            this.pushMeButton.Text = "Push me!";
            this.pushMeButton.UseVisualStyleBackColor = true;
            this.pushMeButton.Click += new System.EventHandler(this.pushMeButton_Click);
            // 
            // noPushMeButton
            // 
            this.noPushMeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.noPushMeButton.Location = new System.Drawing.Point(503, 432);
            this.noPushMeButton.Name = "noPushMeButton";
            this.noPushMeButton.Size = new System.Drawing.Size(93, 21);
            this.noPushMeButton.TabIndex = 1;
            this.noPushMeButton.Text = "No, Push me!";
            this.noPushMeButton.UseVisualStyleBackColor = true;
            this.noPushMeButton.Click += new System.EventHandler(this.noPushMeButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(40, 391);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "Beeeeeeeeeeeeeeeep!    This is a test of the label control.  If this had not been" +
    " a test you would have been given further instructions.";
            // 
            // SplashScreen
            // 
            this.AcceptButton = this.pushMeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.CancelButton = this.noPushMeButton;
            this.ClientSize = new System.Drawing.Size(589, 311);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noPushMeButton);
            this.Controls.Add(this.pushMeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushMeButton;
        private System.Windows.Forms.Button noPushMeButton;
        private System.Windows.Forms.Label label1;
    }
}

