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
        public bool SquareValid(Point destination)
        {
            if (Math.Abs(GetPiecePosition().X - destination.X) <= 1 && Math.Abs(GetPiecePosition().Y - destination.Y) <= 1)
                return true;
            return false;
        }
        internal override bool ValidMove(Point destination, int[,] allPieces)
        {
            if (SquareValid(destination) && SquareIsEmpty(destination, allPieces))
                return true;
            if(SquareValid(destination)&& SquareIsOpositePiece(destination,allPieces))
                return true;
            return false;
        }
    }
}
