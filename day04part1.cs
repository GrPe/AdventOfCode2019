using System;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            //6-digit
            int min = 382345;
            int max = 843167;

            int counter = 0;
            for(int i = min; i <= max; i++)
            {
                counter += Check(i) ? 1 : 0;
            }

            Console.WriteLine(counter);
        }

        public static bool Check(int n)
        {
            string num = n.ToString();

            for(int i = 1; i < num.Length; i++)
            {
                if (num[i] < num[i - 1])
                    return false;
            }

            var numbers = new int[10];
            foreach(var c in num)
                numbers[c - '0']++;

            foreach(var i in numbers)
            {
                if (i >= 2) return true;
            }
            return false;
        }
    }
}
