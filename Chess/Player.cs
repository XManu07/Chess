using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Player
    {
        public String name;
        PieceColors piece_color;
        public List<Piece> pieces;

        public Player(string name,PieceColors color)
        {
            this.name = name;
            this.piece_color = color;
            InitPieces();
        }

        void InitPieces()
        {
            pieces = new List<Piece>();
            Position pos = new Position();
            if (piece_color == PieceColors.white)
            {
                //init white pieces 
                pos.x = 0;

                pos.y = 0;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.white, pos));
                pos.y = 7;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.white, pos));
                

                pos.y = 1;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.white, pos));
                pos.y = 6;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.white, pos));

                pos.y = 2;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.white, pos));
                pos.y = 5;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.white, pos));

                pos.y = 3;
                pieces.Add(new Queen(PieceNames.queen, PieceColors.white, pos));
                pos.y = 4;
                pieces.Add(new King(PieceNames.king, PieceColors.white, pos));
            
                
                //pawns
                pos.x = 1;
                for (int i = 0; i < 8; i++)
                {
                    pos.y = i;
                    pieces.Add(new Pawn(PieceNames.pawn, PieceColors.white, pos));
                }

            }
            
            if (piece_color == PieceColors.black)
            {
                //init black pieces
                pos.x = 7;

                pos.y = 0;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.black, pos));
                pos.y = 7;
                pieces.Add(new Rook(PieceNames.rook, PieceColors.black, pos));

                pos.y = 1;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.black, pos));
                pos.y = 6;
                pieces.Add(new Knight(PieceNames.knight, PieceColors.black, pos));

                pos.y = 2;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.black, pos));
                pos.y = 5;
                pieces.Add(new Bishop(PieceNames.bishop, PieceColors.black, pos));

                pos.y = 3;
                pieces.Add(new Queen(PieceNames.queen, PieceColors.black, pos));
                pos.y = 4;
                pieces.Add(new King(PieceNames.king, PieceColors.black, pos));

                
                //pawns
                pos.x = 6;
                for (int i = 0; i < 8; i++)
                {
                    pos.y = i;
                    pieces.Add(new Pawn(PieceNames.pawn, PieceColors.black, pos));
                }
            }
        }

        public void ShowPieces()
        {
            pieces.ForEach(p => { Console.WriteLine(p.ToString()); });
        }
      
    }
}
