namespace FormsApplication
{
    partial class JobDetailBrowser
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
            this.MainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SectionTabControl = new System.Windows.Forms.TabControl();
            this.JobDetailTab = new System.Windows.Forms.TabPage();
            this.EmployerTab = new System.Windows.Forms.TabPage();
            this.WebBrowser = new System.Windows.Forms.TabPage();
            this.ControlMenuLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PreviousJobButton = new System.Windows.Forms.Button();
            this.NextJobButton = new System.Windows.Forms.Button();
            this.JobDetailRichTextBox = new System.Windows.Forms.RichTextBox();
            this.CommonWebBrowser = new System.Windows.Forms.WebBrowser();
            this.MainLayoutPanel.SuspendLayout();
            this.SectionTabControl.SuspendLayout();
            this.JobDetailTab.SuspendLayout();
            this.WebBrowser.SuspendLayout();
            this.ControlMenuLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayoutPanel
            // 
            this.MainLayoutPanel.ColumnCount = 2;
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.MainLayoutPanel.Controls.Add(this.SectionTabControl, 0, 0);
            this.MainLayoutPanel.Controls.Add(this.ControlMenuLayoutPanel, 1, 0);
            this.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainLayoutPanel.Name = "MainLayoutPanel";
            this.MainLayoutPanel.RowCount = 2;
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98.40319F));
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.596806F));
            this.MainLayoutPanel.Size = new System.Drawing.Size(944, 501);
            this.MainLayoutPanel.TabIndex = 0;
            // 
            // SectionTabControl
            // 
            this.SectionTabControl.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.SectionTabControl.Controls.Add(this.JobDetailTab);
            this.SectionTabControl.Controls.Add(this.EmployerTab);
            this.SectionTabControl.Controls.Add(this.WebBrowser);
            this.SectionTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SectionTabControl.Location = new System.Drawing.Point(3, 3);
            this.SectionTabControl.Multiline = true;
            this.SectionTabControl.Name = "SectionTabControl";
            this.SectionTabControl.SelectedIndex = 0;
            this.SectionTabControl.Size = new System.Drawing.Size(808, 486);
            this.SectionTabControl.TabIndex = 0;
            // 
            // JobDetailTab
            // 
            this.JobDetailTab.Controls.Add(this.JobDetailRichTextBox);
            this.JobDetailTab.Location = new System.Drawing.Point(4, 4);
            this.JobDetailTab.Name = "JobDetailTab";
            this.JobDetailTab.Padding = new System.Windows.Forms.Padding(3);
            this.JobDetailTab.Size = new System.Drawing.Size(781, 478);
            this.JobDetailTab.TabIndex = 0;
            this.JobDetailTab.Text = "JobDetailTab";
            this.JobDetailTab.UseVisualStyleBackColor = true;
            // 
            // EmployerTab
            // 
            this.EmployerTab.Location = new System.Drawing.Point(4, 4);
            this.EmployerTab.Name = "EmployerTab";
            this.EmployerTab.Padding = new System.Windows.Forms.Padding(3);
            this.EmployerTab.Size = new System.Drawing.Size(781, 478);
            this.EmployerTab.TabIndex = 1;
            this.EmployerTab.Text = "EmployerTab";
            this.EmployerTab.UseVisualStyleBackColor = true;
            // 
            // WebBrowser
            // 
            this.WebBrowser.Controls.Add(this.CommonWebBrowser);
            this.WebBrowser.Location = new System.Drawing.Point(4, 4);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.WebBrowser.Size = new System.Drawing.Size(781, 478);
            this.WebBrowser.TabIndex = 2;
            this.WebBrowser.Text = "WebBrowser";
            this.WebBrowser.UseVisualStyleBackColor = true;
            // 
            // ControlMenuLayoutPanel
            // 
            this.ControlMenuLayoutPanel.Controls.Add(this.PreviousJobButton);
            this.ControlMenuLayoutPanel.Controls.Add(this.NextJobButton);
            this.ControlMenuLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlMenuLayoutPanel.Location = new System.Drawing.Point(817, 3);
            this.ControlMenuLayoutPanel.Name = "ControlMenuLayoutPanel";
            this.ControlMenuLayoutPanel.Size = new System.Drawing.Size(124, 486);
            this.ControlMenuLayoutPanel.TabIndex = 1;
            // 
            // PreviousJobButton
            // 
            this.PreviousJobButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousJobButton.Name = "PreviousJobButton";
            this.PreviousJobButton.Size = new System.Drawing.Size(120, 23);
            this.PreviousJobButton.TabIndex = 0;
            this.PreviousJobButton.Text = "Previous Job Button";
            this.PreviousJobButton.UseVisualStyleBackColor = true;
            this.PreviousJobButton.Click += new System.EventHandler(this.PreviousJobButton_Click);
            // 
            // NextJobButton
            // 
            this.NextJobButton.Location = new System.Drawing.Point(3, 32);
            this.NextJobButton.Name = "NextJobButton";
            this.NextJobButton.Size = new System.Drawing.Size(120, 23);
            this.NextJobButton.TabIndex = 1;
            this.NextJobButton.Text = "Next Job Button";
            this.NextJobButton.UseVisualStyleBackColor = true;
            this.NextJobButton.Click += new System.EventHandler(this.NextJobButton_Click);
            // 
            // JobDetailRichTextBox
            // 
            this.JobDetailRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JobDetailRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.JobDetailRichTextBox.Name = "JobDetailRichTextBox";
            this.JobDetailRichTextBox.ReadOnly = true;
            this.JobDetailRichTextBox.Size = new System.Drawing.Size(775, 472);
            this.JobDetailRichTextBox.TabIndex = 0;
            this.JobDetailRichTextBox.Text = "";
            this.JobDetailRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.JobDetailRichTextBox_LinkClicked);
            // 
            // CommonWebBrowser
            // 
            this.CommonWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommonWebBrowser.Location = new System.Drawing.Point(3, 3);
            this.CommonWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.CommonWebBrowser.Name = "CommonWebBrowser";
            this.CommonWebBrowser.Size = new System.Drawing.Size(775, 472);
            this.CommonWebBrowser.TabIndex = 0;
            // 
            // JobDetailBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.MainLayoutPanel);
            this.Name = "JobDetailBrowser";
            this.Text = "JobSearchEnhancer";
            this.MainLayoutPanel.ResumeLayout(false);
            this.SectionTabControl.ResumeLayout(false);
            this.JobDetailTab.ResumeLayout(false);
            this.WebBrowser.ResumeLayout(false);
            this.ControlMenuLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayoutPanel;
        private System.Windows.Forms.TabControl SectionTabControl;
        private System.Windows.Forms.TabPage JobDetailTab;
        private System.Windows.Forms.TabPage EmployerTab;
        private System.Windows.Forms.TabPage WebBrowser;
        private System.Windows.Forms.FlowLayoutPanel ControlMenuLayoutPanel;
        private System.Windows.Forms.Button PreviousJobButton;
        private System.Windows.Forms.Button NextJobButton;
        private System.Windows.Forms.RichTextBox JobDetailRichTextBox;
        private System.Windows.Forms.WebBrowser CommonWebBrowser;

    }
}

