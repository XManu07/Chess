using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chess
{
    public enum Colors { white, black };
    internal class GameLogic
    {
        private TableLayoutPanel chessBoard;
        Player player1 = new Player(Colors.black);
        Player player2= new Player(Colors.white);

        public GameLogic(TableLayoutPanel game)
        {
            this.chessBoard = game;
            StartGame();
            player1.ShowPieces();
            player2.ShowPieces();
        }
        public bool Mate()
        {
            return true;
        }  
        public void InitBoardBackground()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Beige;
                    chessBoard.Controls.Add(square, column, row);
                }
            }
        }
        public void InitPieces()
        {
            foreach (var item in player1.GetPieces())
            {
                //item.pieceImage.Click += PictureBox_Click;
                chessBoard.GetControlFromPosition(item.GetPiecePosition().Y, item.GetPiecePosition().X)
                    .Controls.Add(item.GetPieceImage());
                Console.WriteLine(item.GetPieceImage().ToString()) ;
            }
            foreach (var item in player2.GetPieces())
            {
                //item.pieceImage.Click += PictureBox_Click;
                chessBoard.GetControlFromPosition(item.GetPiecePosition().Y, item.GetPiecePosition().X)
                    .Controls.Add(item.GetPieceImage());
            }
        }
        public void InitBoard()
        {
            InitBoardBackground();
            InitPieces();
        }
        public void StartGame()
        {
            InitBoard();
            //while (!Mate())
            //{
            //    player1.Move();
            //    player2.Move();
            //}
        }
    }
}
