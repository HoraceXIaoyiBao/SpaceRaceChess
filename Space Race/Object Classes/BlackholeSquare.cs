using System.Diagnostics;

namespace Object_Classes {
    /// <summary>
    /// Blackhole Square has a destination position and
    /// fuel used to jump to that position
    /// </summary>
    public class BlackholeSquare : Square {
        private int destination, fuelUsed;

        public BlackholeSquare(string name, int number, int next, int fuel)
                                    : base(name, number) 
        {
            destination = next;
            fuelUsed = fuel;
        }


        public override Square NextSquare() 
        {
            return Board.Squares[destination];
        }//end NextSquare

        /// <summary>  
        /// Landing on WormHole or BlackHole Square uses a specified
        ///   amount of fuel defined in Board class as well as
        ///   updating the location and position of the player
        ///    
        /// Pre:  the player lands on this square
        /// Post: amount of fuel is consumed, player's location and position 
        ///        on the Board is udated
        /// </summary>
        public override void LandOn(Player player)
        {
            player.Location = player.Location.NextSquare();

            player.Position = destination;

            player.ConsumeFuel(fuelUsed);
        } //end LandOn

    }//end BlackholeSquare
}
