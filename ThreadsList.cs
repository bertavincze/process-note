using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class ThreadsList : Form
    {
        Process process;
        ProcessThreadCollection threads;

        public ThreadsList(Process process)
        {
            InitializeComponent();
            this.process = process;
            this.threads = process.Threads;
        }

        private void ThreadsList_Load(object sender, EventArgs e)
        {
            foreach (ProcessThread thread in threads)
            {
                listBox1.Items.Add(thread.Id);
            }
        }
    }
}
