using ChessServer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
        public Point GetOldPiecePos()
        {
            return oldPiecePosition;
        }
        public void SetOldPiecePos(Point oldPos)
        {
            oldPiecePosition = oldPos;
        }
        public Point GetNewPiecePos()
        {
            return newPiecePosition;
        }
        public void SetNewPiecePos(Point newPos)
        {
            newPiecePosition= newPos;
        }
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

                        Console.WriteLine("Received from client:" + data);

                        int number;
                        if (int.TryParse(data, out number))
                        {
                            SetPiecePosition(number);
                            Moved = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        break;
                    }
                }
            }
            Console.WriteLine("Client disconnected...");
            tcpClient.Close();
        }
        private void SetPiecePosition(int number)
        {
            int y = number % 10;
            number /= 10;
            int x=number % 10;
            SetNewPiecePos(new Point(x,y));
            number /= 10;
            y=number % 10;
            number /= 10;
            x=number % 10;
            SetOldPiecePos(new Point(x, y));
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

        public void GeneratePiecesValidMoves()
        {
            foreach (var piece in pieces)
            {
                piece.GenerateValidMoves();
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

        public void ShowPieces()
        {
            pieces.ForEach(p => { Console.WriteLine(p.ToString()); });
        }
        public void RemovePiece(Piece piece)
        {
            pieces.Remove(piece);
        }
        #endregion
        public bool Check(Point kingPos, Player opponentPlayer, BoardMatrix matrix, Piece pieceFromImage, Point destPos=default )
        {
            int[,] initialMatrix = new int[8, 8];                  
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    initialMatrix[i, j] = matrix.allPieces[i, j];
                }
            }
            Point oldPosition = pieceFromImage.GetPiecePosition();
            
            int check = 0;
            pieceFromImage.SetPosition(destPos); 

            if (matrix.allPieces[destPos.X,destPos.Y]!=0)
            {
                foreach(Piece piece in opponentPlayer.pieces)
                {
                    if (piece.GetPiecePosition() == destPos)
                        piece.SetPosition(default);
                }
            }
            matrix.MUpdateOldPos(oldPosition);
            matrix.MInitPieces(this,opponentPlayer);
          
            if (pieceFromImage.GetPieceName() == PieceNames.king)
            {
                kingPos = destPos;
            }
            foreach(Piece piece in opponentPlayer.GetPieces())
            {
                if (piece.KingPosIsValidMove(kingPos))
                    check = 1;
            }


            pieceFromImage.SetPosition(oldPosition);            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    matrix.allPieces[i, j] =initialMatrix [i, j];
                }
            }                      

            return check==1?true:false;
            
        }

        internal bool VeryfiMove(Player opponentPlayer,BoardMatrix boardMatrix)
        {
            Piece selectedPiece = GetPieceFromPos(oldPiecePosition);
            if(selectedPiece != null &&
                selectedPiece.ValidMove(newPiecePosition))
            {
                if (Check(GetKingPoint(), opponentPlayer,boardMatrix, selectedPiece, newPiecePosition))
                {
                    return false;
                }
                if (boardMatrix.MSquareIsOppositePiece(newPiecePosition, playerColorOfPieces))
                {
                    Piece pieceToRemove = GetPieceFromPos(newPiecePosition);
                    opponentPlayer.RemovePiece(pieceToRemove);
                }

                boardMatrix.MUpdateOldPos(selectedPiece.GetPiecePosition());
                selectedPiece.SetPosition(GetNewPiecePos());
                boardMatrix.MInitPieces(this, opponentPlayer);
                boardMatrix.MShow();
                return true;
            }
            return false;
        }

        private Piece GetPieceFromPos(Point pos)
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

        internal void WriteCurrentMove(string move)
        {
            writer.WriteLine(move);
        }
    }
}
