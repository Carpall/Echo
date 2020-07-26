using System;
using System.Windows.Forms;
using Echo;

namespace EchoGraphics
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Manager.Server.Start();
            Update.Start();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            using (EchoSettings ss = new EchoSettings()) {
                ss.ShowDialog();
            }
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            Manager.Server.ReceiveMessage();
            string[] split = Manager.Server.Messages.Split('⚹');
            try {
                for (int i = 0; i < split.Length; i++) {
                    AllMessagesBox.Text += split[i].Substring(split[i].IndexOf("[m]"))+'\n';
                }
            } catch (ArgumentOutOfRangeException) {}
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TypeMessageBox.Text)) {
                Manager.Server.SendMessage("[m]" + TypeMessageBox.Text + "⚹");
                TypeMessageBox.Text = "";
            }
        }
    }
    class Manager
    {
        public static Host Server = new Host();
        public static Package Echo = new Package();
    }
}