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
