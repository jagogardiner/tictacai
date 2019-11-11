using System;

namespace csharpai {
    class AI {
        Game game = new Game();

        string AIPiece;
        /* So we can make some references to the board class */
        private string[] copyBoard(string[] boardToCopy) {
            /* Copy the board so we don't make permament changes */
            string[] copy = new string[9];
            for(int i = 0; i < 9; i++) {
                copy[i] = boardToCopy[i];
            }
            return boardToCopy;
        }

        private int minimax(string[] board, string player) {
            /* The minimax algorithm finds the best spot for the AI to choose. */
            if(player == "X") {
                AIPiece = "O";
            } else {
                AIPiece = "X";
            }

            int score = 0;
            int scoreCount = 0;

            for(int i = 0; i < 9; i++) {
                    if(board[i] != "") {
                        continue;
                    }
                if(game.checkWin(board, player) == -10) {
                    return -10;
                } else {
                    int highScore = 0;
                    int bestSpot = 0;
                    bool firstScore = true;
                    for(int j = 0; j < 9; j++) {
                        int aiScore = aiFindRecurse(board, j, AIPiece);
                        if(firstScore || aiScore > highScore) {
                            firstScore = false;
                            highScore = aiScore;
                            bestSpot = j;
                        }
                    }
                    score += highScore;
                    scoreCount++;
                }
            }

            int adjScore = 0;
            if(scoreCount != 0) {
                adjScore = score / scoreCount;
            }      

            return adjScore;
        }

        private int aiFindRecurse(string[] board, int spot, string AI) {
            board[spot] = AI;

            if(game.checkWin(board, AI) == 10) {
                return 10;
            } else if(game.checkGameEnd(board)) {
                return 0;
            }
            int score = minimax(board, AI);
            return score;
        }
    }
}