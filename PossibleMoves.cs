using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Zadanie1_c_Bukovska
{
    public static class PossibleMoves
    {
        public static Dictionary<(int, int), List<MoveEnum>> possibleMoves = new Dictionary<(int, int), List<MoveEnum>>
        {
            { (0, 0), new List<MoveEnum> { MoveEnum.Left, MoveEnum.Up } },
            { (0, 1), new List<MoveEnum> { MoveEnum.Left, MoveEnum.Right, MoveEnum.Up } },
            { (0, 2), new List<MoveEnum> { MoveEnum.Right, MoveEnum.Up } },
            { (1, 0), new List<MoveEnum> { MoveEnum.Up, MoveEnum.Down, MoveEnum.Right } },
            { (1, 1), new List<MoveEnum> { MoveEnum.Up, MoveEnum.Down, MoveEnum.Right, MoveEnum.Left } },
            { (1, 2), new List<MoveEnum> { MoveEnum.Right, MoveEnum.Up, MoveEnum.Down } },
            { (2, 0), new List<MoveEnum> { MoveEnum.Down, MoveEnum.Left } },
            { (2, 1), new List<MoveEnum> { MoveEnum.Down, MoveEnum.Left, MoveEnum.Right } },
            { (2, 2), new List<MoveEnum> { MoveEnum.Down, MoveEnum.Right } }
        };
    }
}
