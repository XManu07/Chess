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
    internal class Queen:Piece
    {
        public Queen(Colors color, Point position) :
              base()
        {
            SetPieceName(PieceNames.queen);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }
        internal override bool ValidMove(Point destination)
        {
            if((this.GetPiecePosition().X == destination.X || this.GetPiecePosition().Y == destination.Y)||
                Math.Abs(destination.X - this.GetPiecePosition().X) == Math.Abs(this.GetPiecePosition().Y - destination.Y))
            {
                Console.WriteLine("mutare valida");
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
