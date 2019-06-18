using System;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class Warning : Form
    {
        public string Message { get; set; }

        public Warning(string message)
        {
            InitializeComponent();
            Message = message;
        }

        private void Warning_Load(object sender, EventArgs e)
        {
            label1.Text = Message;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
