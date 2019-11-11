using System;

namespace csharpai
{
    public class Debug {
        /* Incase we need any debugging */
    }
    public class Board
    {
        /* Our variable assignments just so we can make sure X/O is defined */
        public string AI;
        public string human;

        public string[] genBoard() {
            /* Generate a new, empty board for a game. */
            string[] board = new string[9];
            for(int i = 0; i < 9; i++) {
                board[i] = "O";
            }
            return board;
        }

        public void printBoard(string[] board) {
            Console.WriteLine("┌─┬─┬─┐");
            Console.WriteLine("│{0}│{1}│{2}│", board[0], board[1], board[2]);
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine("│{0}│{1}│{2}│", board[3], board[4], board[5]);
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine("│{0}│{1}│{2}│", board[6], board[7], board[8]);
            Console.WriteLine("└─┴─┴─┘");
        }
    }

    class Game
    {
        static string[] board;

        static int[,] winConditions = new int[8, 3] {
            /* These are the combinations that a player can possibly win in a 3x3 grid */
            { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
            { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
            { 0, 4, 8 }, { 2, 4, 6 }
        };

        public static Board gameBoard = new Board();
        /* Initialize a new game */
        public Game() {
            board = gameBoard.genBoard();
            gameBoard.printBoard(board);
            bool loop = true;

            /* Verify that the square is either X or O */
            while(loop) {
                Console.WriteLine("Would you like to be X or O?");
                gameBoard.human = Console.ReadLine();
                switch(gameBoard.human) {
                    case "X":
                        gameBoard.human = "X";
                        loop = !loop;
                        break;
                    case "O":
                        gameBoard.human = "O";
                        loop = !loop;
                        break;
                    default:
                        Console.WriteLine("Please enter either X or O!");
                        break;
                }
            }

            /* Assign the AI the other space */
            if(gameBoard.human == "X") {
                gameBoard.AI = "O";
            } else {
                gameBoard.AI = "X";
            }

            System.Console.WriteLine("AI: {0}", gameBoard.AI);
            System.Console.WriteLine("Player: {0}", gameBoard.human);
        }

        public void placeMove(string[] board, int move, string player) {
            /* Just a simple function to place where you want it */
            board[move] = player;
        }

        public int checkWin(string[] board, string player) {
            string altplayer;
            if(player == "X") {
                altplayer = "O";
            } else {
                altplayer = "X";
            }
            for(int i = 0; i < 8; i++) {
                /* Check if the player has 3 in a row */
                if(board[winConditions[i, 0]] == player &&
                    board[winConditions[i, 1]] == player &&
                    board[winConditions[i, 2]] == player) {
                        return 10;
                } else if(board[winConditions[i, 0]] == altplayer &&
                    board[winConditions[i, 1]] == altplayer &&
                    board[winConditions[i, 2]] == altplayer) {
                        return -10;
                } else {
                    return 0;
                }   
            }
            return 0;
        }

        public bool checkGameEnd(string[] board) {
            foreach(string p in board) {
                if(p != " ") {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Game game = new Game();
            for(int i = 0; i < 9; i++) {
                board[i] = "X";
            }
            gameBoard.printBoard(board);
        }
    }
}
