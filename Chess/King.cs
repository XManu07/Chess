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
        public override bool ValidSquare(Point destination)
        {
            return false;
        }

        public override bool PieceToDestinationIsEmpty(Point destination, int[,] allPieces)
        {
            return false;
        }
        public  bool ValidSquare(Point destination,int[,]allPieces)
        {
            if (Math.Abs(GetPiecePosition().X - destination.X) <= 1 && Math.Abs(GetPiecePosition().Y - destination.Y) <= 1)
                if (!OneSquareIsKing(destination,allPieces))
                    return true;
            return false;
        }

        private bool OneSquareIsKing(Point destination, int[,] allPieces)
        {
            for(int i = destination.X - 1; i <= destination.X + 1; i++)
            {
                if (PositionIsInMatrix(i, destination.Y-1))
                {
                    Point dest=new Point(i, destination.Y-1);
                    if (SquareIsOpositeKing(dest, allPieces))
                    {
                        return true;
                    }
                }
                if (PositionIsInMatrix(i, destination.Y +1))
                {
                    Point dest = new Point(i, destination.Y + 1);
                    if (SquareIsOpositeKing(dest, allPieces))
                    {
                        return true;
                    }
                }
            }
            if (PositionIsInMatrix(destination.X+1, destination.Y))
            {
                Point dest = new Point(destination.X+1, destination.Y);
                if (SquareIsOpositeKing(dest, allPieces))
                {
                    return true;
                }
            }
            if (PositionIsInMatrix(destination.X-1, destination.Y ))
            {
                Point dest = new Point(destination.X-1, destination.Y );
                if (SquareIsOpositeKing(dest, allPieces))
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

        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if (ValidSquare(destination,allPieces) && SquareIsEmpty(destination, allPieces))
                return true;
            if(ValidSquare(destination, allPieces)&& SquareIsOpositePiece(destination,allPieces)&&!SquareIsOpositeKing(destination,allPieces))
                return true;
            return false;
        }

    }
}
