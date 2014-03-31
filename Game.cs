using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tictactoe
{
    public struct Game
    {
        private bool playerXWon;
        private bool playerOWon;

        private Grid board;
        private Label Result;
        private Label PlayerOne;
        private Label PlayerTwo;

        private int total;

        private string played;
        private string[,] movePlayed;
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        public Game (Grid grid, Label resul, Label playerOne, Label playerTwo)
        {
            total      = 1;
            played     = "";
            board      = grid;
            Result     = resul;
            playerXWon = false;
            playerOWon = false;
            PlayerOne  = playerOne;
            PlayerTwo  = playerTwo;
            movePlayed = new string[3, 3];
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private bool CheckColumns ()
        {// check the columns

            for (int i = 0; i < 3; i++)
            {
                if (movePlayed[0, i] == "X" && movePlayed[1, i] == "X" && movePlayed[2, i] == "X") { playerXWon = true; return true; }

                if (movePlayed[0, i] == "O" && movePlayed[1, i] == "O" && movePlayed[2, i] == "O") { playerOWon = true; return true; }
            }

            return false;
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private bool CheckRows ()
        {// check the rows

            for (int i = 0; i < 3; i++ )
            {
                if (movePlayed[i, 0] == "X" && movePlayed[i, 1] == "X" && movePlayed[i, 2] == "X") { playerXWon = true; return true; }

                if (movePlayed[i, 0] == "O" && movePlayed[i, 1] == "O" && movePlayed[i, 2] == "O") { playerOWon = true; return true; }
            }

            return false;
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private bool CheckLeftDiagonals ()
        { // check the left diagonal 

            if (movePlayed[0, 0] == "X" && movePlayed[1, 1] == "X" && movePlayed[2, 2] == "X") { playerXWon = true; return true; }

            if (movePlayed[0, 0] == "O" && movePlayed[1, 1] == "O" && movePlayed[2, 2] == "O") { playerOWon = true; return true; }

            return false;
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private bool CheckRightDiagonals ()
        { // check the right diagonal 

            if (movePlayed[0, 2] == "X" && movePlayed[1, 1] == "X" && movePlayed[2, 0] == "X") { playerXWon = true; return true; }

            if (movePlayed[0, 2] == "O" && movePlayed[1, 1] == "O" && movePlayed[2, 0] == "O") { playerOWon = true; return true; }

            return false;
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private SolidColorBrush SetForeground ()
        {
            return ( played == "X" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green) );
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private string Played ()
        {
            return ( played == "X" ? "O" : "X" );
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        public void New ()
        {
            int i          = 0;
            total          = 1;
            Result.Content = "";       
            playerXWon     = false;
            playerOWon     = false;
            
            Random rand = new Random();

            played     = ( ( rand.Next(2) == 1 ) ? "X" : "O" );
            movePlayed = new string[3, 3];            

            while (i < board.Children.Count)
            {
                Label temp = board.Children[i] as Label;

                if (temp != null && Result != temp)
                    board.Children.RemoveAt(i);
                else
                    i++;
            }
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private void Play (string option)
        {
            MediaPlayer player = new MediaPlayer();

            player.Open(new Uri(@"Sound\" + option, UriKind.Relative));
            player.Play();
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        public void SetStroke (Rectangle rect)
        {
            /**
                * Apply a white stroke on the rectangle when mouse
                * pointer is entered
                */

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color           = Colors.White;
            rect.StrokeThickness  = 3;
            rect.Stroke           = brush;
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        private Label NewLabel()
        {
            Label move = new Label();

            move.FontSize            = 40;
            move.Content             = played;
            move.FontWeight          = FontWeights.Bold;
            move.Foreground          = SetForeground();
            move.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            move.VerticalAlignment   = System.Windows.VerticalAlignment.Center;

            return move;
        }
        // ****************************************************************************************************************************************





        // ****************************************************************************************************************************************
        public void PlayGame(Rectangle rectangle)
        {
            int row = Grid.GetRow(( rectangle as Rectangle ))    - 1;
            int col = Grid.GetColumn(( rectangle as Rectangle )) - 1;

            if ( movePlayed[row, col] == null && !playerXWon && !playerOWon )
            {
                Play("click.mp3");
                played               = Played();
                movePlayed[row, col] = played;

                Label label = NewLabel();
                board.Children.Add(label);
                Grid.SetColumn(label, ( col + 1 ));
                Grid.SetRow(label,    ( row + 1 ));


                if (( total >= 5 ) && ( CheckRows() || CheckColumns() || CheckLeftDiagonals() || CheckRightDiagonals() ))
                {
                    Result.Foreground = SetForeground();

                    Play("win.wav");

                    if (playerXWon)
                    {
                        Result.Content    = "Player X win!";
                        PlayerOne.Content = ( int.Parse(PlayerOne.Content.ToString()) + 1 );                        
                    }
                    else
                    {
                        Result.Content    = "Player O win!";
                        PlayerTwo.Content = ( int.Parse(PlayerTwo.Content.ToString()) + 1 );
                    }
                }

                total++;

                if (total == 10 && !playerXWon && !playerOWon)
                {
                    
                    Play("nomoves.wav");

                    Result.Foreground = new SolidColorBrush(Colors.Blue);
                    Result.Content    = "No more moves!";
                }
            }
        }
        // ****************************************************************************************************************************************
    }
}
