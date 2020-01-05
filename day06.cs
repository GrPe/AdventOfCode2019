using System;
using System.Collections.Generic;
using System.IO;

namespace Day06
{
    class Program
    {
        static Dictionary<string, List<string>> orbits;

        static void Main(string[] args)
        {
            orbits = ReadData();

            //Part 1
            int result = GetAllOrbits("COM", 0);
            Console.WriteLine(result);

            //Part 2
            string[] pathSan = GetPath("COM", "SAN").Split(' ');
            string[] pathYou = GetPath("COM", "YOU").Split(' ');

            int i = 0;
            for(; i < Math.Min(pathSan.Length, pathYou.Length); i++)
            {
                if (pathYou[i] != pathSan[i])
                    break;
            }

            Console.WriteLine(pathSan.Length - i + pathYou.Length - i - 2);
        }

        static Dictionary<string, List<string>> ReadData()
        {
            Dictionary<string, List<string>> orbits = new Dictionary<string, List<string>>();

            string[] lines = File.ReadAllLines("data.txt");
            foreach(var line in lines)
            {
                string[] pair = line.Split(')');
                if(!orbits.ContainsKey(pair[0]))
                {
                    orbits.Add(pair[0], new List<string>());
                }
                orbits[pair[0]].Add(pair[1]);
            }

            return orbits;
        }

        static int GetAllOrbits(string orbit, int level)
        {
            int orbitsCounter = level;

            if (!orbits.ContainsKey(orbit))
                return orbitsCounter;

            foreach(var orb in orbits[orbit])
            {
                orbitsCounter += GetAllOrbits(orb, level + 1);
            }

            return orbitsCounter;
        }

        static string GetPath(string orbit, string toFind)
        {

            if (!orbits.ContainsKey(orbit))
            {
                if (orbit == toFind)
                    return orbit;
                return "";
            }

            foreach (var orb in orbits[orbit])
            {
                string ret = GetPath(orb, toFind);
                if (ret != "")
                    return orbit + " " + ret;
            }

            return "";
        }

    }
}
