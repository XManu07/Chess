using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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

        public Colors GetPlayerColor()
        {
            return playerColorOfPieces;
        }
        public Point GetOpponentKingPoint()
        {
            foreach (Piece piece in pieces)
            {
                if (piece.GetPieceName() == PieceNames.king)
                    return piece.GetPiecePosition();
            }
            return new Point(-1,-1);
        }
        public Player(Colors color)
        {
            this.playerColorOfPieces = color;
            InitPieces();
        }

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
        public List<Piece> GetPieces()
        {
            return pieces;
        }
        public void ShowPieces()
        {
            pieces.ForEach(p => { Console.WriteLine(p.ToString()); });
        }
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

        #region Move function
        //public void Move(PictureBox image,Control destination,TableLayoutPanel chessBoard)
        //{
        //    Piece pieceFromImage=GetPieceFromImage(image,chessBoard);
        //    Point destinationPosition = getPointFromDestination(destination, chessBoard);
        //    if (pieceFromImage.ValidMove(destinationPosition))
        //    {
        //        destination = image;
        //    }
        //}   
        public void RemovePiece(Piece piece)
        {
            pieces.Remove(piece);
        }
        public Point getPointFromDestination(Control destination, TableLayoutPanel chessBoard)
        {
            Point destinationPosition = new Point();
            destinationPosition.X = chessBoard.GetPositionFromControl(destination).Row;
            destinationPosition.Y = chessBoard.GetPositionFromControl(destination).Column;
            return destinationPosition;
        }
        public Piece GetPieceFromImage(Panel image, TableLayoutPanel chessBoard)
        {
            foreach (var piece in pieces)
            {
                if (piece.GetPiecePosition().X == chessBoard.GetCellPosition(image).Row
                    && piece.GetPiecePosition().Y == chessBoard.GetCellPosition(image).Column)
                {
                    return piece;
                }
            }
            return null;
        }

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
        #endregion
        

    }
}
