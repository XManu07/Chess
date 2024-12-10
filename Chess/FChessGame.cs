using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Chess
{
    public partial class FChessGame : Form

    {
        public enum Colors { white = 1, black = -1 };
        Board board;
        Colors playerColor;

        public TcpClient client;
        public NetworkStream clientStream;
        public bool ascult;
        public Thread t;
        public FChessGame()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1", 3000);
            ascult = true;

            t = new Thread(new ThreadStart(ClientListener));
            t.Start();
            clientStream = client.GetStream();
            GetPlayerColor();
            Console.WriteLine("am ajuns aici");
            board = new Board(chessBoard,playerColor);
        }

        public void GetPlayerColor()
        {
            StreamReader reader = new StreamReader(clientStream);
            string color=reader.ReadLine();
            Console.WriteLine("color from server is :" + color);
            if (color == "black")
            {
                playerColor= Colors.black;
            }
            else
            {
                playerColor= Colors.white;
            }
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void FChessGame_Load(object sender, EventArgs e)
        {

        }

        #region resize
        private void FChessGame_Resize(object sender, EventArgs e) {
            AdjustChessBoardSize();
        } 
        private void AdjustChessBoardSize() 
        { 
            int size = Math.Min(this.ClientSize.Width, this.ClientSize.Height); 
            chessBoard.Size = new Size(size, size);
            chessBoard.Location = new Point( (this.ClientSize.Width - size) / 2, (this.ClientSize.Height - size) / 2 ); 
        }
        #endregion

        private void ClientListener()
        {
            StreamReader read= new StreamReader(clientStream);
            StreamWriter writer=new StreamWriter(clientStream);
            String clientData;
            ascult = true;
            while (ascult)
            {
                clientData=read.ReadLine();
                Console.WriteLine("Data from server is :"+clientData);
            }
        }
    }
}
