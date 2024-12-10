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
        Colors playerColor = Colors.black;

        public FChessServer()
        {
            InitializeComponent();
            server = new TcpListener(System.Net.IPAddress.Any, 3000);
            server.Start();

            Thread t = new Thread(new ThreadStart(Server_Listener));
            workThread = true;
            t.Start();
        }

        public void Server_Listener()
        {
            while (workThread)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");

                Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (NetworkStream streamServer = client.GetStream())
            using (StreamReader reader = new StreamReader(streamServer))
            using (StreamWriter writer = new StreamWriter(streamServer))
            {
                writer.AutoFlush = true;
                writer.WriteLine(playerColor+"\n");
                switchPlayerColor();
                int i = 1;
                while (workThread)
                {
                    try
                    {
                        string serverData = await reader.ReadLineAsync();
                        if (serverData == null) break;

                        Console.WriteLine("Received from client " + serverData + " " + i++);

                        await writer.WriteLineAsync("Server: " + serverData);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        break;
                    }
                }
            }
            Console.WriteLine("Client disconnected...");
            client.Close();
        }

        private void switchPlayerColor()
        {
            if (playerColor==Colors.black)
                playerColor=Colors.white;
            else playerColor=Colors.black;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
