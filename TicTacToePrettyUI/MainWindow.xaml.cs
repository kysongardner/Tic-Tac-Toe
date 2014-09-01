using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using Tick_Tac_Toe;

namespace TicTacToePrettyUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        TTTEngine MyTicTacToeBoard = new TTTEngine(false);
        public MainWindow()
        {
            
            InitializeComponent();
            UpdateGameState();
            this.DataContext = this;


        }

        public int[][] GameState
        {
            get;
            set;
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            MyTicTacToeBoard = new TTTEngine(false);
            UpdateGameState();
        }
        public void UpdateGameState()
        {
            this.GameState = MyTicTacToeBoard.CurrentBoardState();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("GameState"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void TicTacClick(object sender, RoutedEventArgs e)
        {
            var Button = sender as Button;
            var Position = Button.Tag.ToString().Split(',');
            var MyPoint = new Tick_Tac_Toe.Point(Convert.ToInt32(Position[0]), Convert.ToInt32(Position[1]));
           if( MyTicTacToeBoard.TryMove(MyPoint.row, MyPoint.column, 1))
           {
               var ComputerMove = TicTacToePlayer.SmartComputer(MyTicTacToeBoard.CurrentBoardState());
               MyTicTacToeBoard.TryMove(ComputerMove.row, ComputerMove.column, 0);
           }
            UpdateGameState();
        }
       
    }
}
