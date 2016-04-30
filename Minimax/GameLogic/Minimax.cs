using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minimax.GameLogic
{
    public class Minimax
    {
        public static MinimaxResult Do(Board board, char player, int maxDepth, int currentDepth)
        {
            if (board.IsGameOver() || currentDepth == maxDepth)
                return new MinimaxResult(null, board.Evaluate(player));

            Move bestMove = default(Move);

            if (board.CurrentPlayer() == player)
            {
                float bestScore = -1 / 0.0f;

                foreach (Move move in board.GetMoves())
                {
                    Board newBoard = board.MakeBoardWith(move, player);
                    MinimaxResult result = Minimax.Do(newBoard, player, maxDepth, currentDepth + 1);

                    if (result.GetScore() > bestScore)
                    {
                        bestScore = result.GetScore();
                        bestMove = move;
                    }
                }

                return new MinimaxResult(bestMove, bestScore);
            }
            else
            {
                float bestScore = 1 / 0.0f;

                foreach (Move move in board.GetMoves())
                {
                    Board newBoard = board.MakeBoardWith(move, board.CurrentPlayer());
                    MinimaxResult result = Minimax.Do(newBoard, newBoard.CurrentPlayer(), maxDepth, currentDepth + 1);

                    if (result.GetScore() < bestScore)
                    {
                        bestScore = result.GetScore();
                        bestMove = move;
                    }
                }

                return new MinimaxResult(bestMove, bestScore);
            }
        }
    }

    public class MinimaxResult
    {
        Move move;
        float score;

        public MinimaxResult(Move move, float score)
        {
            this.move = move;
            this.score = score;
        }

        public Move GetMove()
        {
            return move;
        }

        public float GetScore()
        {
            return score;
        }
    }
}
