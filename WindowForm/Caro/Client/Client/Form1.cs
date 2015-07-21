using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace Client
{
    public partial class mainForm : Form
    {
        private MainMenu menu;
        private Play play;
        private Connector connector;
        public mainForm()
        {
            this.InitializeComponent();
            this.Build();
            this.Bind();
        }

        private void Build()
        {
            this.connector = new Connector(server_TextBox.Text);
            this.menu = new MainMenu();
            this.play = new Play();
        }

        private void Bind()
        {
            main_Panel.Controls.Add(this.menu);
            this.menu.OnSendMessage += this.connector.SendMessage;
            this.play.OnSendMessage += this.connector.SendMessage;
            this.play.OnBackToMenu += this.ChangeState;
            this.connector.OnSignupSuccess += this.ChangeState;
            this.connector.OnOrdinal += this.play.PlayNow;
            this.connector.OnPut += this.play.PutToBoard;
            this.connector.OnShowMessage += this.ShowMessage;
            this.connector.OnBackMenu += this.ChangeState;
            this.connector.OnEndGame += this.play.EndGame;
        }

        private void ShowMessage(string title, string message)
        {
            MessageBox.Show(this, message, title);
        }

        private void ChangeState(string content = null)
        {
            main_Panel.Invoke(new MethodInvoker(delegate()
                {
                    if (main_Panel.Controls.Contains(this.menu))
                    {
                        this.play.Reset(content);
                        main_Panel.Controls.Remove(this.menu);
                        main_Panel.Controls.Add(this.play);
                    }
                    else
                    {
                        main_Panel.Controls.Remove(this.play);
                        main_Panel.Controls.Add(this.menu);
                    }
                }
            ));
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult check = MessageBox.Show(this, "WARNING", "Bạn Chắc Chắn Muốn Thoát Chứ ?", MessageBoxButtons.YesNo);
            if (check == DialogResult.No)
                e.Cancel = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                "1312467 : Võ Minh Quốc\r\n1312444 : Võ Như Phúc",
                "Thông Tin Về Nhóm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }

    public class Connector
    {
        private Socket socket;
        private Interpreter interpreter;
        private Thread thread;

        public Connector(string server)
        {
            this.Build(server);
        }

        private void Build(string server)
        {
            try
            {
                string[] temp = server.Split(':');
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint serverendpoint = new IPEndPoint(Dns.Resolve(temp[0]).AddressList[0], int.Parse(temp[1]));
                this.socket.Connect(serverendpoint);
                this.interpreter = new Interpreter(this.socket);
                this.thread = new Thread(this.Listen);
                this.thread.IsBackground = true;
                this.thread.Name = "Receiver";
                this.thread.Start();
            }
            catch(Exception ex)
            {
                throw new Exception("Đã Đủ Người Chơi. Bạn Không Thể Vào Phòng");
            }
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    if (this.socket.Available > 0)
                        this.ReceiveMessage();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Có Lỗi Xảy Ra Ở Hàm Listen Lớp Connector");
            }
        }

        public void SendMessage(string command, string message)
        {
            try
            {
                this.interpreter.SendMessage(command, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public delegate void SignupSuccessHandler(string username);
        public event SignupSuccessHandler OnSignupSuccess;

        public delegate void ShowMessageHandler(string title, string message);
        public event ShowMessageHandler OnShowMessage;

        public delegate void OrdinalHandler(int ordinal);
        public event OrdinalHandler OnOrdinal;

        public delegate void PutHandler(int row, int col);
        public event PutHandler OnPut;

        public delegate void BackMenuHandler(string content);
        public event BackMenuHandler OnBackMenu;

        public delegate void EndGameHandler(string content);
        public event EndGameHandler OnEndGame;
        public void ReceiveMessage()
        {
            try
            {
                this.interpreter.ReceiveMessage();
                switch (this.interpreter.CommandType)
                {
                    case Command.LOGINSUCCESS:
                        OnSignupSuccess((string)this.interpreter.Content);
                        break;

                    case Command.LOGINFAILED:
                        OnShowMessage("Lỗi Đặt Tên", "Vui Lòng Đặt Lại Bằng Tên Khác");
                        break;
                    case Command.ORDINAL:
                        OnOrdinal((int)this.interpreter.Content);
                        break;

                    case Command.PUT:
                        string[] temp = (this.interpreter.Content as string).Split(',');
                        OnPut(int.Parse(temp[0]), int.Parse(temp[1]));
                        break;

                    case Command.WIN:
                        OnEndGame("BẠN ĐÃ THẮNG");
                        break;

                    case Command.LOST:
                        OnEndGame("BẠN ĐÃ THUA");
                        break;

                    case Command.OTHEROUT:
                        OnShowMessage("Thông Báo", "Người Chơi Khác Đã Thoát Game. Bấm OK để tự động thoát");
                        OnBackMenu(null);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public enum Command {LOGINSUCCESS, LOGINFAILED, ORDINAL, PUT, WIN, LOST, OTHEROUT};
    public class Interpreter
    {
        private Socket parrentsocket;
        public Command CommandType
        { get; private set; }
        public object Content
        { get; private set; }
        public Interpreter(Socket server)
        {
            this.Build(server);
        }

        private void Build(Socket server)
        {
            this.parrentsocket = server;
        }

        public bool SendMessage(string command, string message)
        {
            bool result = false;
            try
            {
                string sendstring = string.Format("{0}|{1}", command.ToUpper(), message);
                NetworkStream networkstream = new NetworkStream(this.parrentsocket);
                using (StreamWriter streamwriter = new StreamWriter(networkstream, Encoding.ASCII))
                {
                    streamwriter.Write(sendstring + (char)1);
                }
                networkstream.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Có Lỗi Xảy Ra Ở Hàm SendMessage Lớp Interpreter");
            }
            return result;
        }

        public bool ReceiveMessage()
        {
            bool result = false;
            try
            {
                const char end = (char)(1);
                NetworkStream networkstream = new NetworkStream(this.parrentsocket);
                MemoryStream memorystream = new MemoryStream();
                byte b = (byte)networkstream.ReadByte();
                while (b != end)
                {
                    memorystream.WriteByte(b);
                    b = (byte)networkstream.ReadByte();
                }
                memorystream.Position = 0;
                StreamReader streamreader = new StreamReader(memorystream, Encoding.ASCII);
                string receivestring = streamreader.ReadToEnd();
                this.Parse(receivestring);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Có Lỗi Xảy Ra Ở Hàm ReceiveMessage Lớp Interpreter");
            }
            return result;
        }

        private void Parse(string content)
        {
            string[] temp = content.Split('|');
            switch(temp[0].ToUpper())
            {
                case "ORDINAL":
                    this.CommandType = Command.ORDINAL;
                    this.Content = int.Parse(temp[1]);
                    break;
                
                case "LOGINSUCCESS":
                    this.CommandType = Command.LOGINSUCCESS;
                    this.Content = temp[1];
                    break;
                
                case "LOGINFAILED":
                    this.CommandType = Command.LOGINFAILED;
                    break;

                case "PUT":
                    this.CommandType = Command.PUT;
                    string temp1 = temp[1].Substring(1, temp[1].Length - 2);
                    this.Content = temp1;
                    break;

                case "WIN":
                    this.CommandType = Command.WIN;
                    break;

                case "LOST":
                    this.CommandType = Command.LOST;
                    break;

                case "EXITGAME":
                    this.CommandType = Command.OTHEROUT;
                    break;

                default:
                    break;

            }
        }
    }
}
