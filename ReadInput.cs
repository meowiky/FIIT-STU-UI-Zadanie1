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
                if (lines.Length < 3)
                {
                    Console.WriteLine("Wrong Input, there should be 3 lines");
                    return (null, null);
                }

                string[] size = lines[0].Split('*');
                //ak tam niesu prave 2 cisla, a ak to nahodou niesu cisla
                if (size.Length != 2 || !int.TryParse(size[0], out int rows) || !int.TryParse(size[1], out int columns))
                {
                    Console.WriteLine("Invalid format in the first line. It should be rows*columns");
                    return (null, null);
                }

                if(rows < 2 || columns < 2)
                {
                    Console.WriteLine("Minimum size of puzzle is 2*2");
                    return (null, null);
                }

                string[] inputStartString = lines[1].Split(' ');
                string[] inputFinalString = lines[2].Split(' ');
                //ak je iny pocet vstupnych cisiel ako vstupna velkost hlavolamu
                if (inputStartString.Length != rows * columns || inputFinalString.Length != rows * columns)
                {
                    Console.WriteLine("The number of input numbers must equal the input size");
                    return (null, null);
                }

                int[] inputStartState = new int[rows * columns];
                int[] inputFinalState = new int[rows * columns];

                for (int i = 0; i < rows * columns; i++)
                {
                    if (!int.TryParse(inputStartString[i], out inputStartState[i]))
                    {
                        Console.WriteLine($"Not a number in second line at position {i}.");
                        return (null, null);
                    }
                    if (!int.TryParse(inputFinalString[i], out inputFinalState[i]))
                    {
                        Console.WriteLine($"Not a number in third line at position {i}.");
                        return (null, null);
                    }
                }

                return (new PuzzleNode(inputStartState, rows, columns), new PuzzleNode(inputFinalState, rows, columns));

            }
            catch(Exception ex)
            {
                //ak nastane nahodou nejaka chyba so suborom, nech sa vypise aspon
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (null, null);
            }
        }
    }
}
