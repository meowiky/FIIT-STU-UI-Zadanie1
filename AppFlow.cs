using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UI_Zadanie1_c_Bukovska
{
   public static class AppFlow
   {
        public static void Run(string inputFile, string outputFile, int maxSteps)
        {
            var (startingNode, finalNode) = GetInputFromTXT(inputFile);

            if (startingNode.IsEqualState(finalNode))
            {
                if (File.Exists(outputFile))
                    File.Delete(outputFile);
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    writer.WriteLine("No moves needed");
                }
            }

            List<PuzzleNode> parentsFromStart = new List<PuzzleNode>();
            List<PuzzleNode> childrenfromStart = new List<PuzzleNode>();

            List<PuzzleNode> parentsFromEnd = new List<PuzzleNode>();
            List<PuzzleNode> childrenfromEnd = new List<PuzzleNode>();

            parentsFromStart.Add(startingNode);
            parentsFromEnd.Add(finalNode);

            for (int i = 0; i < maxSteps; i++)
            {

                foreach (PuzzleNode node in parentsFromStart)
                {
                    List<MoveEnum> possibleMoves = node.GetPossibleMoves();
                    foreach (MoveEnum move in possibleMoves)
                    {
                        childrenfromStart.Add(node.GetNewState(move));
                    }
                }

                foreach (PuzzleNode node in parentsFromEnd)
                {
                    List<MoveEnum> possibleMoves = node.GetPossibleMoves();
                    foreach (MoveEnum move in possibleMoves)
                    {
                        childrenfromEnd.Add(node.GetNewState(move));
                    }
                }

                foreach (PuzzleNode nodeA in childrenfromStart)
                {
                    foreach (PuzzleNode nodeB in childrenfromEnd)
                    {
                        if (nodeA.IsEqualState(nodeB))
                        {
                            ReturnFoundResult(nodeA, nodeB, outputFile);
                            return;
                        }
                    }
                    foreach (PuzzleNode nodeC in parentsFromEnd)
                    {
                        if (nodeA.IsEqualState(nodeC))
                        {
                            ReturnFoundResult(nodeA, nodeC, outputFile);
                            return;
                        }
                    }
                }

                parentsFromStart.Clear();
                parentsFromEnd.Clear();
                parentsFromStart = new List<PuzzleNode>(childrenfromStart);
                parentsFromEnd = new List<PuzzleNode>(childrenfromEnd);
                childrenfromStart.Clear();
                childrenfromEnd.Clear();
            }

            if (File.Exists(outputFile))
                File.Delete(outputFile);
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine($"After {maxSteps} steps, the program still hasn't found the right steps");
            }
        }

        public static (PuzzleNode, PuzzleNode) GetInputFromTXT(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);

            string[] dimensions = lines[0].Split('*');
            int rows = int.Parse(dimensions[0]);
            int columns = int.Parse(dimensions[1]);

            string[] startString = lines[1].Split(' ');
            string[] finalString = lines[2].Split(' ');

            int[,] start = new int[rows, columns];
            int[,] final = new int[rows, columns];

            int startIndex = 0;
            int finalIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    start[i, j] = int.Parse(startString[startIndex]);
                    final[i, j] = int.Parse(finalString[finalIndex]);
                    startIndex++;
                    finalIndex++;
                }
            }

            return (new PuzzleNode(start, rows, columns), new PuzzleNode(final, rows, columns));
        }

        public static void ReturnFoundResult(PuzzleNode a, PuzzleNode b, string outputFile)
        {
            b.Moves.Reverse();
            foreach (MoveEnum move in b.Moves)
            {
                switch (move)
                {
                    case MoveEnum.Up:
                        a.Moves.Add(MoveEnum.Down);
                        break;
                    case MoveEnum.Down:
                        a.Moves.Add(MoveEnum.Up);
                        break;
                    case MoveEnum.Left:
                        a.Moves.Add(MoveEnum.Right);
                        break;
                    case MoveEnum.Right:
                        a.Moves.Add(MoveEnum.Left);
                        break;
                    default:
                        break;
                }
            }

            string result = string.Join(", ", a.Moves.Select(m => m.ToString()));

            if (File.Exists(outputFile))
                File.Delete(outputFile);
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine(result);
            }
        }
   }
}
