using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProcessNote
{
    public partial class ProcessNote : Form
    {
        public ProcessNote()
        {
            InitializeComponent();
        }

        private Process[] processes = Process.GetProcesses();

        private void ProcessNote_Load(object sender, EventArgs e)
        {
            foreach (Process process in processes)
            {
                string[] row = new string[] { process.Id.ToString(), process.ProcessName };
                listView.Items.Add(new ListViewItem(row));
            }
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                listView1.Items.RemoveAt(0);
            }

            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0] != null)
            {
                string id = listView.SelectedItems[0].Text;

                foreach (Process process in processes)
                {
                    if (process.Id.ToString() == id)
                    {
                        PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true);
                        PerformanceCounter ramCounter = new PerformanceCounter("Process", "Private Bytes", process.ProcessName, true);
                        double cpu = Math.Round(cpuCounter.NextValue() / Environment.ProcessorCount, 2);
                        double ram = Math.Round(ramCounter.NextValue() / 1024 / 1024, 2);

                        DateTime startTime = process.StartTime;
                        TimeSpan runningTime = DateTime.Now - startTime;

                        string[] row = new string[] { cpu + "%", ram + " MB", runningTime.ToString(), startTime.ToString()};
                        listView1.Items.Add(new ListViewItem(row));
                    }
                }

                // TODO: comment
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            // Warning popup for unsaved comment
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // save comment in memory
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Process process in processes)
            {   
                if (process.Id.ToString() == listView.SelectedItems[0].Text)
                {
                    Process selectedProcess = process;
                    Form threadsList = new ThreadsList(selectedProcess);
                    threadsList.ShowDialog();
                }
            }
        }
    }
}
