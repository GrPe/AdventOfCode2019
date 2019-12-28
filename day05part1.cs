using System;
using System.IO;
using System.Linq;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = LoadData();

            var computer = new Computer(data);
            computer.Run();

            Console.WriteLine("Finish");
        }

        private static int[] LoadData()
        {
            using StreamReader streamReader = new StreamReader("data.txt");

            string[] raw = streamReader.ReadLine().Split(',');

            return (from r in raw select int.Parse(r)).ToArray();
        }
    }

    public class Computer
    {
        private readonly int[] instructions;
        private int instructionPointer = 0;
        private bool isRunning;

        public Computer(int[] data)
        {
            instructions = data;
            instructionPointer = 0;
            isRunning = false;
        }

        public void Run()
        {
            isRunning = true;
            while(isRunning)
            {
                ExecuteInstruction();
            }
        }

        private void ExecuteInstruction()
        {
            var instruction = instructions[instructionPointer].ToString().PadLeft(5, '0');
            var opCode = GetNumber(instruction[^2]) * 10 + GetNumber(instruction[^1]);
            var param1 = GetNumber(instruction[^3]);
            var param2 = GetNumber(instruction[^4]);
            var param3 = GetNumber(instruction[^5]);

            switch (opCode)
            {
                case 1: //sum
                    {
                        var value1 = ReadFromMemory(param1, instructionPointer + 1);
                        var value2 = ReadFromMemory(param2, instructionPointer + 2);
                        WriteToMemory(param3, instructionPointer + 3, value1 + value2);
                        instructionPointer += 4;
                        break;
                    }
                case 2: //multiply
                    {
                        var value1 = ReadFromMemory(param1, instructionPointer + 1);
                        var value2 = ReadFromMemory(param2, instructionPointer + 2);
                        WriteToMemory(param3, instructionPointer + 3, value1 * value2);
                        instructionPointer += 4;
                        break;
                    }
                case 3: //input
                    {
                        Console.Write("Input: ");
                        var value = int.Parse(Console.ReadLine());
                        WriteToMemory(param1, instructionPointer + 1, value);
                        instructionPointer += 2;
                        break;
                    }
                case 4: //output
                    {
                        Console.WriteLine("output: " + ReadFromMemory(param1, instructionPointer + 1).ToString());
                        instructionPointer += 2;
                        break;
                    }
                case 99: //stop
                    {
                        Console.WriteLine("Halt");
                        isRunning = false;
                        break;
                    }
            }
        }

        private int GetNumber(char c)
        {
            return (int)(c - '0');
        }

        private int ReadFromMemory(int mode, int pointer)
        {
            switch (mode)
            {
                case 0:
                    return instructions[instructions[pointer]];
                case 1:
                    return instructions[pointer];
                default:
                    return instructions[instructions[pointer]];
            }

        }

        private void WriteToMemory(int mode, int pointer, int value)
        {
            instructions[instructions[pointer]] = value;
        }
    }
}
