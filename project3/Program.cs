using System;
using System.Text.RegularExpressions;
using System.Numerics;

namespace project3
{
    public class Program
    {
        public class UnlimitedRealNumber : IComparable, IComparable<UnlimitedRealNumber>
        /* 1.2345 = 12345 * 10 ** -4
           1.2345e2 = 12345 * 10 ** -2
           12345 is significant
           -4, -2 are exponents         
        */
        {
            protected BigInteger significant;
            protected BigInteger exponent;

            public UnlimitedRealNumber()
            {
                significant = 0;
                exponent = 0;
            }

            public UnlimitedRealNumber(int i)
            {
                significant = i;
                exponent = 0;
            }

            public UnlimitedRealNumber(UnlimitedRealNumber i)
            {
                significant = i.significant;
                exponent = i.exponent;
            }

            public UnlimitedRealNumber(string s)
            {
                significant = 0;
                exponent = 0;
                Regex rgx = new Regex("^[\\+-]?(\\d*\\.?\\d+){1}([eE][\\+-]?\\d+)?$");
                if (!rgx.IsMatch(s))
                {
                    rgx = new Regex("^[\\+-]?(\\d+\\.?){1}([eE][\\+-]?\\d+)?$"); // case of 1.
                    if (!rgx.IsMatch(s))
                        rgx = new Regex("^[\\+-]?(\\.?\\d+){1}([eE][\\+-]?\\d+)?$"); // case of .1
                    else
                        throw new FormatException();
                }
                Match m = Regex.Match(s, "^[\\+-]?\\d+");
                if (m.Success)
                {
                    significant = BigInteger.Parse(m.Value);
                }
                m = Regex.Match(s, "\\.\\d+");
                if (m.Success)
                {
                    string t = m.Value.Substring(1);
                    foreach (char c in t)
                    {
                        significant = significant * 10 + (c - '0');
                        exponent--;
                    }
                }
                m = Regex.Match(s, "[eE][\\+-]?\\d+");
                if (m.Success)
                {
                    exponent += BigInteger.Parse(m.Value.Substring(1));
                }
                RemoveTrailingZeros();
                Console.WriteLine(s);
                Console.WriteLine(significant);
                Console.WriteLine(exponent);
            }

            private void RemoveTrailingZeros()
            {
                while (significant != 0 && significant % 10 == 0)
                {
                    significant = this.significant / 10;
                    exponent++;
                }
            }

            private UnlimitedRealNumber MatchExponent(UnlimitedRealNumber other)
            {
                this.RemoveTrailingZeros();
                other.RemoveTrailingZeros();
                while (this.exponent != other.exponent)
                {
                    if (this.exponent < other.exponent)
                    {
                        other.significant = other.significant * 10;
                        other.exponent--;
                    }
                    else
                    {
                        this.significant = this.significant * 10;
                        this.exponent--;
                    }
                }
                return other;
            }


            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                if (obj is int i)
                {
                    UnlimitedRealNumber other = new UnlimitedRealNumber(i);
                    return this.significant.CompareTo(other.significant);
                }
                else if (obj is UnlimitedRealNumber other)
                {
                    other = MatchExponent(other);
                    return this.significant.CompareTo(other.significant);
                }
                else
                    throw new ArgumentException("Object is not an UnlimitedRealNumber");
            }

            public int CompareTo(UnlimitedRealNumber other)
            {
                other = MatchExponent(other);
                return this.significant.CompareTo(other.significant);
            }

            public static bool operator !=(UnlimitedRealNumber operand1, UnlimitedRealNumber operand2)
            {
                return operand1.CompareTo(operand2) != 0;
            }

            public static bool operator ==(UnlimitedRealNumber operand1, UnlimitedRealNumber operand2)
            {
                return operand1.CompareTo(operand2) == 0;
            }

            public static bool operator <=(UnlimitedRealNumber operand1, UnlimitedRealNumber operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }

            public static bool operator >=(UnlimitedRealNumber operand1, UnlimitedRealNumber operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            public static bool operator <=(UnlimitedRealNumber operand1, int operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }

            public static bool operator >=(UnlimitedRealNumber operand1, int operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            public static UnlimitedRealNumber operator +(UnlimitedRealNumber operand1, UnlimitedRealNumber operand2)
            {
                UnlimitedRealNumber a = new UnlimitedRealNumber(operand1);
                UnlimitedRealNumber b = new UnlimitedRealNumber(operand2);

                b = a.MatchExponent(b);
                a.significant += b.significant;
                a.RemoveTrailingZeros();
                Console.WriteLine(a.significant);
                Console.WriteLine(a.exponent);
                return a;
            }
        }

        public static string TriangleType(string sa, string sb, string sc)
        {
            try
            {
                UnlimitedRealNumber a = new UnlimitedRealNumber(sa);
                UnlimitedRealNumber b = new UnlimitedRealNumber(sb);
                UnlimitedRealNumber c = new UnlimitedRealNumber(sc);
                if (a <= 0 || b <= 0 || c <= 0)
                    return "neither"; // Test induced "if"!
                if (a == b && a == c) return "equilateral";
                if (a + b <= c || b + c <= a || a + c <= b) return "neither";
                return a == b || a == c || b == c ? "isosceles" : "neither";
            }
            catch (FormatException e)
            {
                Console.WriteLine("Format Exception Handler: {0}", e.ToString());
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
