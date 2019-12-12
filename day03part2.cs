using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03_Part2
{
    class Program
    {
        static int GRIDSIZE = 50000;

        static void Main(string[] args)
        {
            var data = LoadData();

            int[,] grid = new int[GRIDSIZE, GRIDSIZE];
            Result position = new Result { x = GRIDSIZE / 2, y = GRIDSIZE / 2 };

            int pathLenght = 0;
            foreach (var instruction in data.Item1)
            {
                switch (instruction.Direction)
                {
                    case 'R':
                        for (int i = position.x + 1; i <= position.x + instruction.Distance; i++)
                            grid[i, position.y] = pathLenght++;
                        position.x += instruction.Distance;
                        break;
                    case 'L':
                        for (int i = position.x - 1; i >= position.x - instruction.Distance; i--)
                            grid[i, position.y] = pathLenght++;
                        position.x -= instruction.Distance;
                        break;
                    case 'U':
                        for (int i = position.y - 1; i >= position.y - instruction.Distance; i--)
                            grid[position.x, i] = pathLenght++;
                        position.y -= instruction.Distance;
                        break;
                    case 'D':
                        for (int i = position.y + 1; i <= position.y + instruction.Distance; i++)
                            grid[position.x, i] = pathLenght++;
                        position.y += instruction.Distance;
                        break;
                }
            }

            pathLenght = 0;
            List<Result> intersection = new List<Result>();
            position = new Result { x = GRIDSIZE / 2, y = GRIDSIZE / 2 };
            foreach (var instruction in data.Item2)
            {
                switch (instruction.Direction)
                {
                    case 'R':
                        for (int i = position.x + 1; i <= position.x + instruction.Distance; i++)
                        {
                            pathLenght++;
                            if (grid[i, position.y] > 0) intersection.Add(new Result { x = i, y = position.y, distance = pathLenght + grid[i, position.y] });
                        }
                        position.x += instruction.Distance;
                        break;
                    case 'L':
                        for (int i = position.x - 1; i >= position.x - instruction.Distance; i--)
                        {
                            pathLenght++;
                            if (grid[i, position.y] > 0) intersection.Add(new Result { x = i, y = position.y, distance = pathLenght + grid[i, position.y] });
                        }
                        position.x -= instruction.Distance;
                        break;
                    case 'U':
                        for (int i = position.y - 1; i >= position.y - instruction.Distance; i--)
                        {
                            pathLenght++;
                            if (grid[position.x, i] > 0) intersection.Add(new Result { x = position.x, y = i, distance = pathLenght + grid[position.x, i] });
                        }
                        position.y -= instruction.Distance;
                        break;
                    case 'D':
                        for (int i = position.y + 1; i <= position.y + instruction.Distance; i++)
                        {
                            pathLenght++;
                            if (grid[position.x, i] > 0) intersection.Add(new Result { x = position.x, y = i, distance = pathLenght + grid[position.x, i] });
                        }
                        position.y += instruction.Distance;
                        break;
                }
            }

            int nearest = int.MaxValue;
            position = new Result { x = GRIDSIZE / 2, y = GRIDSIZE / 2 };
            foreach (var inter in intersection)
            {
                Console.WriteLine($"{inter.x}  {inter.y}");
                nearest = Math.Min(nearest, inter.distance);
            }

            Console.WriteLine(nearest + 1);
        }

        private static Tuple<Instruction[], Instruction[]> LoadData()
        {
            using StreamReader reader = new StreamReader("data.txt");

            string[] first = reader.ReadLine().Split(',');
            string[] second = reader.ReadLine().Split(',');

            Instruction[] instructionsSetOne = (from x in first select new Instruction(x)).ToArray();
            Instruction[] instructionsSetTwo = (from x in second select new Instruction(x)).ToArray();

            return new Tuple<Instruction[], Instruction[]>(instructionsSetOne, instructionsSetTwo);
        }
    }

    public struct Instruction
    {
        public int Distance { get; set; }
        public char Direction { get; set; }

        public Instruction(string data)
        {
            Direction = data[0];
            Distance = int.Parse(data[1..]);
        }
    }

    public struct Result
    {
        public int x;
        public int y;
        public int distance;
    }
}
