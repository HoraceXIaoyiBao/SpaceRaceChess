using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;
using System.Linq;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();

             Board.SetUpBoard();
             ResizeGUIGameBoard();
             SetUpGUIGameBoard();
            SetupPlayersDataGridView();
            DetermineNumberOfPlayers();
           SpaceRaceGame.SetUpPlayers();
            PrepareToPlay();
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        //  ******************* Uncomment - Remove Block Comment below
        //                         once you've added the TableLayoutPanel to your form.
        //
        //       You will have to replace "tableLayoutPanel" by whatever (Name) you used.
        //
        //        Likewise with "playerDataGridView" by your DataGridView (Name)
        //  ******************************************************************************************


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>

        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard


        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {
            // Code needs to be added here to do the mapping
            //Map the player's square numberto corresponding row and col
            int row = squareNum / 8;
            screenRow = 6 - row;
            if (row % 2 == 0)
                screenCol = squareNum % 8;
            else screenCol = 7 - (squareNum % 8);
        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            // Store the SelectedItem property of the ComboBox in a string
            string NumberOfPlayers = NumberOfPlayersComboBox.Text;

            // Parse string to a number
            int Number = Int32.Parse(NumberOfPlayers);
            // Set the NumberOfPlayers in the SpaceRaceGame class to that number
            SpaceRaceGame.NumberOfPlayers = Number;
        }//end DetermineNumberOfPlayers

        // the process of choosing playing single step or not 
        private void ChooseSingleStep()
        {
            groupBox.Enabled = true;
            RollDiceButton.Enabled = false;
        }//end ChooseSingleStep
        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlay()
        {
            ResetButton.Enabled = false;//disable the Reset button while initialized the game
            ChooseSingleStep();//choose playing single step or not
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);//put players' token in the start square

        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)//return the corresponding square control
        {
            int screenRow;
            int screenCol;
            // get the corresponding row and col
             MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
             return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            //return the player's current position
            return SpaceRaceGame.Players[playerNumber].Position;
        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
                tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            //       determine the square number of the player
            //       retrieve the SquareControl object with that square number
            //       using the typeOfGuiUpdate, update the appropriate element of 
            //          the ContainsPlayers array of the SquareControl object.
            
            if (typeOfGuiUpdate == 0)//if asked to add players into the playerContainers
                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)//for each player,add the token into the corresponding SquareControl
                     {
                             SquareControlAt(GetSquareNumberOfPlayer(i)).ContainsPlayers[i]= true;
                      }
            else//if asked to remove all the players token in all SquaresControl'  playerContainers
                for (int i = 0; i < 56; i++)//for each SquaresControl
                {
                    for(int j=0 ; j < SpaceRaceGame.NumberOfPlayers;j++)//delete all the token
                            SquareControlAt(i).ContainsPlayers[j] = false ;
                }


            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            //Remove all the token
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);

            //Check playing single step or not
            if(YesRadioButton.Checked==true)//play single step once
                SpaceRaceGame.PlayOneRound(true);
            if (NoRadioButton.Checked == true )//play a round once
                SpaceRaceGame.PlayOneRound(false);

            //once begin, can not change the mode during the game
            groupBox.Enabled = false;

            //Update the GUI
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();


            //check if finished after playing a round/step
            if (SpaceRaceGame.CheckIfFinished() == true)
            {
                RollDiceButton.Enabled = false;//disable Roll button if finished
               
                ShowFinalMarksMessageBox();//show final marks
            }
            ResetButton.Enabled = true;//able the Reset button if finished


           


        }// end RollDiceButton_Click

        private void ShowFinalMarksMessageBox()//show the messagebox of final mark
        {
            string result = "The following players finshed the game:\n";
            for (int i = 0; i < SpaceRaceGame.Players.Count(); i++)// find the finished player and output name
            {
                if (SpaceRaceGame.Players[i].AtFinish == true)
                    result=result+ "\n      "+SpaceRaceGame.Players[i].Name;
            }
            MessageBox.Show(result);
        }//end ShowFinalMarksMessageBox
        
        private void ResetButton_Click(object sender, EventArgs e)
        {
            GameReset();  //reset and initialized the game
        }

        private void YesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RollDiceButton.Enabled = true;//if selected , able the Roll button
        }

        private void NoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RollDiceButton.Enabled = true;//if selected , able the Roll button
        }

        private void NumberOfPlayersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If the users change the number of players 
            GameReset();//reset and initialized the game
        }
        private void GameReset()
        {
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);//remove all the players' token
            SetupPlayersDataGridView();//initialized the datagrid
            DetermineNumberOfPlayers();// get the number of players in the ComboBox
            SpaceRaceGame.SetUpPlayers();//initialized the players
            UpdatesPlayersDataGridView();//update the datagrid, put the initial player data in the datagrid
            PrepareToPlay();

            //initial the GUI componets 
            YesRadioButton.Checked = false;
            NoRadioButton.Checked = false;
            RollDiceButton.Enabled = false;
        }
    }// end class
}
