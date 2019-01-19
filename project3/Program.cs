using System;
using System.Text.RegularExpressions;
using System.Numerics;

namespace project3
{
    public class Program
    {
        public class BigDecimal : IComparable, IComparable<BigDecimal>
        /* 1.2345 = 12345 * 10 ** -4
           1.2345e2 = 12345 * 10 ** -2
           12345 is significant
           -4, -2 are exponents         
        */
        {
            protected BigInteger significant;
            protected BigInteger exponent;

            public BigDecimal()
            {
                significant = 0;
                exponent = 0;
            }

            public BigDecimal(int i)
            {
                significant = i;
                exponent = 0;
            }

            public BigDecimal(BigDecimal i)
            {
                significant = i.significant;
                exponent = i.exponent;
            }

            public BigDecimal(string s)
            {
                significant = 0;
                exponent = 0;
                Regex rgx = new Regex("^[\\+-]?(\\d*\\.?\\d+){1}([eE][\\+-]?\\d+)?$");
                if (!rgx.IsMatch(s))
                {
                    rgx = new Regex("^[\\+-]?(\\d+\\.?){1}([eE][\\+-]?\\d+)?$"); // case of 1.
                    if (!rgx.IsMatch(s))
                    {
                        rgx = new Regex("^[\\+-]?(\\.?\\d+){1}([eE][\\+-]?\\d+)?$"); // case of .1
                        if (!rgx.IsMatch(s))
                        {
                            throw new FormatException(String.Format("Error in BigDecimal '{0}'", s));
                        }
                    }
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
            }

            private void RemoveTrailingZeros()
            {
                while (significant != 0 && significant % 10 == 0)
                {
                    significant = this.significant / 10;
                    exponent++;
                }
            }

            private BigDecimal MatchExponent(BigDecimal other)
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

            public int CompareTo(BigDecimal other)
            {
                other = MatchExponent(other);
                return this.significant.CompareTo(other.significant);
            }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                if (obj is int i)
                {
                    BigDecimal other = new BigDecimal(i);
                    return this.significant.CompareTo(other.significant);
                }
                else if (obj is BigDecimal other)
                {
                    other = MatchExponent(other);
                    return this.significant.CompareTo(other.significant);
                }
                else
                    throw new ArgumentException("Object is not an BigDecimal");
            }

            public override bool Equals(object obj)
            {
                return this.CompareTo(obj) == 0;
            }

            public override int GetHashCode()
            {
                this.RemoveTrailingZeros();
                return this.significant.GetHashCode();
            }

            public static bool operator !=(BigDecimal operand1, BigDecimal operand2)
            {
                return operand1.CompareTo(operand2) != 0;
            }
            public static bool operator ==(BigDecimal operand1, BigDecimal operand2)
            {
                return operand1.CompareTo(operand2) == 0;
            }

            public static bool operator <=(BigDecimal operand1, BigDecimal operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }
            public static bool operator >=(BigDecimal operand1, BigDecimal operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            public static bool operator <(BigDecimal operand1, BigDecimal operand2)
            {
                return operand1.CompareTo(operand2) < 0;
            }
            public static bool operator >(BigDecimal operand1, BigDecimal operand2)
            {
                return operand1.CompareTo(operand2) > 0;
            }

            public static bool operator <=(BigDecimal operand1, int operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }
            public static bool operator >=(BigDecimal operand1, int operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            public static BigDecimal operator +(BigDecimal operand1, BigDecimal operand2)
            {
                BigDecimal a = new BigDecimal(operand1);
                BigDecimal b = new BigDecimal(operand2);

                b = a.MatchExponent(b);
                a.significant += b.significant;
                a.RemoveTrailingZeros();
                return a;
            }
        }

        public static string TriangleTypeExtended(string sa, string sb, string sc)
        {
            BigDecimal a, b, c;
            try
            {
                a = new BigDecimal(sa);
                b = new BigDecimal(sb);
                c = new BigDecimal(sc);
            }
            catch (FormatException e)
            {
                return e.ToString();
            }
            if (a <= 0 || b <= 0 || c <= 0)
                return "negative side";
            if (a == b && a == c) return "equilateral";
            if (a + b == c || b + c == a || a + c == b) return "degenerate";
            if (a + b < c || b + c < a || a + c < b) return "two sides < third side";
            return a == b || a == c || b == c ? "isosceles" : "scalene";
        }

        public static string TriangleType(string sa, string sb, string sc)
        {
            string s = TriangleTypeExtended(sa, sb, sc);
            if (s != "equilateral" && s != "isosceles")
                s = "neither";
            return s;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(args.GetLength(0) < 3 ? "number of arguments < 3" :
                              TriangleTypeExtended (args[0], args[1], args[2]));
        }
    }
}
