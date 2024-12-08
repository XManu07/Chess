﻿using System;
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
    public enum Colors { white=1, black=-1 };
    
    internal class GameLogic
    {
        private TableLayoutPanel chessBoard;

        private Player player1;
        private Player player2;
        private Player currentPlayer;
        private Player opponentPlayer;

        BoardMatrix boardMatrix;
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
            Piece.matrix = new BoardMatrix(player1,player2);
            boardMatrix=Piece.matrix;
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
                pieceFromImage.ValidMove(currentPlayer.getPointFromDestination(destinationSquare, chessBoard)))
            {
                currentKingPosition = currentPlayer.GetOpponentKingPoint();//change name
                if (currentPlayer.Check(currentKingPosition, opponentPlayer, boardMatrix,pieceFromImage,
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
            boardMatrix.MUpdateOldPos(pieceFromImage.GetPiecePosition());
            pieceFromImage.SetPosition(chessBoard.GetCellPosition(destinationSquare).Row, chessBoard.GetCellPosition(destinationSquare).Column);
            boardMatrix.MInitPieces(player1, player2);
            boardMatrix.MShow();
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

    }
}