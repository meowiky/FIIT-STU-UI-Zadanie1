using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Zadanie1_c_Bukovska
{
    public class PuzzleNode
    {
        public int[,] State { get; set; }
        public List<MoveEnum> Moves { get; set; }
        public int[] SpacePosition { get; set; }

        public PuzzleNode()
        {
            State = new int[3, 3];
            Moves = new List<MoveEnum>();
            SpacePosition = new int[2];
        }

        public PuzzleNode(int[] input)
            : this()
        {
            int index = 0;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    State[row, col] = input[index];
                    if (input[index] == 0)
                    {

                        SpacePosition[0] = row;
                        SpacePosition[0] = col;
                    }
                    index++;
                }
            }
        }

        public PuzzleNode(int[,] state, int[] spacePosition)



        public List<MoveEnum> GetPossibleMoves()
        {
            return PossibleMoves.possibleMoves[(SpacePosition[0], SpacePosition[0])];
        }

        public (int[,], int[]) GetNewState(MoveEnum move)
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

            return (newState, spacePosition);
        }
    }
}
