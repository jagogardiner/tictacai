 
using System;

namespace csharpai
{
    class AI
    {
        public AI() { }

        public int aiTurn(string[] board, string aiToken)
        {
            float[] scores = new float[9];
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != " ")
                {
                    scores[i] = -1000;
                    continue;
                }
                scores[i] = aiFindRecurse(board, i, aiToken);
            }
            int spot = 0;
            float highscore = 0;
            bool firstScore = true;
            for (int i = 0; i < 9; i++)
            {
                if (scores[i] == -1000)
                {
                    continue;
                }
                if (firstScore || scores[i] > highscore)
                {
                    highscore = scores[i];
                    spot = i;
                    firstScore = false;
                }
            }

            return spot;
        }

        private float aiFindRecurse(string[] board, int spot, string aiToken)
        {
            string[] boardCopy = copyBoard(board);

            boardCopy[spot] = aiToken;
            if (checkWin(boardCopy, aiToken))
            {
                return 10;
            }
            else if (checkDraw(boardCopy))
            {
                return 0;
            }

            float score = fakePlayer(boardCopy, aiToken);
            return score;
        }

        private float fakePlayer(string[] board, string aiToken)
        {
            string playerToken;
            if (aiToken == "X")
            {
                playerToken = "O";
            }
            else
            {
                playerToken = "X";
            }

            float score = 0;
            int scoreCount = 0;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != " ")
                {
                    continue;
                }
                string[] boardCopy = copyBoard(board);
                boardCopy[i] = playerToken;
                if (checkWin(boardCopy, playerToken))
                {
                    return -10;
                }
                else
                {
                    float highScore = 0;
                    int bestSpot = 0;
                    bool firstScore = true;
                    for (int j = 0; j < 9; j++)
                    {
                        if (boardCopy[j] != " ")
                        {
                            continue;
                        }
                        var aiScore = aiFindRecurse(boardCopy, j, aiToken);
                        if(firstScore || aiScore>highScore) {
                            firstScore = false;
                            highScore = aiScore;
                            bestSpot=j;
                        }
                    }
                    score+=highScore;
                    scoreCount++;
                }
            }
    
            float adjustedScore = 0;
            if( scoreCount!=0) {
                adjustedScore = score / scoreCount;
            }

            return adjustedScore;
        }

        private bool checkWin(string[] board, string playerToken)
        {
            int[,] winConditions = new int[8, 3] {
                /* These are the combinations that a player can possibly win in a 3x3 grid */
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                { 0, 4, 8 }, { 2, 4, 6 }
            };
            for (int i = 0; i < 8; i++)
            {
                int num = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (board[winConditions[i, j]] == playerToken)
                    {
                        num++;
                    }
                }
                if (num == 3)
                {
                    return true;
                }
            }

            return false;
        }


        private bool checkDraw(string[] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == " ")
                {
                    return false;
                }
            }
            return true;
        }

        private string[] copyBoard(string[] board)
        {
            string[] copy = new string[9];
            for (int i = 0; i < 9; i++)
            {
                copy[i] = board[i];
            }

            return copy;
        }
    }
}