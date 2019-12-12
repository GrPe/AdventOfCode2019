using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        static int GRIDSIZE = 50000;

        static void Main(string[] args)
        {
            var data = LoadData();

            bool[,] grid = new bool[GRIDSIZE, GRIDSIZE];
            Pair position = new Pair { x = GRIDSIZE / 2, y = GRIDSIZE / 2 };

            foreach (var instruction in data.Item1)
            {
                switch (instruction.Direction)
                {
                    case 'R':
                        for (int i = position.x + 1; i <= position.x + instruction.Distance; i++)
                            grid[i, position.y] = true;
                        position.x += instruction.Distance;
                        break;
                    case 'L':
                        for (int i = position.x - 1; i >= position.x - instruction.Distance; i--)
                            grid[i, position.y] = true;
                        position.x -= instruction.Distance;
                        break;
                    case 'U':
                        for (int i = position.y - 1; i >= position.y - instruction.Distance; i--)
                            grid[position.x, i] = true;
                        position.y -= instruction.Distance;
                        break;
                    case 'D':
                        for (int i = position.y + 1; i <= position.y + instruction.Distance; i++)
                            grid[position.x, i] = true;
                        position.y += instruction.Distance;
                        break;
                }
            }

            List<Pair> intersection = new List<Pair>();
            position = new Pair { x = GRIDSIZE / 2, y = GRIDSIZE / 2 };
            foreach (var instruction in data.Item2)
            {
                switch (instruction.Direction)
                {
                    case 'R':
                        for (int i = position.x + 1; i <= position.x + instruction.Distance; i++)
                            if (grid[i, position.y]) intersection.Add(new Pair { x = i, y = position.y });
                        position.x += instruction.Distance;
                        break;
                    case 'L':
                        for (int i = position.x - 1; i >= position.x - instruction.Distance; i--)
                            if (grid[i, position.y]) intersection.Add(new Pair { x = i, y = position.y });
                        position.x -= instruction.Distance;
                        break;
                    case 'U':
                        for (int i = position.y - 1; i >= position.y - instruction.Distance; i--)
                            if (grid[position.x, i]) intersection.Add(new Pair { x = position.x, y = i });
                        position.y -= instruction.Distance;
                        break;
                    case 'D':
                        for (int i = position.y + 1; i <= position.y + instruction.Distance; i++)
                            if (grid[position.x, i]) intersection.Add(new Pair { x = position.x, y = i });
                        position.y += instruction.Distance;
                        break;
                }
            }

            int nearest = int.MaxValue;
            position = new Pair { x = GRIDSIZE / 2, y = GRIDSIZE / 2 };
            foreach (var inter in intersection)
            {
                Console.WriteLine($"{inter.x}  {inter.y}");
                nearest = Math.Min(nearest, Distance(inter, position));
            }

            Console.WriteLine(nearest);
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

        private static int Distance(Pair p1, Pair p2)
        {
            return (Math.Abs(p2.x - p1.x) + Math.Abs(p2.y - p1.y));
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

    public struct Pair
    {
        public int x;
        public int y;
    }
}
