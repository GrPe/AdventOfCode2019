using System;
using System.IO;
using System.Linq;

namespace Day02_Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = LoadData();
            data[1] = 12;
            data[2] = 2;

            for(int i = 0; i < 100; i++)
            {
                for(int j = 0; j < 100; j++)
                {
                    int[] array = new int[data.Length];
                    Array.Copy(data, 0, array, 0, data.Length);
                    array[1] = i;
                    array[2] = j;

                    int output = ProcessData(array);
                    if(output == 19690720)
                    {
                        Console.WriteLine($"noun = {i}; verb = {j};");
                        Console.WriteLine(i*100 + j);
                        return;
                    }
                }
            }
        }

        private static int ProcessData(int[] data)
        {
            for (int i = 0; i < data.Length; i += 4)
            {
                switch (data[i])
                {
                    case 1:
                        data[data[i + 3]] = data[data[i + 1]] + data[data[i + 2]];
                        break;
                    case 2:
                        data[data[i + 3]] = data[data[i + 1]] * data[data[i + 2]];
                        break;
                    case 99:
                        i = int.MaxValue - 5;
                        break;
                    default:
                        break;
                }
            }
            return data[0];
        }

        private static int[] LoadData()
        {
            using StreamReader streamReader = new StreamReader("data.txt");

            string[] raw = streamReader.ReadLine().Split(',');

            return (from r in raw select int.Parse(r)).ToArray();
        }
    }
}
