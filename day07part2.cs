using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = LoadData();

            var signals = Permutate("56789").ToList();

            long maxOutput = long.MinValue;

            foreach (var signal in signals)
            {
                var computers = new Computer[5];
                for (int i = 0; i < 5; i++)
                    computers[i] = new Computer(data.Select(x => x).ToArray(), true, signal[i] - '0');

                int lastOutput = 0;

                while (!computers[^1].IsHalted)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        computers[i].Run(lastOutput);
                        lastOutput = computers[i].Output.Last();
                    }
                }
                maxOutput = Math.Max(lastOutput, maxOutput);
            }

            Console.WriteLine(maxOutput);

            Console.WriteLine("Finish");
        }

        private static int[] LoadData()
        {
            using StreamReader streamReader = new StreamReader("data.dat");

            string[] raw = streamReader.ReadLine().Split(',');

            return (from r in raw select int.Parse(r)).ToArray();
        }

        private static IEnumerable<string> Permutate(string source)
        {
            if (source.Length == 1) return new List<string> { source };

            var permutations = from c in source from p in Permutate(new string(source.Where(x => x != c).ToArray())) select c + p;

            return permutations;
        }
    }

    public class Computer
    {
        private readonly int[] instructions;
        private int instructionPointer = 0;
        private bool isRunning;
        private Queue<int> input = new Queue<int>();
        private Queue<int> output = new Queue<int>();
        private bool breakMode = false;

        public Computer(int[] data, bool breakMode, params int[] parameters)
        {
            instructions = data;
            instructionPointer = 0;
            isRunning = false;
            this.breakMode = breakMode;

            foreach (var p in parameters)
                input.Enqueue(p);
        }

        public Queue<int> Output { get => output; }

        public bool IsHalted { get; private set; } = false;

        public void Run(params int[] parameters)
        {
            foreach (var p in parameters)
                input.Enqueue(p);

            isRunning = true;
            while (isRunning)
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
                        var value = input.Dequeue();
                        WriteToMemory(param1, instructionPointer + 1, value);
                        instructionPointer += 2;
                        break;
                    }
                case 4: //output
                    {
                        output.Enqueue(ReadFromMemory(param1, instructionPointer + 1));
                        instructionPointer += 2;

                        if (breakMode)
                            isRunning = false;

                        break;
                    }
                case 5: //jump if true
                    {
                        if (ReadFromMemory(param1, instructionPointer + 1) != 0)
                            instructionPointer = ReadFromMemory(param2, instructionPointer + 2);
                        else
                            instructionPointer += 3;
                        break;
                    }
                case 6: //jump if false
                    {
                        if (ReadFromMemory(param1, instructionPointer + 1) == 0)
                            instructionPointer = ReadFromMemory(param2, instructionPointer + 2);
                        else
                            instructionPointer += 3;
                        break;
                    }
                case 7: //less than
                    {
                        var value1 = ReadFromMemory(param1, instructionPointer + 1);
                        var value2 = ReadFromMemory(param2, instructionPointer + 2);
                        WriteToMemory(param3, instructionPointer + 3, (value1 < value2) ? 1 : 0);
                        instructionPointer += 4;
                        break;
                    }
                case 8: //equals
                    {
                        var value1 = ReadFromMemory(param1, instructionPointer + 1);
                        var value2 = ReadFromMemory(param2, instructionPointer + 2);
                        WriteToMemory(param3, instructionPointer + 3, (value1 == value2) ? 1 : 0);
                        instructionPointer += 4;
                        break;
                    }
                case 99: //stop
                    {
                        Console.WriteLine("Halt");
                        isRunning = false;
                        IsHalted = true;
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
