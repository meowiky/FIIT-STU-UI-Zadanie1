namespace UI_Zadanie1_c_Bukovska
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int maxSteps = 10;
                AppFlow.Run("input.txt", "output.txt", maxSteps);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred;  " + e.Message);
            }
            
        }
    }
}