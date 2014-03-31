using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game play;

        public MainWindow ()
        {
            InitializeComponent();

            play = new Game(board, Result, PlayerOne, PlayerTwo);
        }
       
        private void Rectangle_MouseEnter (object sender, MouseEventArgs e)
        {
            play.SetStroke(sender as Rectangle);
        }

        private void Rectangle_MouseLeave (object sender, MouseEventArgs e)
        {
            ( sender as Rectangle ).Stroke = null;
        }

        private void Rectangle_MouseDown (object sender, MouseButtonEventArgs e)
        {
            play.PlayGame(sender as Rectangle);
        }

        private void Button_Click (object sender, RoutedEventArgs e)
        {
            play.New();
        }
    }
}
