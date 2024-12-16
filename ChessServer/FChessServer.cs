using Chess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessServer
{
    public partial class FChessServer : Form
    {
        public TcpListener server;
        bool workThread;

        Player[] players = new Player[100]; 
        int playerNumber;
        Colors playerColorOfPieces = Colors.white;
        public FChessServer()
        {
            InitializeComponent();
            server = new TcpListener(System.Net.IPAddress.Any, 3001);
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
                Console.WriteLine("Player number is: " + playerNumber);
                Console.WriteLine("Player color is: " + playerColorOfPieces);
                players[playerNumber] = player;
                switchPlayerColor();

                if (playerNumber == 2)
                {
                    new GameLogic(players[1], players[2]);
                }
            }
        }

        private void switchPlayerColor()
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
