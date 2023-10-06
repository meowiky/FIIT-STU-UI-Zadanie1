namespace UI_Zadanie1_c_Bukovska
{
    public class Program
    {
        static void Main(string[] args)
        {
            var (startingNode, finalNode) = GetInputFromTXT();

            List<PuzzleNode> parentsFromStart = new List<PuzzleNode>();
            List<PuzzleNode> childrenfromStart = new List<PuzzleNode>();

            List<PuzzleNode> parentsFromEnd = new List<PuzzleNode>();
            List<PuzzleNode> childrenfromEnd = new List<PuzzleNode>();

            parentsFromStart.Add(startingNode);
            parentsFromEnd.Add(finalNode);

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
        }

        static bool IsEqualState(PuzzleNode a, PuzzleNode b)
        {
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    if (a.State[i, j] != b.State[i, j])
                        return false;
                }
            }

            return true;
        }

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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (null, null);
            }
        }
    }
}