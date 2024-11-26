using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Knight:Piece
    {
        public Knight(Colors color, Point position) :
           base()
        {
            SetPieceName(PieceNames.knight);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        internal override bool ValidMove(Point destination)
        {
            Console.WriteLine("mutare valida " + GetPieceName());
            return true;
        }
    }
}
