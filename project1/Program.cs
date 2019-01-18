using System;

namespace project1
{
    class Program
    {
        static string TriangleType(string a, string b, string c)
        {
            if (a == b && a == c) return "equilateral";
            return a == b || a == c || b == c ? "isosceles" : "neither";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(args.GetLength(0) < 3 ? "neither" :
                              TriangleType(args[0], args[1], args[2]));
        }
    }
}
