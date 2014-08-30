using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick_Tac_Toe
{
   public class TicTacToePlayer
    {
       private static Point DumbComputer()
       {
           Random rnd = new Random();
           var RandomNumber = rnd.Next(9);
           var keys = new[] { new Point(0, 0), new Point(0, 1), new Point(0, 2),
                                new Point(1,0), new Point(1,1), new Point(1,2),
                                new Point(2,0), new Point(2,1), new Point(2,2)};
           return keys[RandomNumber];

       }
     public static Point SmartComputer(int[][] board)
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
               return new Point(1, 1);

           if (HowManyPlaysForO + HowManyPlaysForX == 1)
           {
               if (CenterIsOccupied(board))
               {
                   var RandomArray = new[] { new Point(0, 0), new Point(0, 2), new Point(2, 0), new Point(2, 2) };
                   return RandomArray[rnd.Next(RandomArray.Length - 1)];

               }
               if (CornerIsOccupied(board))
               {
                   return new Point(1, 1);
               }
               if (SideIsOccupied(board))
               {
                   // var RandomArray = new[] { ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, ConsoleKey.X };
                   // return RandomArray[rnd.Next(RandomArray.Length - 1)]; 
                   return new Point(1, 1);
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
                           return new Point(i, j);
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
                           return new Point(j, i);
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
                   return new Point(spotToPlay.row, spotToPlay.col);
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

                   return new Point(spotToPlay.row, spotToPlay.col);

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
                   return PlayAvalibleEdge(board);
               }
               if (EdgePlayCount == 0 && board[1][1] == 1)
               {
                   return PlayAvalibleCorner(board);
               }
               return PlayAvalibleEdge(board);
           }
           var play = PlayAvalibleEdge(board);


           if (play != null)
           {
               return play;
           }

           play = PlayAvalibleCorner(board);

           if (play != null)
           {
               return play;
           }
           return DumbComputer();

       }
       private static Point PlayAvalibleEdge(int[][] board)
       {
           if (board[0][1] == 2)
           {
               return new Point(0, 1);
           }
           if (board[1][2] == 2)
           {
               return new Point(1, 2);
           }
           if (board[1][0] == 2)
           {
               return new Point(1, 0);
           }
           if (board[2][1] == 2)
           {
               return new Point(2, 1);
           }

           return null;
       }
       public static Point PlayAvalibleCorner(int[][] board)
       {
           if (board[0][0] == 2)
           {
               return new Point(0, 0);
           }
           if (board[0][2] == 2)
           {
               return new Point(0, 2);
           }
           if (board[2][0] == 2)
           {
               return new Point(2, 0);
           }
           if (board[2][2] == 2)
           {
               return new Point(2, 2);
           }

           return null;
       }

       public static bool SideIsOccupied(int[][] board)
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
    }
}
