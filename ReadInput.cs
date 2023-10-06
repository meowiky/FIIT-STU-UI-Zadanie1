using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Zadanie1_c_Bukovska
{
    public static class ReadInput
    {
        public static (PuzzleNode, PuzzleNode) GetInputFromTXT()
        {
            string filePath = "input.txt";

            try
            {
                string[] lines = File.ReadAllLines(filePath);

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
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (null, null);
            }
        }
    }
}
