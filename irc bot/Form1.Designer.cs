namespace irc_bot
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdConnect = new System.Windows.Forms.Button();
            this._lblLastMSG = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblChannel = new System.Windows.Forms.Label();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.lblConnected = new System.Windows.Forms.Label();
            this.tmrStopbot = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblNP = new System.Windows.Forms.Label();
            this.tmrApi = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(173, 134);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 0;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // _lblLastMSG
            // 
            this._lblLastMSG.AutoSize = true;
            this._lblLastMSG.Location = new System.Drawing.Point(12, 9);
            this._lblLastMSG.Name = "_lblLastMSG";
            this._lblLastMSG.Size = new System.Drawing.Size(78, 13);
            this._lblLastMSG.TabIndex = 1;
            this._lblLastMSG.Text = "Last message: ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(12, 139);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(49, 13);
            this.lblChannel.TabIndex = 2;
            this.lblChannel.Text = "Channel:";
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(67, 136);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(100, 20);
            this.txtChannel.TabIndex = 3;
            this.txtChannel.Text = "Exo0tixZ";
            // 
            // lblConnected
            // 
            this.lblConnected.AutoSize = true;
            this.lblConnected.Location = new System.Drawing.Point(374, 9);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(0, 13);
            this.lblConnected.TabIndex = 4;
            // 
            // tmrStopbot
            // 
            this.tmrStopbot.Interval = 300;
            this.tmrStopbot.Tick += new System.EventHandler(this.tmrStopbot_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lblNP
            // 
            this.lblNP.AutoSize = true;
            this.lblNP.Location = new System.Drawing.Point(12, 32);
            this.lblNP.Name = "lblNP";
            this.lblNP.Size = new System.Drawing.Size(69, 13);
            this.lblNP.TabIndex = 6;
            this.lblNP.Text = "Now Playing:";
            // 
            // tmrApi
            // 
            this.tmrApi.Interval = 10000;
            this.tmrApi.Tick += new System.EventHandler(this.tmrApi_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(452, 166);
            this.Controls.Add(this.lblNP);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.lblChannel);
            this.Controls.Add(this._lblLastMSG);
            this.Controls.Add(this.cmdConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twitch Chat Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Label _lblLastMSG;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Timer tmrStopbot;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblNP;
        private System.Windows.Forms.Timer tmrApi;
    }
}

