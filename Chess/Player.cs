using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
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

        internal bool Check(Point kingPos, Player opponentPlayer, int[,] allPieces, Piece pieceFromImage=null, Point point=default )
        {
            Point oldPosition = pieceFromImage.GetPiecePosition();
            pieceFromImage.SetPosition(point);
            int[,] oldMatrix = allPieces;
            allPieces[oldPosition.X, oldPosition.Y] = 0;
            allPieces[point.X, point.Y] = 1;
            foreach(Piece piece in opponentPlayer.GetPieces())
            {
                if (piece.KingPosIsValidMove(kingPos,allPieces))
                    return true;
            }
            pieceFromImage.SetPosition(oldPosition);
            allPieces = oldMatrix;
            return false;
            
        }
        #endregion


    }
}
