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
    public enum Colors { white=1, black=-1 };
    
    internal class GameLogic
    {
        private Player player1;
        private Player player2;
        private Player currentPlayer;
        private Player opponentPlayer;

        BoardMatrix boardMatrix;
        private Piece pieceToRemove;

        private Piece pieceFromImage;

        Point currentKingPosition;
        public GameLogic()
        {
            InitPlayers();
            Piece.matrix = new BoardMatrix(player1,player2);
            boardMatrix=Piece.matrix;
        }

        public void InitPlayers()
        {
            player1=new Player(Colors.black);
            currentPlayer = player1;
            player2=new Player(Colors.white);
            opponentPlayer = player2; 
        }
        public void PieceChangePos(object sender)
        {

            if (pieceFromImage != null &&
                pieceFromImage.ValidMove(currentPlayer.GetNewPiecePos()))
            {
                currentKingPosition = currentPlayer.GetKingPoint();
                if (currentPlayer.Check(currentKingPosition, opponentPlayer, boardMatrix, pieceFromImage,currentPlayer.GetNewPiecePos()))
                {
                    Console.WriteLine("Check");
                    return;
                }
                if (boardMatrix.MSquareIsOppositePiece(currentPlayer.GetNewPiecePos(),currentPlayer.GetPlayerColor()))
                {
                    pieceToRemove = GetPieceToRemove(currentPlayer);
                    opponentPlayer.RemovePiece(pieceToRemove);
                }
                UpdateSelectedPiece();
                SwitchPlayer();
                pieceFromImage = null;
            }
        }
        public void UpdateSelectedPiece()
        {
            boardMatrix.MUpdateOldPos(pieceFromImage.GetPiecePosition());
            pieceFromImage.SetPosition(currentPlayer.GetNewPiecePos());
            boardMatrix.MInitPieces(player1, player2);
            boardMatrix.MShow();
        }
        public Piece GetPieceToRemove(Player currentPlayer)
        {
            Piece pieceToRemove;
            if (currentPlayer == player1)
            {
                foreach(var piece in player2.GetPieces())
                {
                    if (piece.GetPiecePosition()==currentPlayer.GetNewPiecePos())
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
                    if (piece.GetPiecePosition() == currentPlayer.GetNewPiecePos())
                    {
                        pieceToRemove = piece;
                        return pieceToRemove;
                    }
                }
            }
            return null;
        }

        public void SwitchPlayer()
        {           
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