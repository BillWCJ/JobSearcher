using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApplication
{
    public partial class JobDetailBrowser : Form
    {
        Jobs.Job[] jobs { get; set; }
        int current = 0;

        private void DisplayCurrentJobDetail()
        {
            JobDetailRichTextBox.Text = jobs[current].ToString("f");
        }

        public JobDetailBrowser()
        {
            InitializeComponent();
            jobs = ContentProcess.ContentExtraction.ReadInJobFromLocal();
            DisplayCurrentJobDetail();
        }

        private void PreviousJobButton_Click(object sender, EventArgs e)
        {
            if (--current < 0)
                current = 0;
            DisplayCurrentJobDetail();
        }

        private void NextJobButton_Click(object sender, EventArgs e)
        {
            if (++current >= jobs.Length)
                current = jobs.Length - 1;
            DisplayCurrentJobDetail();
        }
    }
}
