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

        public Board(char firstPlayer, char[,] board = null)
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

            currentPlayer = firstPlayer;
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
            if (IsGameOver())
                return;

            Point position = move.GetPosition();
            
            board[position.X, position.Y] = player;

            SwitchPlayerTurn();
        }

        private void SwitchPlayerTurn()
        {
            if (currentPlayer == 'X')
                currentPlayer = 'O';
            else
                currentPlayer = 'X';
        }

        public Board MakeBoardWith(Move move, char player)
        {
            Board newBoard = new Board(player, (char[,])GetBoard().Clone());

            newBoard.MakeMove(move, player);

            return newBoard;
        }

        public float Evaluate(char player)
        {
            if ((player == 'X' && VerifyWinFor('X')) || (player == 'O' && VerifyWinFor('O')))
            {
                return 1;
            }
            else if ((player == 'X' && VerifyWinFor('O')) || (player == 'O' && VerifyWinFor('X')))
            {
                return -1;
            }
                
            return 0;
        }

        public char CurrentPlayer()
        {
            return currentPlayer;
        }

        public bool IsGameOver()
        {
            if (GetGameState() == State.PlayerOWins ||
                GetGameState() == State.PlayerXWins ||
                GetGameState() == State.Draw)
            {
                return true;
            }

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

        public void Reset()
        {
            board = new char[,]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            };
        }
    }
}
