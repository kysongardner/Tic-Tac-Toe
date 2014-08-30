using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tick_Tac_Toe
{
    class Program
    {
        static void Main()
        {
            bool XTurn = false;
            var myArray = new[]{
		        new []{2,2,2},
		        new []{2,2,2},
		        new []{2,2,2}
	
	            };
            bool IComputerPlayer = true;
            bool valid = false;
            Console.Write("Do you want to play the computer? Use y or n:");
            var ComputerPlayer = Console.ReadKey().KeyChar.ToString().ToUpper(); 
            do
            {
                Console.Clear();
                if (ComputerPlayer == "Y")
                {
                    IComputerPlayer = true;
                    valid = true;
                }
                else if (ComputerPlayer == "N")
                {
                    IComputerPlayer = false;
                    valid = true;
                }
                else
                {

                    Console.WriteLine("Please choose either y or n!!!\tDo you want to play Kyson's Brain? Use y or n:");
                    ComputerPlayer = Console.ReadLine().ToUpper();
                }
            }
            while (!valid);

            var CurrentPosition = Console.CursorTop;
            do
            {
                Console.CursorTop = CurrentPosition;
                PrintBoard(myArray);
                if (IsGameDraw(myArray))
                {
                    Console.WriteLine("You Both Lose!!!! Press any key to play again".PadRight(100));
                    var PlayAgain = Console.ReadKey(true);
                    Console.Clear();
                    Main();
                    return;
                }
                if (IsGameOver(myArray))
                {
                    if (XTurn)
                    {
                        Console.WriteLine("X WINS!!!! Press any key to play again".PadRight(100));
                    }
                    else
                    {
                        Console.WriteLine("O WINS!!!! Press any key to play again".PadRight(100));
                    }
                    var PlayAgain = Console.ReadKey(true);
                    Console.Clear();
                    Main();
                    return;
                }
                else
                {




                    Console.WriteLine("Where would you like to play? Use: Q,W,E  A,S,D  Z,X,C");


                    var UserInput = ConsoleKey.Q;
                    var CurrentSymbol = XTurn ? 0 : 1;
                    if (XTurn && IComputerPlayer)
                    {
                        Thread.Sleep(500);
                        //UserInput = DumbComputer();
                        UserInput = SmartComputer(myArray);
                    }
                    else
                    {
                        UserInput = Console.ReadKey(true).Key;
                    }
                    bool GoodMove = false;
                    if (UserInput == ConsoleKey.Q)
                    { GoodMove = CheckSpot(ref  myArray[0][0], CurrentSymbol); }
                    if (UserInput == ConsoleKey.W)
                    { GoodMove = CheckSpot(ref  myArray[0][1], CurrentSymbol); }
                    if (UserInput == ConsoleKey.E)
                    { GoodMove = CheckSpot(ref  myArray[0][2], CurrentSymbol); }
                    if (UserInput == ConsoleKey.A)
                    { GoodMove = CheckSpot(ref  myArray[1][0], CurrentSymbol); }
                    if (UserInput == ConsoleKey.S)
                    { GoodMove = CheckSpot(ref  myArray[1][1], CurrentSymbol); }
                    if (UserInput == ConsoleKey.D)
                    { GoodMove = CheckSpot(ref  myArray[1][2], CurrentSymbol); }
                    if (UserInput == ConsoleKey.Z)
                    { GoodMove = CheckSpot(ref  myArray[2][0], CurrentSymbol); }
                    if (UserInput == ConsoleKey.X)
                    { GoodMove = CheckSpot(ref  myArray[2][1], CurrentSymbol); }
                    if (UserInput == ConsoleKey.C)
                    { GoodMove = CheckSpot(ref  myArray[2][2], CurrentSymbol); }
                    if (GoodMove)
                    {
                        XTurn = !XTurn;
                    }

                }
            } while (true);
        }

        private static ConsoleKey SmartComputer(int[][] board)
        {
            Random rnd = new Random();
            int HowManyPlaysForO = 0;
            int HowManyPlaysForX = 0;

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == 0)
                    {
                        HowManyPlaysForO++;
                    }
                    if (board[i][j] == 1)
                    {
                        HowManyPlaysForX++;
                    }
                }
            }
            if (HowManyPlaysForO + HowManyPlaysForX == 0)
                return ConsoleKey.S;

            if (HowManyPlaysForO + HowManyPlaysForX == 1)
            {
                if (CenterIsOccupied(board))
                {
                    var RandomArray = new[] { ConsoleKey.Q, ConsoleKey.E, ConsoleKey.Z, ConsoleKey.C };
                    return RandomArray[rnd.Next(RandomArray.Length - 1)];

                }
                if (CornerIsOccupied(board))
                {
                    return ConsoleKey.S;
                }
                if (SideIsOccupied(board))
                {
                    // var RandomArray = new[] { ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, ConsoleKey.X };
                    // return RandomArray[rnd.Next(RandomArray.Length - 1)]; 
                    return ConsoleKey.S;
                }
            }

            // check for 2 in a row
            for (int i = 0; i < board.Length; i++)
            {
                var HowManyInRowO = 0;
                var HowManyInRowX = 0;
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == 0)
                    {
                        HowManyInRowO++;
                    }
                    if (board[i][j] == 1)
                    {
                        HowManyInRowX++;
                    }
                }
                if (HowManyInRowO == 2 || HowManyInRowX == 2)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (board[i][j] == 2)
                        {
                            return MapBoardLocationToConsoleKey(i, j);
                        }
                    }
                }
            }
            // Check for 2 in a column
            for (int i = 0; i < board.Length; i++)
            {
                var HowManyInRowO = 0;
                var HowManyInRowX = 0;
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[j][i] == 0)
                    {
                        HowManyInRowO++;
                    }
                    if (board[j][i] == 1)
                    {
                        HowManyInRowX++;
                    }
                }
                if (HowManyInRowO == 2 || HowManyInRowX == 2)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (board[j][i] == 2)
                        {
                            return MapBoardLocationToConsoleKey(j, i);
                        }
                    }
                }
            }

            var backDiagLocations = from spot in new[] {  Tuple.Create(0,0),
                                                            Tuple.Create(1,1),
                                                            Tuple.Create(2,2)
                                                        }
                                    select new { row = spot.Item1, col = spot.Item2 };

            var backDiagO = (from location in backDiagLocations
                             where board[location.row][location.col] == 0
                             select 0).Count();

            var backDiagX = (from location in backDiagLocations
                             where board[location.row][location.col] == 1
                             select 1).Count();


            var frontDiagLocations = from spot in new[] { Tuple.Create(0,2),
                                                            Tuple.Create(1,1),
                                                            Tuple.Create(2,0)}
                                     select new { row = spot.Item1, col = spot.Item2 };

            var frontDiagO = (from location in frontDiagLocations
                              where board[location.row][location.col] == 0
                              select 0).Count();

            var frontDiagX = (from location in frontDiagLocations
                              where board[location.row][location.col] == 1
                              select 1).Count();

            // 2 in a back diag, play missing spot
            if (backDiagO == 2 || backDiagX == 2)
            {
                var spotToPlay = (from location in backDiagLocations
                                  where board[location.row][location.col] == 2
                                  select location).FirstOrDefault();
                if (spotToPlay != null)
                {
                    return MapBoardLocationToConsoleKey(spotToPlay.row, spotToPlay.col);
                }
            }
            // 2 in a front diag, play missing spot
            if (frontDiagO == 2 || frontDiagX == 2)
            {
                var spotToPlay = (from location in frontDiagLocations
                                  where board[location.row][location.col] == 2
                                  select location).FirstOrDefault();
                if (spotToPlay != null)
                {

                    return MapBoardLocationToConsoleKey(spotToPlay.row, spotToPlay.col);

                }

            }
            if (HowManyPlaysForO == 1)
            {
                var EdgeLocations = from spot in new[] {  Tuple.Create(0,1),
                                                            Tuple.Create(1,0),
                                                            Tuple.Create(1,2),
                                                            Tuple.Create(2,1)
                                                        }
                                    select new { row = spot.Item1, col = spot.Item2 };

                var EdgePlayCount = (from location in EdgeLocations
                                     where board[location.row][location.col] != 2
                                     select 0).Count();
                if (EdgePlayCount == 0 && board[1][1] == 0)
                {
                    return PlayAvalibleEdge(board).Value;
                }
                if (EdgePlayCount == 0 && board[1][1] == 1)
                {
                    return PlayAvalibleCorner(board).Value;
                }
                return PlayAvalibleEdge(board).Value;
            }
            var play = PlayAvalibleEdge(board);


            if (play != null)
            {
                return play.Value;
            }

            play = PlayAvalibleCorner(board);

            if (play != null)
            {
                return play.Value;
            }
            return DumbComputer();

        }

        private static ConsoleKey? PlayAvalibleEdge(int[][] board)
        {
            if (board[0][1] == 2)
            {
                return MapBoardLocationToConsoleKey(0, 1);
            }
            if (board[1][2] == 2)
            {
                return MapBoardLocationToConsoleKey(1, 2);
            }
            if (board[1][0] == 2)
            {
                return MapBoardLocationToConsoleKey(1, 0);
            }
            if (board[2][1] == 2)
            {
                return MapBoardLocationToConsoleKey(2, 1);
            }

            return new Nullable<ConsoleKey>();
        }
        private static ConsoleKey? PlayAvalibleCorner(int[][] board)
        {
            if (board[0][0] == 2)
            {
                return MapBoardLocationToConsoleKey(0, 0);
            }
            if (board[0][2] == 2)
            {
                return MapBoardLocationToConsoleKey(0, 2);
            }
            if (board[2][0] == 2)
            {
                return MapBoardLocationToConsoleKey(2, 0);
            }
            if (board[2][2] == 2)
            {
                return MapBoardLocationToConsoleKey(2, 2);
            }

            return new Nullable<ConsoleKey>();
        }

        static ConsoleKey[][] keyMap = new[]{
                new [] {ConsoleKey.Q, ConsoleKey.W, ConsoleKey.E},
                new [] {ConsoleKey.A, ConsoleKey.S, ConsoleKey.D},
                new [] {ConsoleKey.Z, ConsoleKey.X, ConsoleKey.C}
            };
        private static ConsoleKey MapBoardLocationToConsoleKey(int i, int j)
        {
            return keyMap[i][j];
        }

        private static Tuple<int, int> MapConsoleKeyToBoardLocation(ConsoleKey key)
        {

            for (int i = 0; i < keyMap.Length; i++)
            {
                for (int j = 0; j < keyMap[i].Length; j++)
                {
                    if (keyMap[i][j] == key)
                        return Tuple.Create(i, j);
                }
            }
            return Tuple.Create(0, 0);
        }

        private static bool SideIsOccupied(int[][] board)
        {
            return board[0][1] != 2 || board[1][0] != 2 || board[1][2] != 2 || board[2][1] != 2;
        }

        private static bool CornerIsOccupied(int[][] board)
        {
            return board[0][0] != 2 || board[0][2] != 2 || board[2][0] != 2 || board[2][2] != 2;
        }

        private static bool CenterIsOccupied(int[][] board)
        {
            return board[1][1] != 2;
        }

        private static ConsoleKey DumbComputer()
        {
            Random rnd = new Random();
            var RandomNumber = rnd.Next(9);
            var keys = new[] { ConsoleKey.Q, ConsoleKey.W, ConsoleKey.E, ConsoleKey.A, ConsoleKey.S, ConsoleKey.D, ConsoleKey.Z, ConsoleKey.X, ConsoleKey.C };
            return keys[RandomNumber];

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
                    Console.Write(Col == board[Row].Length - 1 ? "" : "|");
                }
                Console.WriteLine();
                Console.WriteLine(Row == board.Length - 1 ? "" : "-----|-----|----");
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
            if (board.Any(Row => Row.All(Col => Col == 0)))
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
        static bool CheckSpot(ref int BoardLocation, int newvalue)
        {
            if (BoardLocation != 2)
                return false;
            else
            {
                BoardLocation = newvalue;
                return true;
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
