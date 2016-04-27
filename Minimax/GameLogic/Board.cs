using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Minimax.GameLogic
{
    public class Board
    {
        char[,] board;
        char currentPlayer;
        bool isGameOver;

        public enum State { PlayerXWins, PlayerOWins, Draw, Playing };

        public Board(char[,] board = null)
        {
            if (board == null)
                this.board = new char[,]
                {
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' }
                };
            else
                this.board = board;
        }

        public char[,] GetBoard()
        {
            return board;
        }

        public List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (this.board[x, y] == ' ')
                        moves.Add(new Move(new Point(x, y)));
                }
            }

            return moves;
        }

        public void MakeMove(Move move, char player)
        {
            Point position = move.GetPosition();

            board[position.X, position.Y] = player;
        }

        public float Evaluate(char player)
        {
            float infinity = 1 / 0.0f;

            return infinity;
        }

        public char CurrentPlayer()
        {
            return currentPlayer;
        }

        public bool IsGameOver()
        {
            if (GetMoves().Count == 0)
                return true;

            return false;
        }

        public State GetGameState()
        {
            if (VerifyWinFor('X'))
                return State.PlayerXWins;

            if (VerifyWinFor('O'))
                return State.PlayerOWins;

            if (GetMoves().Count == 0)
                return State.Draw;

            return State.Playing;
        }

        private bool VerifyWinFor(char player)
        {
            for (int x = 0; x < 3; x++)
                if ((board[x, 0] == player) &&
                    (board[x, 1] == player) &&
                    (board[x, 2] == player))
                    return true;

            for (int x = 0; x < 3; x++)
                if ((board[0, x] == player) &&
                    (board[1, x] == player) &&
                    (board[2, x] == player))
                    return true;

            if ((board[0, 0] == player) &&
                (board[1, 1] == player) &&
                (board[2, 2] == player))
                return true;

            if ((board[0, 2] == player) &&
                (board[1, 1] == player) &&
                (board[2, 0] == player))
                return true;

            return false;
        }
    }
}
