using System;
using System.IO;

namespace Day01_Part2
{
    class Program
    {
        private static int DATASIZE = 100;

        static void Main(string[] args)
        {
            var data = LoadData();
            int sum = 0;

            foreach (int mass in data)
            {
                int fuel = mass;
                int totalFuel = 0;
                for(; ; )
                {
                    fuel = (fuel / 3) - 2;
                    if (fuel <= 0) break;
                    totalFuel += fuel;
                }
                sum += totalFuel;
            }

            Console.WriteLine(sum);
        }

        private static int[] LoadData()
        {
            using StreamReader streamReader = new StreamReader("data.txt");

            int[] data = new int[DATASIZE];
            for (int i = 0; i < DATASIZE; i++)
                data[i] = int.Parse(streamReader.ReadLine());

            return data;
        }
    }
}
