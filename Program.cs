using System.Diagnostics;

namespace UI_Zadanie1_c_Bukovska
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Stopwatch stopwatch = new Stopwatch();
                int maxSteps = 20;
                // stopwatch.Start();
                AppFlow.Run("input.txt", "output.txt", maxSteps);
                // stopwatch.Stop();
                // long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                // Console.WriteLine($"Elapsed Time: {elapsedMilliseconds} ms");
                // Console.WriteLine(PuzzleNode.pocet);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred;  " + e.Message);
            }
            
        }
    }
}