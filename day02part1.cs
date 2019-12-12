using System;
using System.IO;
using System.Linq;

namespace Day02
{
    class Program
    {

        static void Main(string[] args)
        {
            var data = LoadData();

            data[1] = 12;
            data[2] = 2;

            foreach (var d in data)
                Console.Write(d.ToString() + " ");
            Console.WriteLine();

            for (int i = 0; i < data.Length; i+=4)
            {
                switch(data[i])
                {
                    case 1:
                        data[data[i + 3]] = data[data[i + 1]] + data[data[i + 2]];
                        break;
                    case 2:
                        data[data[i + 3]] = data[data[i + 1]] * data[data[i + 2]];
                        break;
                    case 99:
                        i = int.MaxValue -5;
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
            }
            Console.WriteLine(data[0]);
        }

        private static int[] LoadData()
        {
            using StreamReader streamReader = new StreamReader("data.txt");

            string[] raw = streamReader.ReadLine().Split(',');

            return (from r in raw select int.Parse(r)).ToArray(); 
        }
    }
}
