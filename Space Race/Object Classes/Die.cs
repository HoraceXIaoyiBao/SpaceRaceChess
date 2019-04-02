using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Object_Classes {

   

    /// <summary>
    /// Represents a dice with many face values that can be rolled.
    /// </summary>
    public class Die {
        private const int MIN_FACES = 4;
        private const int DEFAULT_FACE_VALUE = 1;
        private const int SIX_SIDED = 6;
        private static string defaultPath = Environment.CurrentDirectory;
        private static string rollFileName = defaultPath + "\\testrolls.txt";
        private static StreamReader rollFile = new StreamReader(rollFileName);
        private static bool DEBUG = true;
        
        // number of sides on the die
        private int numOfFaces;
        public int NumOfFaces {
            get {
                return numOfFaces;
            }
        }
        
        // current value showing on the die
        private int faceValue;
        public int FaceValue {
            get {
                return faceValue;
            }
        }
        
        private int initialFaceValue; //use only in Reset()
    
        private static Random random = new Random(100);
    

       //-----------------------------------------------------------------
       //  Parameterless Constructor
       //  defaults to a six-sided die with a default initial face value.
       //-----------------------------------------------------------------
        public Die() {
           numOfFaces = SIX_SIDED;
           faceValue = DEFAULT_FACE_VALUE;
        } 

       //-----------------------------------------------------------------
       //  Explicitly sets the size of the die. Defaults to a size of
       //  six if the parameter is invalid.  Face value is randomly chosen
       //-----------------------------------------------------------------
       public Die(int faces){
           
           if (faces < MIN_FACES) {
                numOfFaces = SIX_SIDED;
            } else {
                numOfFaces = faces;
            }

            faceValue = Roll();
	        initialFaceValue = FaceValue;
       }

       //-----------------------------------------------------------------
       //  Rolls the die and returns the result.
       //-----------------------------------------------------------------
       public int Roll() {
            if (!DEBUG)
                {
                  faceValue = random.Next(NumOfFaces) + 1;
                }
            else {
                string RollValue= rollFile.ReadLine();
                if (RollValue == null)
                {
                
                    rollFile = new StreamReader(rollFileName);
                    RollValue = rollFile.ReadLine();
                }
                faceValue = int.Parse(RollValue);
               
                    }
            return FaceValue;
        }
     
     
       //-----------------------------------------------------------------
       //  Resets the die face value to its initial value.
       //-----------------------------------------------------------------
       public void Reset() {
           faceValue = initialFaceValue;
        }
  
        //----------------------------------------------------------------
        // Returns a String representation of the dice's attributes.
        //----------------------------------------------------------------
       public override string ToString() {
           string str = string.Format("{0}-Sided die showng {1}", numOfFaces, faceValue);
           
           return str;
       }
    }
}
