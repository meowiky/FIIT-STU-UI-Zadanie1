﻿namespace UI_Zadanie1_c_Bukovska
{
    public class Program
    {
        static void Main(string[] args)
        {
            //int[] input = { 8, 7, 3, 0, 2, 1, 4, 6, 5 };
            //PuzzleNode skuska = new PuzzleNode(input, 3, 3);
            //List<MoveEnum> possible = skuska.GetPossibleMoves();
            //(int[,], int[]) next = skuska.GetNewState(MoveEnum.Up);
            //Console.WriteLine(skuska.GetPossibleMoves());

            var (startingNode, finalNode) = ReadInput.GetInputFromTXT();
            Console.WriteLine(startingNode.Columns);
        }
    }
}