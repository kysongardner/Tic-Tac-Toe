using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tick_Tac_Toe
{
    public class TTTEngine
    {
        public bool isXTurn
        {
            get
            {
                return XTurn;
            }
        }
        bool XTurn = false;
        int[][] boardState = new[]{
		        new []{2,2,2},
		        new []{2,2,2},
		        new []{2,2,2}
	
	            };
        public int[][] CurrentBoardState()
        {
            var result = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                result[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    result[i][j] = boardState[i][j];
                }
            }
            return result;

        }
        public TTTEngine(bool Turn)
        {
            XTurn = Turn;
        }
        public bool IsGameOver()
        {
            return IsGameOver(boardState);
        }
        public static bool IsGameOver(int[][] board)
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
        public bool TryMove(int row, int column, int newValue)
        
        {
            var GoodMove = CheckSpot(ref boardState[row][column], newValue);
            if (GoodMove)
            {
                XTurn = !XTurn;
            }
            return GoodMove;
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
        public bool IsGameDraw()
        {
            return IsGameDraw(boardState);
        }
        public static bool IsGameDraw(int[][] CheckDraw)
        {
            if (IsGameOver(CheckDraw) == false && CheckDraw.All(Row => Row.All(Col => Col != 2)))
                return true;
            return false;

        }
    }
}
