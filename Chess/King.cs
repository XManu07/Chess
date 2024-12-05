using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class King: Piece
    {
        public King(Colors color,Point position) 
            : base()
        {
            SetPieceName(PieceNames.king);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }

        public override bool ValidDestination(Point destination)
        {
            return false;
        }
        public override bool PieceToDestinationIsEmpty(Point destination)
        {
            return false;
        }
        public  bool ValidSquare(Point destination)
        {
            if (Math.Abs(GetPiecePosition().X - destination.X) <= 1 && Math.Abs(GetPiecePosition().Y - destination.Y) <= 1)
                if (!OneSquareIsKing(destination))
                    return true;
            return false;
        }

        private bool OneSquareIsKing(Point destination)
        {
            for(int i = destination.X - 1; i <= destination.X + 1; i++)
            {
                if (PositionIsInMatrix(i, destination.Y-1))
                {
                    Point dest=new Point(i, destination.Y-1);
                    if (matrix.MSquareIsOppositeKing(dest,GetPieceColor()))
                    {
                        return true;
                    }
                }
                if (PositionIsInMatrix(i, destination.Y +1))
                {
                    Point dest = new Point(i, destination.Y + 1);
                    if (matrix.MSquareIsOppositeKing(dest, GetPieceColor()))
                    {
                        return true;
                    }
                }
            }
            if (PositionIsInMatrix(destination.X+1, destination.Y))
            {
                Point dest = new Point(destination.X+1, destination.Y);
                if (matrix.MSquareIsOppositeKing(dest, GetPieceColor()))
                {
                    return true;
                }
            }
            if (PositionIsInMatrix(destination.X-1, destination.Y ))
            {
                Point dest = new Point(destination.X-1, destination.Y );
                if (matrix.MSquareIsOppositeKing(dest,GetPieceColor()))
                {
                    return true;
                }
            }
            return false;
        }
        public bool PositionIsInMatrix(int x,int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
                return true;
            return false;
        }

        internal override bool ValidMove(Point destination)
        {
            if (ValidSquare(destination) && matrix.MSquareIsEmpty(destination))
                return true;
            if(ValidSquare(destination)&& matrix.MSquareIsOppositePiece(destination,GetPieceColor()))
                return true;
            return false;
        }

    }
}
