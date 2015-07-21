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
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace Server
{
    public partial class Form1 : Form
    {
        
        Socket server;
        Thread listener;
        Thread handler;
        int ordinal;
        int width;
        int height;
        Player[,] board;

        List<User> listuser;
        public Form1()
        {
            InitializeComponent();
            this.Build();
        }

        private void Build()
        {
            this.width = 37;
            this.height = 21;
            this.listuser = new List<User>(2);
            this.board = new Player[height + 2, width + 2];
            Reset();
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
            this.server.Bind(new IPEndPoint(IPAddress.Any, 21));
            this.server.Listen(2);
            this.listener = new Thread(Listen);
            this.listener.IsBackground = true;
            this.listener.Start();
        }

        public Player CheckEnd(int rw, int cl)
        {
            bool Player1, Player2;
            int r = 1, c = 1;
            int i;

            while (c <= this.width - 4)
            {
                Player1 = true; Player2 = true;

                for (i = 0; i < 5; i++)
                {
                    if (board[rw, c + i] != Player.Player1)
                        Player1 = false;
                    if (board[rw, c + i] != Player.Player2)
                        Player2 = false;
                }

                if (Player1 && (board[rw, c - 1] != Player.Player2 || board[rw, c + 5] != Player.Player2)) return Player.Player1;
                if (Player2 && (board[rw, c - 1] != Player.Player1 || board[rw, c + 5] != Player.Player1)) return Player.Player2;
                c++;
            }

            while (r <= this.height - 4)
            {
                Player1 = true; Player2 = true;
                for (i = 0; i < 5; i++)
                {
                    if (board[r + i, cl] != Player.Player1)
                        Player1 = false;
                    if (board[r + i, cl] != Player.Player2)
                        Player2 = false;
                }
                if (Player1 && (board[r - 1, cl] != Player.Player2 || board[r + 5, cl] != Player.Player2)) return Player.Player1;
                if (Player2 && (board[r - 1, cl] != Player.Player1 || board[r + 5, cl] != Player.Player1)) return Player.Player2;
                r++;
            }

            r = rw; c = cl;
            while (r > 1 && c > 1) { r--; c--; }
            while (r <= this.height - 4 && c <= this.width - 4)
            {
                Player1 = true; Player2 = true;
                for (i = 0; i < 5; i++)
                {
                    if (board[r + i, c + i] != Player.Player1)
                        Player1 = false;
                    if (board[r + i, c + i] != Player.Player2)
                        Player2 = false;
                }
                if (Player1 && (board[r - 1, c - 1] != Player.Player2 || board[r + 5, c + 5] != Player.Player2)) return Player.Player1;
                if (Player2 && (board[r - 1, c - 1] != Player.Player1 || board[r + 5, c + 5] != Player.Player1)) return Player.Player2;
                r++; c++;
            }

            r = rw; c = cl;
            while (r < this.height && c > 1) { r++; c--; }
            while (r >= 5 && c <= this.width - 4)
            {
                Player1 = true; Player2 = true;
                for (i = 0; i < 5; i++)
                {
                    if (board[r - i, c + i] != Player.Player1)
                        Player1 = false;
                    if (board[r - i, c + i] != Player.Player2)
                        Player2 = false;
                }
                if (Player1 && (board[r + 1, c - 1] != Player.Player2 || board[r - 5, c + 5] != Player.Player2)) return Player.Player1;
                if (Player2 && (board[r + 1, c - 1] != Player.Player1 || board[r - 5, c + 5] != Player.Player1)) return Player.Player2;
                r--; c++;
            }

            return Player.None;
        }

        public void AppendText(string content)
        {
            if (InvokeRequired)
            {
                textBox1.Invoke(new MethodInvoker(delegate()
                    {
                        textBox1.Text += content + Environment.NewLine;
                    }
                ));
            }
            else
                textBox1.Text += content + Environment.NewLine;
        }

        public bool CheckError(string username)
        {
            if(username.Length > 10)
                return true;

            string pattern = @"[^A-Za-z0-9]+";
            if (Regex.IsMatch(username, pattern))
                return true;

            foreach (User user in listuser)
            {
                if (user.Username == username)
                    return true;
            }
            return false;
        }

        public void OtherPut(string username, int row, int col)
        {
            foreach (User user in listuser)
            {
                if (user.Username == username)
                    board[row, col] = user.player;
                else
                    user.Send("PUT", string.Format("({0},{1})", row, col));
            }

            Player winner = CheckEnd(row, col);
            if (winner != Player.None)
            {
                foreach (User user in listuser)
                {
                    if (user.player == winner)
                    {
                        user.Send("WIN", "");
                        AppendText(user.Username + " Đã Chiến Thắng");
                    }
                    else
                        user.Send("LOST", "");
                }
            }
        }

        public void CheckIfFullUser()
        {
            while (listuser.Count < 2)
            {
                Thread.Sleep(10);
            }
        }

        public void AddNewUser(User user)
        {
            user.StartGame = true;
            user.Ordinal = ordinal;
            this.listuser.Add(user);
            ordinal++;
        }

        public void ExitGame(string username)
        {
            foreach (User user in listuser)
            {
                if (user.Username != username)
                    user.Send("EXITGAME", "");
                AppendText(user.Username + " Đã Thoát");
            }
            listuser.Clear();
            AppendText("Game Đã Kết Thúc" + Environment.NewLine);
            Reset();
        }

        public void Reset()
        {
            ordinal = 1;
            AppendText("Đã Tạo Game Mới");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = Player.None;
                }
            }
        }
        public void Listen()
        {
            while (true)
            {
                User user =  new User(this.server.Accept());
                user.OnCheckError += CheckError;
                user.OnCheckFullUser += CheckIfFullUser;
                user.OnAddUser += AddNewUser;
                user.OnOtherPut += OtherPut;
                user.OnExitGame += ExitGame;
                user.OnAppendText += AppendText;
            }
        }
    }

    public enum Player { None, Player1, Player2 };
    public class User
    {
        private Socket socket;
        private Thread thread;
        public int Ordinal;
        public string Username;
        public bool StartGame;
        public Player player;
        public User(Socket socket)
        {
            this.socket = socket;
            this.Build();
        }

        public void Build()
        {
            this.thread = new Thread(this.Listen);
            this.thread.IsBackground = true;
            this.thread.Name = "Receiver";
            this.thread.Start();
            this.StartGame = false;
        }

        public delegate bool CheckErrorHandler(string username);
        public event CheckErrorHandler OnCheckError;

        public delegate void CheckFullUserHandler();
        public event CheckFullUserHandler OnCheckFullUser;

        public delegate void AddUserHandler(User user);
        public event AddUserHandler OnAddUser;

        public delegate void AppendTextHandler(string content);
        public event AppendTextHandler OnAppendText;

        public delegate void OtherPutHandler(string username, int row, int col);
        public event OtherPutHandler OnOtherPut;

        public delegate void ExitGameHandler(string username);
        public event ExitGameHandler OnExitGame;
        public void Listen()
        {
            try
            {
                while (true)
                {
                    if (socket.Available > 0)
                    {
                        string command = ReadRespone();
                        string[] temp = command.Split('|');
                        switch (temp[0].ToUpper())
                        { 
                            case "SIGNUPREQUEST":
                                bool check = OnCheckError(temp[1]);
                                if (!check)
                                {
                                    this.Username = temp[1];
                                    OnAddUser(this);
                                    Send("LOGINSUCCESS", temp[1]);
                                    OnAppendText(this.Username + " Đã Vào Phòng");
                                }
                                else
                                {
                                    Send("LOGINFAILED", temp[1]);
                                    OnAppendText(this.Username + " Đăng Kí Thất Bại");
                                }
                                break;
                            
                            case "ORDINAL":
                                OnCheckFullUser();
                                this.player = (Ordinal == 1) ? Player.Player1 : Player.Player2;
                                Send("ORDINAL", this.Ordinal.ToString());
                                OnAppendText(this.Username + " Đang Chơi Game");
                                break;

                            case "PUT":
                                string temp1 = temp[1].Substring(1, temp[1].Length - 2);
                                string[] temp2 = temp1.Split(',');
                                OnOtherPut(this.Username, int.Parse(temp2[0]), int.Parse(temp2[1]));
                                break;

                            case "OUT":
                                OnExitGame(this.Username);
                                break;

                            default:
                                break;
                        }
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            { 
                
            }
        }

        public string ReadRespone()
        {
            string result = null;
            NetworkStream networkstream = new NetworkStream(socket);
            MemoryStream memorystream = new MemoryStream();
            const char end = (char)(1);
            byte b = (byte)networkstream.ReadByte();
            while (b != end)
            {
                memorystream.WriteByte(b);
                b = (byte)networkstream.ReadByte();
            }
            memorystream.Position = 0;
            StreamReader streamreader = new StreamReader(memorystream, Encoding.ASCII);
            result = streamreader.ReadToEnd();
            streamreader.Close();
            return result;
        }

        public void Send(string command, string message)
        {
            string sendstr = string.Format("{0}|{1}", command, message);
            NetworkStream networkstream = new NetworkStream(socket);
            using (StreamWriter streamwriter = new StreamWriter(networkstream, Encoding.ASCII))
            {
                streamwriter.Write(sendstr + (char)1);
            }
        }
    }
}
