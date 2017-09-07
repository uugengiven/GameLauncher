namespace TestSteamLauncher
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblGame = new System.Windows.Forms.Label();
            this.lblSteamLoc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start DbD 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(311, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 56);
            this.button2.TabIndex = 1;
            this.button2.Text = "Start DbD 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(311, 303);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 56);
            this.button3.TabIndex = 2;
            this.button3.Text = "Close Steam";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(311, 241);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(127, 56);
            this.button4.TabIndex = 3;
            this.button4.Text = "Start Web";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblGame
            // 
            this.lblGame.AutoSize = true;
            this.lblGame.Location = new System.Drawing.Point(80, 82);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(45, 13);
            this.lblGame.TabIndex = 4;
            this.lblGame.Text = "lblGame";
            // 
            // lblSteamLoc
            // 
            this.lblSteamLoc.AutoSize = true;
            this.lblSteamLoc.Location = new System.Drawing.Point(80, 144);
            this.lblSteamLoc.Name = "lblSteamLoc";
            this.lblSteamLoc.Size = new System.Drawing.Size(35, 13);
            this.lblSteamLoc.TabIndex = 5;
            this.lblSteamLoc.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 501);
            this.Controls.Add(this.lblSteamLoc);
            this.Controls.Add(this.lblGame);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.Label lblSteamLoc;
    }
}

