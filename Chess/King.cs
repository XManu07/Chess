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
        internal override bool ValidMove(Point destination)
        {
            if (Math.Abs(GetPiecePosition().X-destination.X)<=1 && Math.Abs(GetPiecePosition().Y - destination.Y) <= 1)
            {
                Console.WriteLine("mutare valid");
                return true;
            }
            else
            {
                Console.WriteLine("mutare invalida");
                return false;
            }
        }
    }
}
