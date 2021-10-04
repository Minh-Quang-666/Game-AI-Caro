
namespace Game_Caro
{
    partial class playchess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(playchess));
            this.pnchessboard = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.prbcooldown = new System.Windows.Forms.ProgressBar();
            this.tbplayname = new System.Windows.Forms.TextBox();
            this.tmcooldown = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayWithPeopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playWithTheMachineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computerAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAlgorithmCurentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printListAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAIcom = new System.Windows.Forms.TextBox();
            this.imgplayer = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgplayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnchessboard
            // 
            this.pnchessboard.Location = new System.Drawing.Point(10, 37);
            this.pnchessboard.Name = "pnchessboard";
            this.pnchessboard.Size = new System.Drawing.Size(1002, 789);
            this.pnchessboard.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(1015, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 338);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.tbAIcom);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.imgplayer);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.prbcooldown);
            this.panel2.Controls.Add(this.tbplayname);
            this.panel2.Location = new System.Drawing.Point(1018, 381);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 445);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "victory or defeat at skill";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(4, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "connect";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(4, 85);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(199, 29);
            this.textBox2.TabIndex = 2;
            // 
            // prbcooldown
            // 
            this.prbcooldown.Location = new System.Drawing.Point(4, 50);
            this.prbcooldown.Name = "prbcooldown";
            this.prbcooldown.Size = new System.Drawing.Size(199, 29);
            this.prbcooldown.TabIndex = 1;
            // 
            // tbplayname
            // 
            this.tbplayname.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbplayname.Location = new System.Drawing.Point(4, 15);
            this.tbplayname.Multiline = true;
            this.tbplayname.Name = "tbplayname";
            this.tbplayname.ReadOnly = true;
            this.tbplayname.Size = new System.Drawing.Size(199, 29);
            this.tbplayname.TabIndex = 0;
            this.tbplayname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tmcooldown
            // 
            this.tmcooldown.Tick += new System.EventHandler(this.tmcooldown_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.computerAlgorithmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1385, 30);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 26);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayWithPeopleToolStripMenuItem,
            this.playWithTheMachineToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // PlayWithPeopleToolStripMenuItem
            // 
            this.PlayWithPeopleToolStripMenuItem.Name = "PlayWithPeopleToolStripMenuItem";
            this.PlayWithPeopleToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.PlayWithPeopleToolStripMenuItem.Text = "Play with people";
            this.PlayWithPeopleToolStripMenuItem.Click += new System.EventHandler(this.PlayWithPeopleToolStripMenuItem_Click);
            // 
            // playWithTheMachineToolStripMenuItem
            // 
            this.playWithTheMachineToolStripMenuItem.Name = "playWithTheMachineToolStripMenuItem";
            this.playWithTheMachineToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.playWithTheMachineToolStripMenuItem.Text = "Play with the machine";
            this.playWithTheMachineToolStripMenuItem.Click += new System.EventHandler(this.playWithTheMachineToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.CheckOnClick = true;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // computerAlgorithmToolStripMenuItem
            // 
            this.computerAlgorithmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAlgorithmCurentToolStripMenuItem,
            this.printListAlgorithmToolStripMenuItem});
            this.computerAlgorithmToolStripMenuItem.Name = "computerAlgorithmToolStripMenuItem";
            this.computerAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.computerAlgorithmToolStripMenuItem.Text = "Computer Algorithm";
            // 
            // showAlgorithmCurentToolStripMenuItem
            // 
            this.showAlgorithmCurentToolStripMenuItem.Name = "showAlgorithmCurentToolStripMenuItem";
            this.showAlgorithmCurentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.showAlgorithmCurentToolStripMenuItem.Size = new System.Drawing.Size(296, 26);
            this.showAlgorithmCurentToolStripMenuItem.Text = "Show Algorithm Curent";
            this.showAlgorithmCurentToolStripMenuItem.Click += new System.EventHandler(this.showAlgorithmCurentToolStripMenuItem_Click);
            // 
            // printListAlgorithmToolStripMenuItem
            // 
            this.printListAlgorithmToolStripMenuItem.Name = "printListAlgorithmToolStripMenuItem";
            this.printListAlgorithmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printListAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(296, 26);
            this.printListAlgorithmToolStripMenuItem.Text = "Print List Algorithm";
            this.printListAlgorithmToolStripMenuItem.Click += new System.EventHandler(this.printListAlgorithmToolStripMenuItem_Click);
            // 
            // tbAIcom
            // 
            this.tbAIcom.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAIcom.Location = new System.Drawing.Point(4, 204);
            this.tbAIcom.Multiline = true;
            this.tbAIcom.Name = "tbAIcom";
            this.tbAIcom.Size = new System.Drawing.Size(345, 238);
            this.tbAIcom.TabIndex = 6;
            // 
            // imgplayer
            // 
            this.imgplayer.BackColor = System.Drawing.SystemColors.Control;
            this.imgplayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgplayer.Location = new System.Drawing.Point(209, 15);
            this.imgplayer.Name = "imgplayer";
            this.imgplayer.Size = new System.Drawing.Size(133, 133);
            this.imgplayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgplayer.TabIndex = 4;
            this.imgplayer.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(345, 328);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // playchess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 888);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnchessboard);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(0, 200);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "playchess";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.playchess_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgplayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnchessboard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ProgressBar prbcooldown;
        private System.Windows.Forms.TextBox tbplayname;
        private System.Windows.Forms.PictureBox imgplayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmcooldown;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayWithPeopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playWithTheMachineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computerAlgorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAlgorithmCurentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printListAlgorithmToolStripMenuItem;
        private System.Windows.Forms.TextBox tbAIcom;
    }
}

