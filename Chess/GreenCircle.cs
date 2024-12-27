using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Chess
{
    internal class GreenCircle
    {
        PictureBox image;
        public PictureBox Image { get => image; set => image = value; }

        public GreenCircle()
        {
            InitGreenCircle();
        }

        public void InitGreenCircle()
        {
            ResourceManager rm = new ResourceManager("Chess.Properties.Resources", Assembly.GetExecutingAssembly());
            Image image = (Image)rm.GetObject("greenCircle");
            this.image = new PictureBox();
            this.image.Image = image;
            this.image.BackColor = Color.Transparent;
            this.image.Dock = DockStyle.Fill;
            this.image.SizeMode = PictureBoxSizeMode.StretchImage;
            this.image.Enabled = false;
        }
    }
}
