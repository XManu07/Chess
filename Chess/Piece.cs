using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Piece
    {
        PieceNames piece_name;
        PieceColors piece_color;
        Position position;

        public Piece(PieceNames name, PieceColors color, Position pos)
        {
            piece_name = name;
            piece_color = color;
            position = pos;
        }
        public Piece()
        {
            
        }

        public override string ToString()
        {
            return "[Piece="+piece_name+",color="+piece_color+",position="
                +position.x+","+position.y+"]";
                ;
        }
    }
}
