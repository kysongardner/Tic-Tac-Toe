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
            IsShowingNewGameScreen = Visibility.Visible;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsShowingNewGameScreen"));
            }

        }

        public int[][] GameState
        {
            get;
            set;
        }
        public string ReturnStatus
        {
            get;
            set;
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            MyTicTacToeBoard = new TTTEngine(false);
            UpdateGameState();
            IsShowingNewGameScreen = Visibility.Visible;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsShowingNewGameScreen"));
            }
        }
        public void UpdateGameState()
        {

            this.GameState = MyTicTacToeBoard.CurrentBoardState();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("GameState"));
            }
            //If the game is a draw then Say you both lost Otherwise if the game is over say someone won
            if (MyTicTacToeBoard.IsGameDraw())
                this.ReturnStatus = "You Both Lost!";
            //X Will never win versus the computer so there is no reason to have X Wins right here. 
            else if (MyTicTacToeBoard.IsGameOver())
            {
                if (MyTicTacToeBoard.isXTurn)
                {
                    this.ReturnStatus = " X Wins!!!!";
                }
                else
                {
                    this.ReturnStatus = " O Wins!!!!";
                }
            }

            else
            {
                this.ReturnStatus = "";
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ReturnStatus"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void TicTacClick(object sender, RoutedEventArgs e)
        {
            if (MyTicTacToeBoard.IsGameOver() == true)
                return;
            var Button = sender as Button;
            var Position = Button.Tag.ToString().Split(',');
            var MyPoint = new Tick_Tac_Toe.Point(Convert.ToInt32(Position[0]), Convert.ToInt32(Position[1]));

            var Turn = MyTicTacToeBoard.isXTurn ? 0 : 1;
            if (MyTicTacToeBoard.TryMove(MyPoint.row, MyPoint.column, Turn))
            {
                if (IsPlayingComputer)
                {

                    var ComputerMove = TicTacToePlayer.SmartComputer(MyTicTacToeBoard.CurrentBoardState());
                    MyTicTacToeBoard.TryMove(ComputerMove.row, ComputerMove.column, 0);
                }

            }

            UpdateGameState();
        }

        public bool IsPlayingComputer
        {
            get;
            set;
        }

        public Visibility IsShowingNewGameScreen
        {
            get;
            set;
        }

        private void PlayTheComputer(object sender, RoutedEventArgs e)
        {
            this.IsPlayingComputer = true;
            IsShowingNewGameScreen = Visibility.Collapsed;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsShowingNewGameScreen"));
            }
        }

        private void PlayAnotherPerson(object sender, RoutedEventArgs e)
        {
            this.IsPlayingComputer = false;
            IsShowingNewGameScreen = Visibility.Collapsed;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsShowingNewGameScreen"));
            }
        }



    }
}
