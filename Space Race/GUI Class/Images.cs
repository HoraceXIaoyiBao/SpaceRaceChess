using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snakes_and_Ladders
{
    /// <summary>
    /// Provides easy access to various images, 
    /// 
    /// 
    /// For your assignment, it is not important to understand all the finer details 
    /// of all the methods in this class.
    /// 
    /// Do not confuse this class with the Microsoft-supplied class, Image, which has a similar name.
    /// </summary>
    public static class Images {

        private static Bitmap[] dieImages;

        /// <summary>
        /// Constructor - Loads images from disk files.
        /// </summary>
        static Images() {
           
            //Load die images
            dieImages = new Bitmap[7];

            for (int i = 1; i < dieImages.Length; i++) {
                string dieImageName = "Face_";
                dieImageName = dieImageName + i;
                dieImages[i] = LoadImage("Dice", dieImageName);
            }
        }//end Images

        
        public static Bitmap GetDieImage(int faceValue) {
            return dieImages[faceValue];
        }


        /// <summary>
        /// Used by the constructor in this class only.  Do NOT use elsewhere.
        /// </summary>
        private static Bitmap LoadImage(string subfolderName, string imageName) {
            string fileSpec = string.Format(@".\Images\{0}\{1}.png", subfolderName, imageName);
            Bitmap bitmap = new Bitmap(fileSpec);
            return bitmap;
        }
    }
}
