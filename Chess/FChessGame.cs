using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Chess
{
    public partial class FChessGame : Form

    {
        public enum Colors { white = 1, black = -1 };

        private Board board;

        private TcpClient client;
        private bool ascult;
        private Thread t;

        private NetworkStream clientStream;
        private StreamWriter writer;
        private StreamReader reader;

        public FChessGame()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1", 3001); //to be modified with server ipadress
            ascult = true;

            clientStream = client.GetStream();
            writer= new StreamWriter(clientStream);
            writer.AutoFlush = true;
            reader = new StreamReader(clientStream);

            Colors playerColor=GetPlayerColor();
            board = new Board(chessBoard,playerColor);
            board.Writer=writer;

            t = new Thread(new ThreadStart(ClientListener));
            t.Start();

            AdjustChessBoardSize();

            SetPbPlayerColor(playerColor);
        }

        private void SetPbPlayerColor(Colors playerColor)
        {
            if (playerColor == Colors.white)
                pbPlayerColor.BackColor = Color.Gold;
            else pbPlayerColor.BackColor = Color.Black;
        }
        public Colors GetPlayerColor()
        {
            string color=reader.ReadLine();
            Console.WriteLine("color from server is :" + color);
            if (color == "black")
            {
                return Colors.black;
            }
            else
            {
                return Colors.white;
            }
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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

        #region Listener
        private void ClientListener()
        {
            while (ascult)
            {
                string clientData =reader.ReadLine();

                if (clientData == null) break;

                if(clientData == "true")
                {
                    HandleCaseTrue();
                }

                if(int.TryParse(clientData,out int number))
                {
                    HandleCaseNumber(number);
                }

                if (clientData == "CheckMate")
                {
                    HandleCheckMate();
                }

                if (clientData[0] == 'm')//m from move
                {
                    string points = clientData.Substring(1);
                    HandlePoints(points);
                }

                Console.WriteLine("Data from server is :"+clientData);
            }
        }



        private void HandlePoints(string points)
        {
            for(int plen=points.Length-1; plen>0; plen-=2) 
            {
                int y = points[plen]-48;
                int x = points[plen-1]-48;
                board.LValidMoves.Add(new Point(x, y));
            }
            MethodInvoker m = new MethodInvoker(() => board.ShowGreenCircles());
            Invoke(m);
        }

        public void HandleCaseTrue()
        {
            MethodInvoker m = new MethodInvoker(() => { board.DeleteGreenCircles();board.LValidMoves.Clear(); board.UpdatePieceImage(); }) ;
            this.Invoke(m);
        }

        public void HandleCaseNumber(int number)
        {
            Point O_OldPiecePos = default;
            Point O_NewPiecePos = default;
            O_NewPiecePos.Y = number % 10;
            number = number / 10;
            O_NewPiecePos.X = number % 10;
            number /= 10;
            O_OldPiecePos.Y = number % 10;
            number /= 10;
            O_OldPiecePos.X = number % 10;
            MethodInvoker m = new MethodInvoker(() => board.UpdateOponnentImage(O_OldPiecePos, O_NewPiecePos));
            this.Invoke(m);
        }

        public void HandleCheckMate()
        {
            ascult = false;
            Console.WriteLine("Game Over");
            reader.Close();
            writer.Close();
            clientStream.Close();
            t.Abort();
            client.Close();
        }
        #endregion
    }
}
