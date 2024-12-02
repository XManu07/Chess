using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chess
{
    public enum Colors { white=1, black=9 };
    public enum Pieces { blackKing=8,whiteKing=2}
    internal class GameLogic
    {
        private TableLayoutPanel chessBoard;

        private Player player1;
        private Player player2;
        private Player currentPlayer;
        private Player opponentPlayer;

        private int[,] allPieces=new int[8,8];
        private Piece pieceToRemove;

        private PictureBox selectedPieceImage;
        private Piece pieceFromImage;
        private Panel parentSelectedImage;
        private Control destinationSquare;
        Point currentKingPosition;
        public GameLogic(TableLayoutPanel game)
        {
            this.chessBoard = game;
            InitBoardBackground();
            InitPlayers();
            InitPieces();
            InitPieceMatrix(player1,player2);
            ShowMatrix(allPieces);
            StartGame();
        }

        #region InitBoard
        public void InitPlayers()
        {
            player1=new Player(Colors.black);
            currentPlayer = player1;
            player2=new Player(Colors.white);
            opponentPlayer = player2; 
        }
        public void InitBoardBackground()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Panel square = new Panel();
                    square.BackColor = (row + column) % 2 == 0 ? Color.SaddleBrown : Color.Beige;
                    square.Click += Control_Click;
                    chessBoard.Controls.Add(square, column, row);
                }
            }
        }
        public void InitPieces()
        {
            foreach (var item in player1.GetPieces())
            {
                item.GetPieceImage().Click += Control_Click;
                chessBoard.GetControlFromPosition(item.GetPiecePosition().Y, item.GetPiecePosition().X)
                    .Controls.Add(item.GetPieceImage());
            }
            foreach (var item in player2.GetPieces())
            {
                item.GetPieceImage().Click += Control_Click;
                chessBoard.GetControlFromPosition(item.GetPiecePosition().Y, item.GetPiecePosition().X)
                    .Controls.Add(item.GetPieceImage());
            }
        }
        #endregion

        #region Click Listener
        public void Control_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(PictureBox))
            {
                HandlePictureBoxClick(sender);
            }
            else
            {
                HandlePanelClick(sender);
            }
        }
        public void HandlePictureBoxClick(object sender)
        {
            selectedPieceImage = sender as PictureBox;
            parentSelectedImage = selectedPieceImage.Parent as Panel;
            pieceFromImage = currentPlayer.GetPieceFromImage(parentSelectedImage, chessBoard);

            if (pieceFromImage != null)
                Console.WriteLine("clicked on picture box " + pieceFromImage.ToString());
        }
        public void HandlePanelClick(object sender)
        {
            destinationSquare = sender as Panel;
            Console.WriteLine("click on panel " + chessBoard.GetCellPosition(destinationSquare));

            if (selectedPieceImage != null &&
                pieceFromImage !=null &&
                pieceFromImage.ValidMove(currentPlayer.getPointFromDestination(destinationSquare, chessBoard), allPieces))
            {
                currentKingPosition = currentPlayer.GetOpponentKingPoint();
                if (currentPlayer.Check(currentKingPosition, opponentPlayer, allPieces,pieceFromImage,
                    new Point(chessBoard.GetCellPosition(destinationSquare).Row, chessBoard.GetCellPosition(destinationSquare).Column)))
                {
                    Console.WriteLine("Check");
                    return;
                }
                    if (PanelHasImage(destinationSquare))
                {
                    pieceToRemove = GetPieceToRemove(destinationSquare, currentPlayer);
                    opponentPlayer.RemovePiece(pieceToRemove);
                    RemovePictureBox(destinationSquare);
                }
                UpdateSelectedPieceImage();
                SwitchPlayer();
                selectedPieceImage = null;
                pieceFromImage = null;
                parentSelectedImage = null;
            }
        }
        #endregion
        public void StartGame()
        {
        }
        public void UpdateSelectedPieceImage()
        {
            parentSelectedImage.Controls.Remove(selectedPieceImage);
            destinationSquare.Controls.Add(selectedPieceImage);
            UpdateMatrix(pieceFromImage.GetPiecePosition());
            pieceFromImage.SetPosition(chessBoard.GetCellPosition(destinationSquare).Row, chessBoard.GetCellPosition(destinationSquare).Column);
            InitPieceMatrix(player1, player2);
            ShowMatrix(allPieces);
        }
        public Piece GetPieceToRemove(Control destinationSquare, Player currentPlayer)
        {
            Piece pieceToRemove;
            if (currentPlayer == player1)
            {
                foreach(var piece in player2.GetPieces())
                {
                    if (chessBoard.GetCellPosition(destinationSquare).Row==piece.GetPiecePosition().X&&
                        chessBoard.GetCellPosition(destinationSquare).Column == piece.GetPiecePosition().Y)
                    {
                        pieceToRemove = piece;
                        return pieceToRemove;
                    }
                }
            }


            if (currentPlayer == player2)
            {
                foreach (var piece in player1.GetPieces())
                {
                    if (chessBoard.GetCellPosition(destinationSquare).Row == piece.GetPiecePosition().X &&
                        chessBoard.GetCellPosition(destinationSquare).Column == piece.GetPiecePosition().Y)
                    {
                        pieceToRemove = piece;
                        return pieceToRemove;
                    }
                }
            }
            return null;
        }
        private void RemovePictureBox(Control destinationSquare)
        {
            destinationSquare.Controls.RemoveAt(0);
        }
        private bool PanelHasImage(Control destinationSquare)
        {
            if(destinationSquare.HasChildren == true)
            {
                return true;
            }
            return false;
        }
        public void SwitchPlayer()
        {
            foreach (var piece in currentPlayer.GetPieces())
            {
                piece.GetPieceImage().Enabled = false;
            }
            foreach (var piece in opponentPlayer.GetPieces())
            {
                piece.GetPieceImage().Enabled = true;
            }
           
            if(currentPlayer == player1) 
            {
                currentPlayer = player2;
                opponentPlayer = player1;
            }
            else
            {
                currentPlayer = player1;
                opponentPlayer = player2;
            }
        }

        #region Matrix
        public void UpdateMatrix(Point pos)
        {
            allPieces[pos.X, pos.Y] = 0;
        }
        public void InitPieceMatrix(Player p1, Player p2)
        {
            foreach(var piece in p1.GetPieces())
            {
                allPieces[piece.GetPiecePosition().X,piece.GetPiecePosition().Y] = (int)p1.GetPlayerColor();
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
        public void ShowMatrix(int[,] pieces)
        {
            for (int line=0;line<8;line++)
            {
                for(int column = 0; column < 8; column++)
                {
                    Console.Write(allPieces[line, column] + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion 
    }
}