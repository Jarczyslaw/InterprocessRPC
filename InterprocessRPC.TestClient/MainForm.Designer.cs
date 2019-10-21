namespace InterprocessRPC.TestClient
{
    partial class MainForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnCheckConnection = new System.Windows.Forms.Button();
            this.btnGetMessage = new System.Windows.Forms.Button();
            this.btnGetServerInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(283, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 101);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 1;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(283, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCheckConnection
            // 
            this.btnCheckConnection.Location = new System.Drawing.Point(12, 70);
            this.btnCheckConnection.Name = "btnCheckConnection";
            this.btnCheckConnection.Size = new System.Drawing.Size(283, 23);
            this.btnCheckConnection.TabIndex = 3;
            this.btnCheckConnection.Text = "Check connection";
            this.btnCheckConnection.UseVisualStyleBackColor = true;
            this.btnCheckConnection.Click += new System.EventHandler(this.btnCheckConnection_Click);
            // 
            // btnGetMessage
            // 
            this.btnGetMessage.Location = new System.Drawing.Point(118, 99);
            this.btnGetMessage.Name = "btnGetMessage";
            this.btnGetMessage.Size = new System.Drawing.Size(177, 23);
            this.btnGetMessage.TabIndex = 4;
            this.btnGetMessage.Text = "Get message";
            this.btnGetMessage.UseVisualStyleBackColor = true;
            this.btnGetMessage.Click += new System.EventHandler(this.btnGetMessage_Click);
            // 
            // btnGetServerInfo
            // 
            this.btnGetServerInfo.Location = new System.Drawing.Point(12, 127);
            this.btnGetServerInfo.Name = "btnGetServerInfo";
            this.btnGetServerInfo.Size = new System.Drawing.Size(283, 23);
            this.btnGetServerInfo.TabIndex = 5;
            this.btnGetServerInfo.Text = "Get server info";
            this.btnGetServerInfo.UseVisualStyleBackColor = true;
            this.btnGetServerInfo.Click += new System.EventHandler(this.btnGetServerInfo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 167);
            this.Controls.Add(this.btnGetServerInfo);
            this.Controls.Add(this.btnGetMessage);
            this.Controls.Add(this.btnCheckConnection);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "InterprocessRPC - Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCheckConnection;
        private System.Windows.Forms.Button btnGetMessage;
        private System.Windows.Forms.Button btnGetServerInfo;
    }
}

