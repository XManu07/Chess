using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Player
    {
        private Colors playerColorOfPieces;
        private List<Piece> pieces;

        public Player(Colors color)
        {
            this.playerColorOfPieces = color;
            InitPieces();
        }
        public int GetLine()
        {
            return playerColorOfPieces == Colors.black ? 7 : 0;
        }
        public int GetPawnLine()
        {
            int fline=GetLine();
            return fline== 0 ? fline + 1 : fline - 1;
        }
        public List<Piece> GetPieces()
        {
            return pieces;
        }

        void InitPieces()
        { 
            pieces = new List<Piece>
            {
                new Rook(playerColorOfPieces, new Point(GetLine(), 0)),
                new Knight(playerColorOfPieces, new Point(GetLine(), 1)),
                new Bishop(playerColorOfPieces, new Point(GetLine(), 2)),
                new Queen(playerColorOfPieces, new Point(GetLine(), 3)),
                new King(playerColorOfPieces, new Point(GetLine(), 4)),
                new Bishop(playerColorOfPieces, new Point(GetLine(), 5)),
                new Knight(playerColorOfPieces, new Point(GetLine(), 6)),
                new Rook(playerColorOfPieces, new Point(GetLine(), 7)),

            };
            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new Pawn(playerColorOfPieces,new Point(GetPawnLine(),i)));
            }
        }

        public void ShowPieces()
        {
            pieces.ForEach(p => { Console.WriteLine(p.ToString()); });
        }

        public void Move()
        {
            
        }
    }
}
