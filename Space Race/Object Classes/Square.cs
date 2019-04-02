using System;

namespace Object_Classes {
    /// <summary>
    /// A Ordinary square, the Start nd Finish squares as well as being the parent (superclass)
    ///  for a Blackhole Square and a Wormhole Square
    /// </summary>
    public class Square  {
        private int number;
        public int Number {
            get {
                return number;
            }
        }

        // The name of this square. 
        // Usually a string version of its number, but not for the Start and Finish squares.
        private string name;
        public string Name {
            get {
                return name;
            }
        }

        // Only used by Wormhole and Blackhole Squares
        //
        // Indicates the final destination square a player moves to if they land
        // on a Wormhole or Blackhole Square at the completion of their move.
        public virtual Square NextSquare() {
            return null;
        }
         

        /// <summary>
        /// Parameterless constructor.
        /// Do not want the generic default constructor to be used
        /// as there is no way to set the square's data.
        /// This overwrites the compiler's generic default constructor.
        /// Pre:  none
        /// Post: ALWAYS throws an ArgumentException.
        /// </summary>
        /// <remarks>NOT TO BE USED!</remarks>
        public Square() {
            throw new ArgumentException("Parameterless constructor invalid.");
        } // end Square constructor

        /// <summary>
        /// Constructor with initialisation parameters.
        /// </summary>
        /// <param name="name">Name for this square</param>
        /// <param name="Id">ID for this square</param>
        public Square(string name, int number) {
            this.name = name;
            this.number = number;
        } // end Square constructor

        /// <summary>
        /// Reduces the amount of rocket fuel used when a player lands on this square
        /// at the end of their turn.
        /// 
        /// Landing on any Ordinary Square uses a constant amount of fuel
        ///    
        /// Landing on WormHole or BlackHole Square uses a specified
        ///   amount of fuel defined in Board class as well as
        ///   updating the location of the player
        ///    
        /// Pre:  the player lands on this square
        /// Post: amount of fuel is consumed
        /// </summary>
        /// <param name="player">who landed on this square</param>
        /// <remarks>Virtual method</remarks>
        public virtual void LandOn(Player player) {
            const int fuelUsed = 2;
            
            player.ConsumeFuel(fuelUsed);
        } //end LandOn


        /// <summary>
        /// Check if a square is the 'start' square.
        /// Pre:  an initialised square location to check
        /// Post: whether the supplied location is the 'start' square.
        /// </summary>
        /// <param name="location">the square to check</param>
        /// <returns>
        /// true if the square is the start square,
        /// false otherwise
        /// </returns>
        public bool IsStart() {
            // check whether the location is the 'start' square.
            return (Number == Board.START_SQUARE_NUMBER);
        }



    }// end Square
}
