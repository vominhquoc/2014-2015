using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace Client
{
    public partial class Play : UserControl
    {
        private UserBoard userplay;

        public Play()
        {
            InitializeComponent();
            this.Build();
            
        }

        public void Build()
        {
            this.userplay = new UserBoard(board_Panel.Width, board_Panel.Height);
        }

        public void Reset(string username)
        {
            username_Label2.Text = username;
            this.userplay.playing = false;
            status_Label.Visible = false;
            wait_Label.Visible = true;
            this.userplay.ResetBoard();
            OnSendMessage("ORDINAL", "");
        }

        public void PlayNow(int ordinal)
        {
            board_Panel.Invoke(new MethodInvoker(delegate()
                {
                    wait_Label.Visible = false;
                    if (ordinal == 1)
                    {
                        this.userplay.ismyordinal = true;
                        this.userplay.currentplayer = Player.Player1;
                    }
                    else
                    {
                        this.userplay.ismyordinal = false;
                        this.userplay.currentplayer = Player.Player2;
                    }
                    this.userplay.playing = true;
                }
            ));
        }

        public delegate void SendMessageHandler(string command, string message);
        public event SendMessageHandler OnSendMessage;

        public delegate void BackToMenuHandler(string content);
        public event BackToMenuHandler OnBackToMenu;
        private void back_Button_Click(object sender, EventArgs e)
        {
            DialogResult check = DialogResult.Yes;
            if (this.userplay.playing)
            {
                check = MessageBox.Show(this, "Bạn Chắc Muốn Thoát Ván Đang Chơi Chứ ?", 
                    "WARNING",
                    MessageBoxButtons.YesNo);
            }
            if (check == DialogResult.Yes && OnBackToMenu != null)
            {
                OnSendMessage("OUT", "");
                OnBackToMenu(null);
            }
        }

        public void EndGame(string content)
        {
            this.userplay.playing = false;
            board_Panel.Invoke(new MethodInvoker(delegate()
                {
                    status_Label.Text = content;
                    status_Label.Visible = true;
                }));
        }

        public void PutToBoard(int row, int col)
        {
            board_Panel.Invoke(new MethodInvoker(delegate()
                {
                    this.userplay.board[row, col] = (this.userplay.currentplayer == Player.Player1) ? Player.Player2 : Player.Player1;
                    this.userplay.ismyordinal = true;
                    this.board_Panel.Refresh();
                }));
        }

        private void board_Panel_Paint(object sender, PaintEventArgs e)
        {
                this.userplay.DrawBoard(e.Graphics);
        }

        private void board_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (this.userplay.playing && this.userplay.ismyordinal))
            {
                bool check = this.userplay.HandleEvent(e);
                if (check)
                { 
                    OnSendMessage("PUT", string.Format("({0},{1})", e.Y/20 + 1, e.X/20 + 1));
                    this.userplay.ismyordinal = false;
                    this.board_Panel.Refresh();
                }
            }
        }
    }

    public enum Player { None, Player1, Player2 };
    public class UserBoard
    {
        private Bitmap boardsymbol;
        private Bitmap player1symbol;
        private Bitmap player2symbol;
        private int width;
        private int height;
        private int offsetw;
        private int offseth;
        public Player currentplayer;
        public Player[,] board;
        public bool ismyordinal;
        public bool playing;
        public UserBoard(int width, int height)
        {
            this.width = width / 20;
            this.height = height / 20;
            this.board = new Player[this.height + 2, this.width + 2];
            this.Build();
        }

        public void Build()
        {
            this.boardsymbol = new Bitmap(Properties.Resources.bmpNone);
            this.player1symbol = new Bitmap(Properties.Resources.bmpHuman);
            this.player2symbol = new Bitmap(Properties.Resources.bmpMachine);
            this.offseth = 1;
            this.offsetw = 1;
            this.ResetBoard();
        }

        public void ResetBoard()
        {
            int r, c;
            for (r = 0; r < height + 2; r++)
            {
                for (c = 0; c < width + 2; c++)
                {
                    if (r == 0 || c == 0 || r == height + 1 || c == width + 1)
                        continue;
                    board[r, c] = Player.None;
                }
            }
        }

        public void DrawBoard(Graphics g)
        {
            Rectangle rect = new Rectangle();
            for (int r = offseth; r <= height + 1; r++)
            {
                for (int c = offsetw; c <= width + 1; c++)
                {
                    rect = new Rectangle((c - offsetw) * 20, (r - offseth) * 20, 20, 20);
                    if(board[r,c] == Player.None)
                        g.DrawImage(boardsymbol, rect);
                    else if(board[r,c] == Player.Player1)
                        g.DrawImage(player1symbol, rect);
                    else
                        g.DrawImage(player2symbol, rect);
                }
            }
        }

        public bool HandleEvent(MouseEventArgs e)
        {
            bool result = false;
            int r = (e.Y / 20) + offseth;
            int c = (e.X / 20) + offsetw;
            if (board[r, c] == Player.None)
            {
                board[r, c] = currentplayer;
                this.ismyordinal = false;
                result = true;
            }
            return result;
        }
    }
}
