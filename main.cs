using System;
using System.Threading;

class Program
{

    public static int grid_limit = 10; // Grid size
    public static int[,] grid = new int[grid_limit, grid_limit]; // Grid displayed
    public static int[,] neighbours = new int[grid_limit, grid_limit]; // Counts neighbouring cells
    public static int fps = 1000; // refersh delay in ms

    public static void Main(string[] args)
    {
        driver();
    }

    /********************* Control Loop *********************/
    public static void driver()
    {
        randomStart(); // Create random starting block
        generateGrid();

        while (true)
        {
            Thread.Sleep(fps); // Delay
            update();
        }
    }

    /********************* Refresh Grid *********************/
    public static void update()
    {
        countNeighbours();
        updateGrid();
        generateGrid();
    }

    /********************* Update Grid Based Off Neighbours *********************/
    public static void updateGrid()
    {
        for (int i = 0; i < grid_limit; i++)
        {
            for (int j = 0; j < grid_limit; j++)
            {

                if (neighbours[i, j] <= 1) grid[i, j] = 0;
                else if (neighbours[i, j] >= 4) grid[i, j] = 0;
                else if (neighbours[i, j] == 3) grid[i, j] = 1;

            }
        }
    }

    /********************* Count Neighbours For Each Cell *********************/
    public static void countNeighbours()
    {

        for (int i = 0; i < grid_limit; i++)
        {
            for (int j = 0; j < grid_limit; j++)
            {

                int count = 0;

                // Top Left
                if (i > 0 && j > 0 && grid[i - 1, j - 1] == 1) count++;

                // Top
                if (i > 0 && grid[i - 1, j] == 1) count++;

                // Top Right
                if (i > 0 && j + 1 < grid_limit && grid[i - 1, j + 1] == 1) count++;

                // Left
                if (j > 0 && grid[i, j - 1] == 1) count++;

                // Right
                if (j + 1 < grid_limit && grid[i, j + 1] == 1) count++;

                // Bottom Left
                if (i + 1 < grid_limit && j > 0 && grid[i + 1, j - 1] == 1) count++;

                // Bottom
                if (i + 1 < grid_limit && grid[i + 1, j] == 1) count++;

                // Bottom Right
                if (i + 1 < grid_limit && j + 1 < grid_limit && grid[i + 1, j + 1] == 1) count++;

                neighbours[i, j] = count;
            }
        }
    }

    /********************* Print Out Grid *********************/
    public static void generateGrid()
    {
        for (int i = 0; i < grid_limit; i++)
        {
            for (int j = 0; j < grid_limit; j++)
            {
				if (grid[i, j] == 1) {
					Console.BackgroundColor = ConsoleColor.Red;
					Console.ForegroundColor = ConsoleColor.Red;
                	Console.Write(grid[i, j] + " ");
				} else {
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.Black;
                	Console.Write(grid[i, j] + " ");
				}
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /********************* Get Random 5 Starting Cells *********************/
    public static void randomStart()
    {
        Random rand = new Random();

        int[] positions = new int[5];

        for (int i = 0; i < positions.Length; i++)
        {
            int testPos = rand.Next(0, 9);

            if (arrContainsNum(testPos, positions))
            {
                i--;
            }
            else
            {
                positions[i] = testPos;
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            grid[(positions[i] / 3) + 3, (positions[i] % 3) + 3] = 1;
        }
    }

    /********************* Check For Duplicates *********************/
    public static bool arrContainsNum(int testNum, int[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (testNum == positions[i]) return true;
        }
        return false;
    }
}
