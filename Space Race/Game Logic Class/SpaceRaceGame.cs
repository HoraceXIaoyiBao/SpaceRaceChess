using System.Drawing;
using System;
using System.ComponentModel;
using Object_Classes;
using System.Linq;
using System.Windows.Forms;


namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        private static int SinglePlayerNumber=0;//record the player's number during the single step mode

        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;
        
        private static int numberOfPlayers = 2;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values
        
        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();


        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>

        //input the number of players and check if it is valid
        public static void InputNumberOfPlayers()
        {
            Console.WriteLine("         This game is for 2 to 6 players  \n         How many players (2-6):");

            //if the input number is invalid ,ask user to type again
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out numberOfPlayers) != false)//if it is a number
                {
                    if (numberOfPlayers <= MAX_PLAYERS && numberOfPlayers >= MIN_PLAYERS)//if between 2 and 6
                        break;
                }
                Console.WriteLine("         Invalid input ! This game is for 2 to 6 players \nHow many players (2-6):");//if invalid ,type again
            }
        }//end InputNumberOfPlayers

        //Initialized players
        public static void SetUpPlayers() 
        {
            //clear the players list
            //in case if the players data of last game existed in the list
            players.Clear();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                  players.Add(new Player(names[i]));// add the new player into the list, named as number
                 players[i].PlayerTokenColour = playerTokenColours[i];
            }
        }//end SetUpPlayers

        /// <summary>
        ///  Plays one round of a game
        /// </summary>
        //function for each round
        //IfSingleStep is should be true if the user want to play a step one time
        //else it should be false
        public static void PlayOneRound(Boolean IfSingleStep) 
        {
            if (IfSingleStep == true)//if the user choose to play single step once
            {
                if (players[SinglePlayerNumber].HasPower == true)
                {
                    players[SinglePlayerNumber].Play(new Die(), new Die());

                    //after the player moving, check if he has fuel
                    //If the player run out of fuel ,show the message 
                    if (players[SinglePlayerNumber].HasPower == false)
                    {
                        RunOutOfFuel(players[SinglePlayerNumber]);
                    }
                }

                SinglePlayerNumber++;//static number plus 1 after each players' moving 

              if (SinglePlayerNumber == numberOfPlayers)//if finish one round, return to first player
                    SinglePlayerNumber = 0;
            }

            if (IfSingleStep == false)//if the user choose to play one round once
            {
                for (int i = 0; i < players.Count(); i++)// for each player
                {
                    if (players[i].HasPower == true)
                    {
                        players[i].Play(new Die(), new Die());

                        //if the player ran out of fuel in this round,
                        //show the message
                        if (players[i].HasPower == false)
                        {
                            RunOutOfFuel(players[i]);//pop up the message box
                            Console.WriteLine("         {0} has ran out of fuel \n", players[i].Name);
                        }
                    }
                    //only display in console mode
                    Console.WriteLine("         {0} on square {1} with {2} yottawatt of power remaining\n", players[i].Name, players[i].Position, players[i].RocketFuel);
                }
            }
        }//end PlayOneRound

        //output the message of run out of fuel
        private static void RunOutOfFuel(Player player)
        {
            //if this player's out of fuel message haven't been show
            if (player.IfHasShowOutOfFuelMessage == false)
            {
                MessageBox.Show(player.Name + " has ran out of fuel!  ");
                
                //once showed , change the value
                player.IfHasShowOutOfFuelMessage = true;
            }
        }

        //check if there is a player have finished
        //if founded return true
        //else return false
        public static Boolean CheckIfFinished()
        {
            for (int i = 0; i < players.Count(); i++)
            {
                if (players[i].AtFinish == true)//find someone finished
                {
                    //reset the  SinglePlayerNumber to 0,
                    //make sure the game start at player 0 at next round.
                    SinglePlayerNumber = 0;
                    return true;
                }
            }
            return false;
        }// end CheckIfFinished

     
        //show the final mark
        //only for console mode   
        public static void ShowFinialMarks()
        {
            Console.WriteLine("\n         The following players finshed the game:");
            for (int i = 0; i < players.Count(); i++)// find the finished player and output name
            {
                if (players[i].AtFinish == true)
                    Console.Write("\n                  {0}", players[i].Name);
                if (i == players.Count() - 1) 
                    Console.Write("\n");
            }

            //show final situation
            Console.WriteLine("\n         Individual players finished with the at the loaction specified\n");

            for (int i = 0; i < players.Count(); i++)//output each player's mark
            {
                Console.WriteLine("         {0}  with {1} yottawatt of power on square {2}\n", players[i].Name, players[i].RocketFuel, players[i].Position);
            }

            //clear the players list ,
            //in case if the user wants to play again.
            players.Clear();

        }//end ShowFinialMarks

    }//end SnakesAndLadders
}