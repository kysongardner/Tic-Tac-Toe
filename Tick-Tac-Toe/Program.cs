using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick_Tac_Toe
{
    class Program
    {
        static void Main()
        {
            bool XTurn = true;
            var myArray = new[]{
		        new []{2,2,2},
		        new []{2,2,2},
		        new []{2,2,2}
	
	            };
            var CurrentPosition = Console.CursorTop;
            
            do
            {
                Console.CursorTop = CurrentPosition;    
                PrintBoard(myArray);
                if(IsGameDraw(myArray))
                {
                    Console.WriteLine("You Both Lose!!!! Press any key to play again".PadRight(100));
                    var PlayAgain = Console.ReadKey(true);
                    Console.Clear();
                    Main();
                    return;
                }
                if(IsGameOver(myArray))
                {
                    Console.WriteLine("You Won!!!! Press any key to play again".PadRight(100));
                    var PlayAgain = Console.ReadKey(true);
                    Console.Clear();
                    Main();
                    return;
                }
                else { 
                    
                    Console.WriteLine("Where would you like to play? Use: Q,W,E  A,S,D  Z,X,C");
                    var UserInput = Console.ReadKey(true);
                    var CurrentSymbol = XTurn ? 0 : 1;
                    if (UserInput.Key == ConsoleKey.Q)
                        {CheckSpot(ref  myArray[0][0], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.W)
                        { CheckSpot(ref  myArray[0][1], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.E)
                        { CheckSpot(ref  myArray[0][2], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.A)
                        { CheckSpot(ref  myArray[1][0], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.S)
                        { CheckSpot(ref  myArray[1][1], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.D)
                        { CheckSpot(ref  myArray[1][2], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.Z)
                        { CheckSpot(ref  myArray[2][0], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.X)
                        { CheckSpot(ref  myArray[2][1], CurrentSymbol); }
                    if (UserInput.Key == ConsoleKey.C)
                        { CheckSpot(ref  myArray[2][2], CurrentSymbol); }
                    XTurn = !XTurn;
                }
            } while (true);
        }

        static void PrintBoard(Int32[][] board)
        {
            var ConsoleTop = Console.CursorTop;
            var ConsoleLeft = Console.CursorLeft;
            for (int Row = 0; Row < board.Length; Row++)
            {
                for (int Col = 0; Col < board[Row].Length; Col++)
                {
                    Console.Write("  {0}  ", PrintTickyTack(board[Row][Col]));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Col == board[Row].Length -1? "":"|");
                }
                Console.WriteLine();
                Console.WriteLine(Row == board.Length -1? "":"-----|-----|----");
            }
            /*Console.WriteLine();
            Console.Write("  {0}", PrintTickyTack(board[0][0])); Console.ForegroundColor = ConsoleColor.White;
            //, PrintTickyTack(board[0][1]), PrintTickyTack(board[0][2]));
            Console.WriteLine("-----|-----|----");
            Console.WriteLine("  {0}  |  {1}  |  {2}", PrintTickyTack(board[1][0]), PrintTickyTack(board[1][1]), PrintTickyTack(board[1][2]));
            Console.WriteLine("-----|-----|----");
            Console.WriteLine("  {0}  |  {1}  |  {2}", PrintTickyTack(board[2][0]), PrintTickyTack(board[2][1]), PrintTickyTack(board[2][2]));
            Console.WriteLine();*/
        }
        static char PrintTickyTack(int TickyTack)
        {

            if (TickyTack == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return 'o';

            }

            if (TickyTack == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return 'x';
            }
            if (TickyTack == 2)
                return ' ';

            return 'b';
        }
        static bool IsGameOver(int[][] board)
        {
            if(board.Any(Row=>Row.All(Col=>Col==0)))
                return true;
            if (board.Any(Row => Row.All(Col => Col == 1)))
                return true;
            for (int i = 0; i < 3; i++)
            {
                if (board.All(Row => Row[i] == 0))
                    return true;
                if (board.All(Row => Row[i] == 1))
                    return true;
            }
            if (board[0][0] != 2 && board[0][0] == board[1][1] && board[0][0] == board[2][2])
            {
                return true;
            }
            if (board[0][2] != 2 && board[0][2] == board[1][1] && board[0][2] == board[2][0])
            {
                return true;
            }
            return false;
        }
        static void CheckSpot(ref int BoardLocation,int newvalue)
        {
            if (BoardLocation != 2)
                return;
            else
            {
                BoardLocation = newvalue;
            }
        }
        static bool IsGameDraw(int[][] CheckDraw)
        {
            if (IsGameOver(CheckDraw) == false && CheckDraw.All(Row => Row.All(Col => Col != 2)))
                return true;
            return false;

        }
    }
}
