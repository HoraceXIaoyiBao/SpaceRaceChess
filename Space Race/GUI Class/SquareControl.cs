using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Object_Classes;
using Game_Logic_Class;

namespace GUI_Class
{

    public partial class SquareControl : PictureBox {

        private Square square;  // A reference to the corresponding square object, in the Object Classes.

        private BindingList<Player> players;  // References the players in the overall game.

        // the players on the corresponding square, initially no one is on 
        private bool[] containsPlayers = new bool[SpaceRaceGame.MAX_PLAYERS];
        public bool[] ContainsPlayers {
            get {
                return containsPlayers;
            }
            set
            {
                containsPlayers = value;
            }
        }

        public const int SQUARE_SIZE = 95;

        // Font and brush for displaying text inside the square.
        private Font textFont = new Font("Microsoft Sans Serif", 8);
        private Brush textBrush = Brushes.White;

        public SquareControl(Square square, BindingList<Player> players) {

            this.square = square;
            this.players = players;

            //  Set GUI properties of the whole square.
            Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
            Margin = new Padding(0);  // No spacing around the cell. (Default is 3 pixels.)
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.SlateGray;

            SetSquareImage();
        }

        private void SetSquareImage() {

            if (square is WormholeSquare) {
                LoadImageFromFile("Wormhole entry.png");
                textBrush = Brushes.Red;
            } else if (square is BlackholeSquare) {
                LoadImageFromFile("Going down.png");
                textBrush = Brushes.Red;
            } else if (square.Name == "Finish") {
                LoadImageFromFile("Landing.png");
                textBrush = Brushes.Black;
                textFont = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            } else  if (square.Name == "Start")
            {
                LoadImageFromFile("blast-off-rocket.png");
                textBrush = Brushes.Black;
                textFont = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            } else { //Ordinary Square
                LoadImageFromFile("Space.png");
            }
        }

        private void LoadImageFromFile(string fileName) {

            Image image = Image.FromFile(@"Images\" + fileName);
            Image = image;
            SizeMode = PictureBoxSizeMode.StretchImage;  // Zoom is also ok.
            
        }

        protected override void OnPaint(PaintEventArgs e) {

            //  Due to a limitation in WinForms, don't use base.OnPaint(e) here.

            if (Image != null)
                e.Graphics.DrawImage(Image, e.ClipRectangle);

            string name = square.Name;

            // Create rectangle for drawing.
            float textWidth = textFont.Size * name.Length;
            float textHeight = textFont.Height;
            float textX = e.ClipRectangle.Right - textWidth;
            float textY = e.ClipRectangle.Bottom - textHeight;
            RectangleF drawRect = new RectangleF(textX, textY, textWidth, textHeight);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Far;  // Right-aligned.

            // Draw string to screen.
            e.Graphics.DrawString(name, textFont, textBrush, drawRect, drawFormat);


            //  Draw player tokens (when any players are on this square).
           const int PLAYER_TOKENS_PER_ROW = 3;
           const int PLAYER_TOKEN_SIZE = 20;  // pixels.
            const int PLAYER_TOKEN_SPACING = 8;// (SQUARE_SIZE - (PLAYER_TOKEN_SIZE * PLAYER_TOKENS_PER_ROW)) / (PLAYER_TOKENS_PER_ROW - 1);

            for (int i = 0; i < containsPlayers.Length; i++) {
                if (containsPlayers[i]) {
                    int xPosition = i % PLAYER_TOKENS_PER_ROW;
                    int yPosition = i / PLAYER_TOKENS_PER_ROW;
                    int xPixels = xPosition * (PLAYER_TOKEN_SIZE + PLAYER_TOKEN_SPACING);
                    int yPixels = yPosition * (PLAYER_TOKEN_SIZE + PLAYER_TOKEN_SPACING);
                    Brush playerTokenColour = players[i].PlayerTokenColour;                                  //UNCOMMENT :Remove  line comments on this and next line whne instructed to do so in Part B specs
                    e.Graphics.FillEllipse(playerTokenColour, xPixels, yPixels, PLAYER_TOKEN_SIZE, PLAYER_TOKEN_SIZE);
                }
            }//endfor
        }

    }//end class
}
