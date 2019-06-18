using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class ThreadsList : Form
    {
        public Process Process { get; set; }
        public ProcessThreadCollection Threads { get; set; }

        public ThreadsList(Process process)
        {
            InitializeComponent();
            this.Process = process;
            this.Threads = process.Threads;
        }

        private void ThreadsList_Load(object sender, EventArgs e)
        {
            foreach (ProcessThread thread in Threads)
            {
                listBox1.Items.Add(thread.Id);
            }
        }
    }
}
