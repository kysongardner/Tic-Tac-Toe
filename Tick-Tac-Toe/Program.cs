﻿using System;
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
            TTTEngine MyEngine = new TTTEngine(false);
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
                PrintBoard(MyEngine.CurrentBoardState());
                if (MyEngine.IsGameDraw())
                {
                    Console.WriteLine("You Both Lose!!!! Press any key to play again".PadRight(100));
                    var PlayAgain = Console.ReadKey(true);
                    Console.Clear();
                    Main();
                    return;
                }
                if (MyEngine.IsGameOver())
                {
                    if (MyEngine.isXTurn)
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
                    var CurrentSymbol = MyEngine.isXTurn ? 0 : 1;
                    if (MyEngine.isXTurn && IComputerPlayer)
                    {
                        Thread.Sleep(500);
                        //UserInput = DumbComputer();
                        var ComputerMove = TicTacToePlayer.SmartComputer(MyEngine.CurrentBoardState());
                        UserInput = MapBoardLocationToConsoleKey(ComputerMove.row, ComputerMove.column);
                            
                    }
                    else
                    {
                        UserInput = Console.ReadKey(true).Key;
                    }
                    bool GoodMove = false;
                    if (UserInput == ConsoleKey.Q)
                    { GoodMove = MyEngine.TryMove(0, 0, CurrentSymbol); }
                    if (UserInput == ConsoleKey.W)
                    { GoodMove = MyEngine.TryMove(0, 1, CurrentSymbol); }
                    if (UserInput == ConsoleKey.E)
                    { GoodMove = MyEngine.TryMove(0, 2, CurrentSymbol); }
                    if (UserInput == ConsoleKey.A)
                    { GoodMove = MyEngine.TryMove(1, 0, CurrentSymbol); }
                    if (UserInput == ConsoleKey.S)
                    { GoodMove = MyEngine.TryMove(1, 1, CurrentSymbol); }
                    if (UserInput == ConsoleKey.D)
                    { GoodMove = MyEngine.TryMove(1, 2, CurrentSymbol); }
                    if (UserInput == ConsoleKey.Z)
                    { GoodMove = MyEngine.TryMove(2, 0, CurrentSymbol); }
                    if (UserInput == ConsoleKey.X)
                    { GoodMove = MyEngine.TryMove(2, 1, CurrentSymbol); }
                    if (UserInput == ConsoleKey.C)
                    { GoodMove = MyEngine.TryMove(2, 2, CurrentSymbol); }
                   
                }
            } while (true);
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
        public static Point MapLocationToPoint(int r, int c)
        {
            return new Point(r, c);
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
        
    }
}
