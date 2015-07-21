using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Client
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public delegate void SendMessageHandler(string command, string message);
        public event SendMessageHandler OnSendMessage;
        private void startgame_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Interaction.InputBox("", "Nhập Tên Người Chơi");
                if (OnSendMessage != null)
                    OnSendMessage("SIGNUPREQUEST", username);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Thông Báo");
            }
        }

        private void readme_Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Chưa Implement Cái Chức Năng Này", "Thông Báo");
        }

        private void exit_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
