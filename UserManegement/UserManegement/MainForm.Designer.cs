namespace UserManegement
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.statusLabelUser = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemTimeTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTaskManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReporting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabelUser.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(12, 27);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1505, 443);
            this.mainPanel.TabIndex = 0;
            // 
            // statusLabelUser
            // 
            this.statusLabelUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusLabelUser.Location = new System.Drawing.Point(0, 481);
            this.statusLabelUser.Name = "statusLabelUser";
            this.statusLabelUser.Size = new System.Drawing.Size(1529, 22);
            this.statusLabelUser.TabIndex = 2;
            this.statusLabelUser.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTimeTracking,
            this.menuItemTaskManagement,
            this.menuItemReporting,
            this.menuItemExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1529, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemTimeTracking
            // 
            this.menuItemTimeTracking.Name = "menuItemTimeTracking";
            this.menuItemTimeTracking.Size = new System.Drawing.Size(95, 20);
            this.menuItemTimeTracking.Text = "Учёт времени";
            this.menuItemTimeTracking.Click += new System.EventHandler(this.menuItemTimeTracking_Click);
            // 
            // menuItemTaskManagement
            // 
            this.menuItemTaskManagement.Name = "menuItemTaskManagement";
            this.menuItemTaskManagement.Size = new System.Drawing.Size(140, 20);
            this.menuItemTaskManagement.Text = "Управление задачами";
            this.menuItemTaskManagement.Click += new System.EventHandler(this.menuItemTaskManagement_Click);
            // 
            // menuItemReporting
            // 
            this.menuItemReporting.Name = "menuItemReporting";
            this.menuItemReporting.Size = new System.Drawing.Size(60, 20);
            this.menuItemReporting.Text = "Отчёты";
            this.menuItemReporting.Click += new System.EventHandler(this.menuItemReporting_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(54, 20);
            this.menuItemExit.Text = "Выход";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1529, 503);
            this.Controls.Add(this.statusLabelUser);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.mainPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.statusLabelUser.ResumeLayout(false);
            this.statusLabelUser.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.StatusStrip statusLabelUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemTimeTracking;
        private System.Windows.Forms.ToolStripMenuItem menuItemTaskManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemReporting;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
    }
}