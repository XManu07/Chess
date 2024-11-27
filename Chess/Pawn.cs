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
    internal class Pawn: Piece 
    {

        public Pawn(Colors color,Point position):
            base()
        {
            SetPieceName(PieceNames.pawn);
            SetPieceColor(color);
            SetPosition(position);
            SetPieceImage();
        }

        internal override bool ValidMove(Point destination)
        {
            /*
            -destination = next square
            if is black
                if piece line=destination line+1 and destination column = piece column
                    if destination == empty (doesn t have white or black piece)
                        return true
                    else return false
            
            -in case in destination is a picturebox
            if piece line = destination line+1 and destination column is piece column +/- 1
                if piece color from destination = white 
                    return true
                if piece color from destination =black
                    return false
            return false
            */
            if (this.GetPieceColor() == Colors.black)
            {
                if (this.GetPiecePosition().X==destination.X+1 && this.GetPiecePosition().Y == destination.Y)
                {
                    //if destination is empty
                    //return true;
                    //else return false
                    Console.WriteLine("mutare valida ");
                    return true;
                }
                if(this.GetPiecePosition().X==destination.X+1 && (
                    this.GetPiecePosition().Y == destination.Y+1 || this.GetPiecePosition().Y == destination.Y - 1))
                {
                    //if destination.color==white
                    //return true
                    //else return false
                    Console.WriteLine("mutare valida ");
                    return true;
                }
                Console.WriteLine("mutare invalida ");
                return false;
            }
            
            if (this.GetPieceColor() == Colors.white)
            {
                if (this.GetPiecePosition().X == destination.X - 1 && this.GetPiecePosition().Y == destination.Y)
                {
                    //if destination is empty
                    //return true;
                    //else return false
                    Console.WriteLine("mutare valida ");
                    return true;
                }
                if (this.GetPiecePosition().X == destination.X + 1 && (
                    this.GetPiecePosition().Y == destination.Y + 1 || this.GetPiecePosition().Y == destination.Y - 1))
                {
                    //if destination.color==white
                    //return true
                    //else return false
                    Console.WriteLine("mutare valida ");
                    return true;
                }
                Console.WriteLine("mutare invalida ");
                return false;

            }
            Console.WriteLine("mutare invalida ");
            return false; 
        }
    }
}
