using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Chess
{
    internal abstract class Piece
    {
        PieceNames piece_name;
        public PieceColors piece_color;
        public Position position;
        public PictureBox pieceImage;

        
        public void SetPieceImage()
        {
            string nameOfImage = piece_name.ToString()+"_"+piece_color.ToString();

            ResourceManager rm = new ResourceManager("Chess.Properties.Resources", Assembly.GetExecutingAssembly());
            Image PieceImage = (Image)rm.GetObject(nameOfImage);
            pieceImage = new PictureBox();
            pieceImage.Image = PieceImage;
            pieceImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pieceImage.Dock = DockStyle.Fill;
            pieceImage.BackColor = Color.Transparent;

        }
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
                
        }
    }
}
