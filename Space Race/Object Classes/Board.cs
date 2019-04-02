using System.Collections.Generic;
using System.Diagnostics;
using System;


namespace Object_Classes {
    /// <summary>
    /// Models a game board for Space Race consisting of three different types of squares
    /// 
    /// Ordinary squares, Wormhole squares and Blackhole squares.
    /// 
    /// landing on a Wormhole or Blackhole square at the end of a player's move 
    /// results in the player moving to another square
    /// 
    /// </summary>
    public static class Board {
        /// <summary>
        /// Models a game board for Space Race consisting of three different types of squares
        /// 
        /// Ordinary squares, Wormhole squares and Blackhole squares.
        /// 
        /// landing on a Wormhole or Blackhole square at the end of a player's move 
        /// results in the player moving to another square
        /// 
        /// 
        /// </summary>

        public const int NUMBER_OF_SQUARES = 56;
        public const int START_SQUARE_NUMBER = 0;
        public const int FINISH_SQUARE_NUMBER = NUMBER_OF_SQUARES - 1;

        private static Square[] squares = new Square[NUMBER_OF_SQUARES];

        public static Square[] Squares {
            get {
                Debug.Assert(squares != null, "squares != null",
                   "The game board has not been instantiated");
                return squares;
            }
        }

        public static Square StartSquare {
            get {
                return squares[START_SQUARE_NUMBER];
            }
        }


        /// <summary>
        ///  Eight Wormhole squares.
        ///  
        /// Each row represents a Wormhole square number, the square to jump forward to and the amount of fuel consumed in that jump.
        /// 
        /// For example {2, 22, 10} is a Wormhole on square 2, jumping to square 22 and using 10 units of fuel
        /// 
        /// </summary>
        private static int[,] wormHoles =
        {
            {2, 22, 10},
            {3, 9, 3},
            {5, 17, 6},
            {12, 24, 6},
            {16, 47, 15},
            {29, 38, 4},
            {40, 51, 5},
            {45, 54, 4}
        };

        /// <summary>
        ///  Eight Blackhole squares.
        ///  
        /// Each row represents a Blackhole square number, the square to jump back to and the amount of fuel consumed in that jump.
        /// 
        /// For example {10, 4, 6} is a Blackhole on square 10, jumping to square 4 and using 6 units of fuel
        /// 
        /// </summary>
        private static int[,] blackHoles =
        {
            {10, 4, 6},
            {26, 8, 18},
            {30, 19, 11},
            {35,11, 24},
            {36, 34, 2},
            {49, 13, 36},
            {52, 41, 11},
            {53, 42, 11}
        };


        /// <summary>
        /// Parameterless Constructor
        /// Initialises a board consisting of a mix of Ordinary Squares,
        ///     Wormhole Squares and Blackhole Squares.
        /// 
        /// Pre:  none
        /// Post: board is constructed
        /// </summary>
        
        //check if the squre should be a wormhole square
        //return the position of the squre in the list 
        //return -1 if not found
        public static int Check_if_wormhole(int square_number)
        {
            for (int i = 0; i < 8; i++)
                if (square_number == wormHoles[i,0]  )
                    return i;
            return -1;
        }//end Check_if_wormhole

        //check if the squre should be a blackhole square
        //return the position of the squre in the list 
        //return -1 if not found
        public static int Check_if_blackhole(int square_number){
            for (int i = 0; i < 8; i++)
                if (square_number == blackHoles[i, 0])
                    return i;
            return -1;
        }//Check_if_blackhole

        public static void SetUpBoard() {

            // Create the 'start' square where all players will start.
            squares[START_SQUARE_NUMBER] = new Square("Start", START_SQUARE_NUMBER);

            // Create the main part of the board, squares 1 .. 54
            //  CODE NEEDS TO BE ADDED HERE
            //
            //   Need to call the appropriate constructor for each square
            //       either new Square(...),  new WormholeSquare(...) or new BlackholeSquare(...)
            //
        //    List<Square> squares = new List<Square>();
            for (int i = 1; i <= 54; i++)
            {
                int result_of_check;//the result of checking if blackhole or wormhole

                result_of_check = Check_if_blackhole(i);//check

                //if should be a blackhole,create a blackhole
                if (result_of_check != -1)
                {
                    squares[i] = new BlackholeSquare(  i.ToString() , i , blackHoles[result_of_check,1] , blackHoles[result_of_check, 2]  );
                    continue;
                }
                
                result_of_check = Check_if_wormhole(i);//check

                //if should be a blackhole,create a blackhole
                if (result_of_check != -1)
                {
                    squares[i] = new WormholeSquare(  i.ToString() , i , wormHoles[result_of_check, 1] , wormHoles[result_of_check, 2]  );
               
                    continue;
                }

                //if it is not neither a blackhole or wormhole , create a ordinary squre 
                squares[i] = new Square( i.ToString() , i );
            }//end for

            // Create the 'finish' square.
            squares[FINISH_SQUARE_NUMBER] = new Square("Finish", FINISH_SQUARE_NUMBER);
        } // end SetUpBoard

        /// <summary>
        /// Finds the destination square and the amount of fuel used for either a 
        /// Wormhole or Blackhole Square.
        /// 
        /// pre: squareNum is either a Wormhole or Blackhole square number
        /// post: destNum and amount are assigned correct values.
        /// </summary>
        /// <param name="holes">a 2D array representing either the Wormholes or Blackholes squares information</param>
        /// <param name="squareNum"> a square number of either a Wormhole or Blackhole square</param>
        /// <param name="destNum"> destination square's number</param>
        /// <param name="amount"> amont of fuel used to jump to the deestination square</param>
        private static void FindDestSquare(int[,] holes, int squareNum, out int destNum, out int amount) {
            const int start = 0, exit = 1, fuel = 2;
            destNum = 0; amount = 0;

            //  CODE NEEDS TO BE ADDED HERE 

        } //end FindDestSquare

    } //end class Board
}