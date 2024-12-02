using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class BoardMatrix
    {
        public int[,] allPieces = new int[8, 8];
        public BoardMatrix(Player p1,Player p2) 
        {
            InitPieceMatrix(p1, p2);
            ShowMatrix();
        }
        public void InitPieceMatrix(Player p1, Player p2)
        {
            foreach (var piece in p1.GetPieces())
            {
                allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)p1.GetPlayerColor();
                //init king
                if (piece.GetPieceName() == PieceNames.king)
                {
                    if (p1.GetPlayerColor() == Colors.black)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.blackKing;
                    if (p1.GetPlayerColor() == Colors.white)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.whiteKing;
                }
            }

            foreach (var piece in p2.GetPieces())
            {
                allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)p2.GetPlayerColor();
                //init king 
                if (piece.GetPieceName() == PieceNames.king)
                {
                    if (p2.GetPlayerColor() == Colors.black)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.blackKing;
                    if (p2.GetPlayerColor() == Colors.white)
                        allPieces[piece.GetPiecePosition().X, piece.GetPiecePosition().Y] = (int)Pieces.whiteKing;
                }
            }
        }
        public void ShowMatrix()
        {
            for (int line = 0; line < 8; line++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Console.Write(allPieces[line, column] + " ");
                }
                Console.WriteLine();
            }
        }
        public void UpdateMatrix(Point oldPos)
        {
            allPieces[oldPos.X, oldPos.Y] = 0;
        }

    }
}
