using ChessServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace Chess
{
    internal class Player
    {
        private Colors playerColorOfPieces;
        private List<Piece> pieces;

        TcpClient tcpClient;
        bool workThread = true;

        private Point oldPiecePosition;
        private Point newPiecePosition;

        NetworkStream streamServer;
        StreamReader reader;
        StreamWriter writer;

        #region Set,Get 
        public Point OldPiecePosition { get => oldPiecePosition; set => oldPiecePosition = value; }
        public Point NewPiecePosition { get => newPiecePosition; set => newPiecePosition = value; }
        public Colors PlayerColorOfPieces { get => playerColorOfPieces; set => playerColorOfPieces = value; }
        public Colors GetPlayerColor()
        {
            return playerColorOfPieces;
        }
        public List<Piece> GetPieces()
        {
            return pieces;
        }
        public Point GetKingPoint()
        {
            foreach (Piece piece in pieces)
            {
                if (piece.GetPieceName() == PieceNames.king)
                    return piece.GetPiecePosition();
            }
            return new Point(-1,-1);
        }
        #endregion
        #region Get Line from color
        public int GetPiecesLine()
        {
            return playerColorOfPieces == Colors.black ? 7 : 0;
        }
        public int GetPawnLine()
        {
            int fline=GetPiecesLine();
            return fline== 0 ? fline + 1 : fline - 1;
        }
        #endregion

        public bool Moved = false;

        public Player(Colors color,TcpClient client)
        {
            this.playerColorOfPieces = color;
            tcpClient=client;
            InitPieces();

            streamServer=tcpClient.GetStream();
            writer= new StreamWriter(streamServer);
            reader=new StreamReader(streamServer);

            Console.WriteLine("Client connected ...");
            Task.Run(() => HandleClientAsync(tcpClient));
        }


        private async Task HandleClientAsync(TcpClient tcpClient)
        {
            using ( streamServer = tcpClient.GetStream())
            using ( reader = new StreamReader(streamServer))
            using ( writer = new StreamWriter(streamServer))
            {
                writer.AutoFlush = true;
                writer.WriteLine(playerColorOfPieces);
                while (workThread)
                {
                    try
                    {
                        string data = await reader.ReadLineAsync();
                        if (data == null) break;

                        if (data[0] == 'p')     //p from piece
                        {
                            HandleSendValidMoveList(data);
                        }

                        int number;
                        if (int.TryParse(data, out number))
                        {
                            HandlePiecePosition(number);
                        }
                        
                        Console.WriteLine("Received from client:" + data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        break;
                    }
                }
            }
            FChessServer.playerNumber --;
            Console.WriteLine("Client disconnected...");
            tcpClient.Close();
        }

        private void HandleSendValidMoveList(string data)
        {
            int position;
            int y;
            if (int.TryParse(data.Substring(1),out position))
            {
                y = position % 10;
                oldPiecePosition.Y = y;
                position/=10;

                oldPiecePosition.X = position;
            }
            Piece currentPiece = GetPieceFromPos(oldPiecePosition);
            writer.WriteLine("m" + currentPiece.ListMovesToString()); //m from moves
        }

        private void HandlePiecePosition(int number)
        {
            int y = number % 10;
            number /= 10;
            int x=number % 10;
            NewPiecePosition=new Point(x,y);
            number /= 10;
            y=number % 10;
            number /= 10;
            x=number % 10;
            OldPiecePosition = new Point(x, y);
            
            Moved = true;
        }


        #region Pieces
        void InitPieces()
        { 
            pieces = new List<Piece>
            {
                new Rook(playerColorOfPieces, new Point(GetPiecesLine(), 0)),
                new Knight(playerColorOfPieces, new Point(GetPiecesLine(), 1)),
                new Bishop(playerColorOfPieces, new Point(GetPiecesLine(), 2)),
                new Queen(playerColorOfPieces, new Point(GetPiecesLine(), 3)),
                new King(playerColorOfPieces, new Point(GetPiecesLine(), 4)),
                new Bishop(playerColorOfPieces, new Point(GetPiecesLine(), 5)),
                new Knight(playerColorOfPieces, new Point(GetPiecesLine(), 6)),
                new Rook(playerColorOfPieces, new Point(GetPiecesLine(), 7)),

            };
            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new Pawn(playerColorOfPieces,new Point(GetPawnLine(),i)));
            }
        }

        public bool HasValidMoves()
        {
            foreach (var piece in pieces)
            {
                if (piece.GetLValidMoves() != null)
                    return true;
            }
            return false;
        }
        public void RemovePiece(Piece piece)
        {
            pieces.Remove(piece);
        }
        #endregion
     
        public Piece GetPieceFromPos(Point pos)
        {
            foreach(Piece piece in pieces)
            {
                if (piece.GetPiecePosition()==pos) return piece;
            }
            return null;
        }

        internal void WriteGoodMove()
        {
            writer.WriteLine("true");
            Console.WriteLine("am scris");
        }

        internal void WriteCurrentPlayerMove(string move)
        {
            writer.WriteLine(move);
        }

        internal void ClearLValidMoves()
        {
            foreach (var piece in pieces)
                piece.GetLValidMoves().Clear();
        }
    }
}
