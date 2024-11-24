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
    internal class Pawn: Piece 
    {
        public Pawn(Colors color,Point position):
            base()
        {
            SetPieceName(PieceNames.pawn);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }
    }
}
