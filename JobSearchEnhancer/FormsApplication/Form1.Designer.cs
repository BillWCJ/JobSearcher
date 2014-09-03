namespace FormsApplication
{
    partial class Window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.PrevJobBtn = new System.Windows.Forms.Button();
            this.NextJobBtn = new System.Windows.Forms.Button();
            this.JobDetailTextBox = new System.Windows.Forms.RichTextBox();
            this.Tab = new System.Windows.Forms.TabControl();
            this.JobDetailTab = new System.Windows.Forms.TabPage();
            this.EmployerDetailTab = new System.Windows.Forms.TabPage();
            this.Tab.SuspendLayout();
            this.JobDetailTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrevJobBtn
            // 
            resources.ApplyResources(this.PrevJobBtn, "PrevJobBtn");
            this.PrevJobBtn.Name = "PrevJobBtn";
            this.PrevJobBtn.UseVisualStyleBackColor = true;
            this.PrevJobBtn.UseWaitCursor = true;
            this.PrevJobBtn.Click += new System.EventHandler(this.PrevJobBtn_Click);
            // 
            // NextJobBtn
            // 
            resources.ApplyResources(this.NextJobBtn, "NextJobBtn");
            this.NextJobBtn.Name = "NextJobBtn";
            this.NextJobBtn.UseVisualStyleBackColor = true;
            this.NextJobBtn.UseWaitCursor = true;
            this.NextJobBtn.Click += new System.EventHandler(this.NextJobBtn_Click);
            // 
            // JobDetailTextBox
            // 
            this.JobDetailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.JobDetailTextBox, "JobDetailTextBox");
            this.JobDetailTextBox.Name = "JobDetailTextBox";
            this.JobDetailTextBox.UseWaitCursor = true;
            this.JobDetailTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Tab
            // 
            resources.ApplyResources(this.Tab, "Tab");
            this.Tab.Controls.Add(this.JobDetailTab);
            this.Tab.Controls.Add(this.EmployerDetailTab);
            this.Tab.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.Tab.Multiline = true;
            this.Tab.Name = "Tab";
            this.Tab.SelectedIndex = 0;
            // 
            // JobDetailTab
            // 
            this.JobDetailTab.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.JobDetailTab.BackColor = System.Drawing.Color.Transparent;
            this.JobDetailTab.Controls.Add(this.JobDetailTextBox);
            resources.ApplyResources(this.JobDetailTab, "JobDetailTab");
            this.JobDetailTab.Name = "JobDetailTab";
            // 
            // EmployerDetailTab
            // 
            resources.ApplyResources(this.EmployerDetailTab, "EmployerDetailTab");
            this.EmployerDetailTab.Name = "EmployerDetailTab";
            this.EmployerDetailTab.UseVisualStyleBackColor = true;
            // 
            // Window
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.Tab);
            this.Controls.Add(this.NextJobBtn);
            this.Controls.Add(this.PrevJobBtn);
            this.Name = "Window";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.Window_Load);
            this.Tab.ResumeLayout(false);
            this.JobDetailTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PrevJobBtn;
        private System.Windows.Forms.Button NextJobBtn;
        private System.Windows.Forms.RichTextBox JobDetailTextBox;
        private System.Windows.Forms.TabControl Tab;
        private System.Windows.Forms.TabPage EmployerDetailTab;
        public System.Windows.Forms.TabPage JobDetailTab;
    }
}

