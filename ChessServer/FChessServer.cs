using Chess;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChessServer
{
    public partial class FChessServer : Form
    {
        public TcpListener server;
        bool workThread;

        Player[] players = new Player[100]; 
        public static int playerNumber;
        Colors playerColorOfPieces = Colors.white;

        public FChessServer()
        {
            InitializeComponent();
            server = new TcpListener(IPAddress.Any, 3001);
            server.Start();

            playerNumber = 0;

            Thread t = new Thread(new ThreadStart(ListenerForIncomingClients));
            workThread = true;
            t.Start();
            
        }
        public void ListenerForIncomingClients()
        {
            while (workThread)
            {
                TcpClient client = server.AcceptTcpClient();
                Player player=new Player(playerColorOfPieces,client);
                playerNumber++;

                players[playerNumber] = player;
                SwitchPlayerColor();

                if (playerNumber == 2)
                {
                    new GameLogic(players[1], players[2]);
                }
            }
        }
        private void SwitchPlayerColor()
        {
            if (playerColorOfPieces == Colors.black)
                playerColorOfPieces = Colors.white;
            else playerColorOfPieces = Colors.black;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
