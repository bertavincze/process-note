using System;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class Warning : Form
    {
        string Message;

        public Warning(string message)
        {
            InitializeComponent();
            Message = message;
        }

        private void Warning_Load(object sender, EventArgs e)
        {
            label1.Text = Message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
