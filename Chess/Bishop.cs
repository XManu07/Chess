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
    internal class Bishop:Piece
    {
        public Bishop(Colors color, Point position) :
            base()
        {
            SetPieceName(PieceNames.bishop);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }
    }
}
