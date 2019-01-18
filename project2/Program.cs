using System;

namespace project2
{
    public class Program
    {
        private static double ConvertToDouble(string s)
        {
            double a;
            try
            {
                a = Convert.ToDouble(s);
            }
            catch (OverflowException)
            {
                a = Double.PositiveInfinity;
            }
            /* It is a question: if we really want to catch that here
            catch (FormatException)
            {
                a = Double.NaN;
            }
            */
            return a;
        }

        public static string TriangleType(string sa, string sb, string sc)
        {
            try
            {
                double a = ConvertToDouble(sa);
                double b = ConvertToDouble(sb);
                double c = ConvertToDouble(sc);
                if (a <= 0 || b <= 0 || c <= 0)
                    return "neither"; // Test induced "if"!
                if (a == b && a == c) return "equilateral";
                if (a + b <= c || b + c <= a || a + c <= b) return "neither";
                return a == b || a == c || b == c ? "isosceles" : "neither";
            }
            catch (FormatException)
            {
                return "neither";
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
                return "neither";
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(args.GetLength(0) < 3 ? "neither" :
                              TriangleType(args[0], args[1], args[2]));
        }
    }
}