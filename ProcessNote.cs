using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace ProcessNote
{
    public partial class ProcessNote : Form
    {
        public Process[] Processes { get; set; }
        public Dictionary<Process, string> ProcessComments { get; set; }

        public ProcessNote()
        {
            InitializeComponent();
            Processes = Process.GetProcesses();
            ProcessComments = new Dictionary<Process, string>();
        }

        private void ProcessNote_Load(object sender, EventArgs e)
        {
            foreach (Process process in Processes)
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

            if (textBox1.Text != null)
            {
                textBox1.Clear();
            }

            Process process = GetSelectedProcess();

            if (process != null)
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true);
                PerformanceCounter ramCounter = new PerformanceCounter("Process", "Private Bytes", process.ProcessName, true);
                double cpu = Math.Round(cpuCounter.NextValue() / Environment.ProcessorCount, 2);
                double ram = Math.Round(ramCounter.NextValue() / 1024 / 1024, 2);

                DateTime startTime = process.StartTime;
                TimeSpan runningTime = DateTime.Now - startTime;

                string[] row = new string[] { cpu + "%", ram + " MB", runningTime.ToString(), startTime.ToString() };
                listView1.Items.Add(new ListViewItem(row));

                foreach (Process p in ProcessComments.Keys)
                {
                    if (process == p)
                    {
                        textBox1.Text = ProcessComments[p];
                    }
                }
            }
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (!button1.ContainsFocus)
            {
                Form warning = new Warning("Your comment is not saved yet!");
                warning.ShowDialog();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Process process = GetSelectedProcess();

            if (textBox1.Text != null && process != null)
            {
                if (!ProcessComments.ContainsKey(process))
                {
                    ProcessComments.Add(process, textBox1.Text);
                } else
                {
                    ProcessComments[process] = textBox1.Text;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        { 
            Process selectedProcess = GetSelectedProcess();
            Form threadsList = new ThreadsList(selectedProcess);
            threadsList.ShowDialog();
        }

        private Process GetSelectedProcess()
        {
            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0] != null)
            {
                foreach (Process process in Processes)
                {
                    if (process.Id.ToString() == listView.SelectedItems[0].Text)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
    }
}
