using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Zadanie1_c_Bukovska
{
    public class PuzzleNode
    {
        public int[,] State{get; set;}
        public List<MoveEnum> Moves { get; set; }
        public int[] SpaceIndex { get; set; }

        public PuzzleNode()
        {
            State = new int[3,3];
            Moves = new List<MoveEnum>();
            SpaceIndex = new int[2];
        }

        public PuzzleNode(int[] input)
            :this() 
        {
            int index = 0;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    State[row, col] = input[index];
                    if (input[index] == 0)
                    {
                        SpaceIndex[0] = row;
                        SpaceIndex[0] = col;
                    }
                    index++;
                }
            }
        }

        public List<MoveEnum> GetPossibleMoves()
        {
            return PossibleMoves.possibleMoves[(SpaceIndex[0], SpaceIndex[0])];
        }
    }
}
