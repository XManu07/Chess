using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;

namespace Chess
{
    public enum PieceNames { gol,pawn=1, rook, knight, bishop, king, queen };
    internal abstract class Piece
    {
        private PieceNames pieceName;
        private Colors pieceColor;
        private Point piecePosition;

        private List<Point> LValidMoves;
        public static BoardMatrix matrix;
        
        public Piece()
        {
            LValidMoves = new List<Point>();
        }
        #region Set,Get
        public void SetPieceName(PieceNames name)
        {
            pieceName = name;
        }
        public PieceNames GetPieceName()
        {
            return pieceName;
        }
        public void SetPieceColor(Colors color)
        {
            pieceColor = color;
        }
        public Colors GetPieceColor()
        {
            return pieceColor;
        }
        public void SetPosition(Point pos)
        {
            piecePosition.X = pos.X;
            piecePosition.Y = pos.Y;
        }
        public void SetPosition(int x,int y)
        {
            piecePosition.X = x;
            piecePosition.Y = y;
        }
        public Point GetPiecePosition()
        {
            return piecePosition;
        }

        public List<Point> GetLValidMoves()
        {
            return LValidMoves;
        }
        #endregion

        internal abstract bool ValidMove(Point destination);

        public virtual bool KingPosIsValidMove(Point kingPos)
        {
            if (ValidDestination(kingPos))
            {
                if (PieceToDestinationIsEmpty(kingPos))
                    return true;              
            }
            return false;
        }

        public abstract bool ValidDestination(Point destination);
        public abstract bool PieceToDestinationIsEmpty(Point destination);

        public string ListMovesToString()
        {
            string data=null;
            foreach(Point move in LValidMoves)
            {
                data=data+move.X.ToString()+ move.Y.ToString();
            }
            return data;
        }

    }
}
 