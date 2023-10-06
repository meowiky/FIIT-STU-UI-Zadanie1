using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Zadanie1_c_Bukovska
{
    public class PuzzleNode
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int[,] State { get; set; }
        public List<MoveEnum> Moves { get; set; }
        public int[] SpacePosition { get; set; }

        public PuzzleNode(int[,] state, int rows, int columns, int[] spacePosition, List<MoveEnum> moves)
        {
            State = new int[rows, columns];
            Rows = rows;
            Columns = columns;
        
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    State[i, j] = state[i, j];
                }
            }

            SpacePosition = spacePosition.ToArray();

            Moves = new List<MoveEnum>(moves);
        }

        public PuzzleNode(int[,] state, int rows, int columns)
        {
            State = new int[rows, columns];
            Rows = rows;
            Columns = columns;
            SpacePosition = new int[2];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    State[i, j] = state[i, j];

                    if (state[i, j] == 0)
                    {
                        SpacePosition[0] = i;
                        SpacePosition[1] = j;
                    }
                }
            }

            Moves = new List<MoveEnum>();
        }

        public List<MoveEnum> GetPossibleMoves()
        {
            List<MoveEnum> possibleMoves = new List<MoveEnum>();
            MoveEnum lastMove;
            if (Moves.Count != 0)
                lastMove = Moves.Last();
            else
                lastMove = MoveEnum.Possible;

            // left
            if ((SpacePosition[1] + 1 < Columns) && (lastMove != MoveEnum.Right))
                possibleMoves.Add(MoveEnum.Left);
            // right
            if ((SpacePosition[1] - 1 >= 0) && (lastMove != MoveEnum.Left))
                possibleMoves.Add(MoveEnum.Right);
            //up
            if ((SpacePosition[0] + 1 < Rows) && (lastMove != MoveEnum.Down))
                possibleMoves.Add(MoveEnum.Up);
            //down
            if ((SpacePosition[0] - 1 >= 0) && (lastMove != MoveEnum.Up))
                possibleMoves.Add(MoveEnum.Down);

            return possibleMoves;
        }

        public PuzzleNode GetNewState(MoveEnum move)
        {
            int[,] newState = new int[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newState[i, j] = State[i, j];
                }
            }

            int tomoveX = -1;
            int tomoveY = -1;

            switch (move) 
            {
                case MoveEnum.Up:
                    tomoveX = SpacePosition[0] + 1;
                    tomoveY = SpacePosition[1];
                    break;
                case MoveEnum.Down:
                    tomoveX = SpacePosition[0] - 1;
                    tomoveY = SpacePosition[1];
                    break;
                case MoveEnum.Left:
                    tomoveX = SpacePosition[0];
                    tomoveY = SpacePosition[1] - 1;
                    break;
                case MoveEnum.Right:
                    tomoveX = SpacePosition[0];
                    tomoveY = SpacePosition[1] + 1;
                    break;
            }

            newState[SpacePosition[0], SpacePosition[1]] = State[tomoveX, tomoveY];
            newState[tomoveX, tomoveY] = State[SpacePosition[0], SpacePosition[1]];

            int[] spacePosition = new int[2];
            spacePosition[0] = tomoveX;
            spacePosition[1] = tomoveY;

            List<MoveEnum> newMoves = new List<MoveEnum>(Moves);
            newMoves.Add(move);

            return new PuzzleNode(newState, Rows, Columns, spacePosition, newMoves);
        }

        public bool IsEqualState(PuzzleNode other)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (State[i, j] != other.State[i, j])
                        return false;
                }
            }

            return true;
        }
    }
}
