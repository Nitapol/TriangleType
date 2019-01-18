using System;
using Xunit;
using project2;

namespace Test
{
    public class UnitTest1
    {
        static string equilateral_S = null, isosceles_S = null, neither_S = null;

        string equilateral()
        {
            if (equilateral_S == null)
                equilateral_S = Program.TriangleType("1", "1", "1");
            return equilateral_S;
        }

        string isosceles()
        {
            if (isosceles_S == null)
                isosceles_S = Program.TriangleType("2", "2", "1");
            return isosceles_S;
        }

        string neither()
        {
            if (neither_S == null)
                neither_S = Program.TriangleType("2", "3", "4");
            return neither_S;
        }

        [Fact]
        // Basic tests
        public void Test_01_Basic() 
        {
            for (int i = 2; i < 10; i++)
            {
                string a = i.ToString();
                Assert.Equal(equilateral(), Program.TriangleType(a, a, a));
                string b = (i-1).ToString();
                Assert.Equal(isosceles(), Program.TriangleType(a, a, b));
                Assert.Equal(isosceles(), Program.TriangleType(a, b, a));
                Assert.Equal(isosceles(), Program.TriangleType(b, a, a));
                string c = (i + 1).ToString();
                Assert.Equal(neither(), Program.TriangleType(a, b, c));
                Assert.Equal(neither(), Program.TriangleType(a, c, b));
                Assert.Equal(neither(), Program.TriangleType(b, a, c));
                Assert.Equal(neither(), Program.TriangleType(b, c, a));
                Assert.Equal(neither(), Program.TriangleType(c, a, b));
                Assert.Equal(neither(), Program.TriangleType(c, b, a));
                // Line, not the triangle: A + B == C 
                // Actually it is, according to Wiki:
                // "degenerate triangle, one with collinear vertices"
                c = (2 * i - 1).ToString();
                Assert.Equal(neither(), Program.TriangleType(a, b, c));
                Assert.Equal(neither(), Program.TriangleType(a, c, b));
                Assert.Equal(neither(), Program.TriangleType(b, a, c));
                Assert.Equal(neither(), Program.TriangleType(b, c, a));
                Assert.Equal(neither(), Program.TriangleType(c, a, b));
                Assert.Equal(neither(), Program.TriangleType(c, b, a));
                c = (2 * i).ToString();
                Assert.Equal(neither(), Program.TriangleType(a, a, c));
                Assert.Equal(neither(), Program.TriangleType(a, c, a));
                Assert.Equal(neither(), Program.TriangleType(c, a, a));
                // Now: not a triangle: A + B < C
                c = (2 * i).ToString();
                Assert.Equal(neither(), Program.TriangleType(a, b, c));
                Assert.Equal(neither(), Program.TriangleType(a, c, b));
                Assert.Equal(neither(), Program.TriangleType(b, a, c));
                Assert.Equal(neither(), Program.TriangleType(b, c, a));
                Assert.Equal(neither(), Program.TriangleType(c, a, b));
                Assert.Equal(neither(), Program.TriangleType(c, b, a));

            }

            // Different float point variations of same numbers. 
            Assert.Equal(equilateral(), Program.TriangleType("1", "1.0", "+1.00e-0"));
            Assert.Equal(isosceles(), Program.TriangleType("+1", ".1E1", "1.9"));
            // Line, not the triangle.
            Assert.Equal(neither(), Program.TriangleType("1", "1", "2"));

            // These three types should not match. 
            Assert.DoesNotMatch(equilateral(), isosceles());
            Assert.DoesNotMatch(equilateral(), neither());
            Assert.DoesNotMatch(isosceles(), neither());

            // More basic types
            Assert.Equal(equilateral(), Program.TriangleType("2", "2", "2"));
            Assert.Equal(isosceles(), Program.TriangleType("2", "3", "2"));
            Assert.Equal(neither(), Program.TriangleType("2", "3", "4"));
        }

        [Fact]
        // Test the number of significant digits 
        public void Test_02_Significant()
        {
            Console.WriteLine("Double.Epsilon  {0:r} - the smallest positive value that is greater than zero.", Double.Epsilon);
            Console.WriteLine("Double.MaxValue {0:r} - the largest possible value of Double.", Double.MaxValue);
            Console.WriteLine("Double.MinValue {0:r} - the largest smallest value of Double.", Double.MinValue);
            Console.WriteLine("Double.NaN      {0:r} - represents a value that is not a number.", Double.NaN);
            Console.WriteLine("Double.NaN      {0:r} - represents a value that is not a number.", Double.NaN);
            Console.WriteLine("Double.PositiveInfinity {0:r} - represents positive infinity.", Double.PositiveInfinity);
            Console.WriteLine("Double.NegativeInfinity {0:r} - represents negative infinity.", Double.NegativeInfinity);

            // Console.WriteLine("PositiveInfinity plus 10.0 equals {0}.", (Double.PositiveInfinity + 10.0).ToString());
            string s = "9.";
            for (int i = 0; i < 100; i++)
            {
                s = s + "0";
                string a = s + "1", b = s + "2", c = s + "3";
                Console.WriteLine("Significant i = {0} a = {1} b = {2} c = {3}",
                    i.ToString(), a, b, c);

                Assert.Equal(equilateral(), Program.TriangleType(a, a, a));
                Assert.Equal(equilateral(), Program.TriangleType(b, b, b));
                Assert.Equal(equilateral(), Program.TriangleType(c, c, c));

                Assert.Equal(isosceles(), Program.TriangleType(a, a, b));
                Assert.Equal(isosceles(), Program.TriangleType(a, b, a));
                Assert.Equal(isosceles(), Program.TriangleType(b, a, a));

                Assert.Equal(isosceles(), Program.TriangleType(a, a, c));
                Assert.Equal(isosceles(), Program.TriangleType(c, a, a));
                Assert.Equal(isosceles(), Program.TriangleType(c, a, a));

                Assert.Equal(isosceles(), Program.TriangleType(b, b, a));
                Assert.Equal(isosceles(), Program.TriangleType(b, a, b));
                Assert.Equal(isosceles(), Program.TriangleType(a, b, b));

                Assert.Equal(isosceles(), Program.TriangleType(b, b, c));
                Assert.Equal(isosceles(), Program.TriangleType(b, c, b));
                Assert.Equal(isosceles(), Program.TriangleType(c, b, b));

                Assert.Equal(isosceles(), Program.TriangleType(c, c, a));
                Assert.Equal(isosceles(), Program.TriangleType(c, a, c));
                Assert.Equal(isosceles(), Program.TriangleType(a, c, c));

                Assert.Equal(isosceles(), Program.TriangleType(c, c, b));
                Assert.Equal(isosceles(), Program.TriangleType(c, b, c));
                Assert.Equal(isosceles(), Program.TriangleType(b, c, c));

                Assert.Equal(neither(), Program.TriangleType(a, b, c));
                Assert.Equal(neither(), Program.TriangleType(a, c, b));
                Assert.Equal(neither(), Program.TriangleType(b, a, c));
                Assert.Equal(neither(), Program.TriangleType(b, c, a));
                Assert.Equal(neither(), Program.TriangleType(c, a, b));
                Assert.Equal(neither(), Program.TriangleType(c, b, a));
            }
        }

        [Theory]
        // Must be "neither", but results were different in the "proect1".
        // Still debatable what to expect
        [InlineData("a", "a", "a")]
        [InlineData("a", "b", "c")]
        [InlineData("a", "a", "c")]
        [InlineData("a", "b", "a")]
        [InlineData("a", "b", "b")]
        // Sanity tests
        [InlineData("0", "0", "0")] // Got me here  :-) 
        [InlineData("1", "1", "0")]
        [InlineData("1", "0", "1")]
        [InlineData("0", "1", "1")]
        // More sanity tests
        [InlineData("1", "1", "-0")]
        [InlineData("1", "-0", "1")]
        [InlineData("-0", "1", "1")]
        [InlineData("1", "1", "-1")]
        [InlineData("1", "-1", "1")]
        [InlineData("-1", "1", "1")]
        [InlineData("1", "1", "-+1")]
        [InlineData("1", "-+1", "1")]
        [InlineData("-+1", "1", "1")]
        // Triangles, but Scalene or "neither" in our case.
        [InlineData("2.1", "2.2", "2.3")]
        [InlineData("2.01", "2.02", "2.03")]
        [InlineData("2.001", "2.002", "2.003")]
        // ... can be made 
        [InlineData("2.0000000000001", "2.0000000000002", "2.0000000000003")]
        [InlineData("2.00000000000001", "2.00000000000002", "2.00000000000003")]
        [InlineData("2.000000000000001", "2.000000000000002", "2.000000000000003")]
        [InlineData("9.000000000000001", "9.000000000000002", "9.000000000000003")]
        // doubles have precision of 15 digits. Test this for equilateral  
        [InlineData("2.0000000000000001", "2.0000000000000002", "2.0000000000000003")]
        [InlineData("2.00000000000000001", "2.00000000000000002", "2.00000000000000003")]
        // The smallest positive Double in C# is 4.94065645841247E-324
        [InlineData("4.94065645841247E-324", "4.94065645841248E-324", "4.94065645841249E-324")]
        [InlineData("4E-324", "5E-324", "8E-324")]
        // The largest possible value of a Double 1.79769313486232E+308 (in docs.microsoft.com)
        [InlineData("1.79769313486232E+308", "1.79769313486231E+308", "1.79769313486230E+308")]
        // Actually it is                         1.7976931348623157E+308
        [InlineData("1.7976931348623157E+308", "1.7976931348623156E+308", "1.7976931348623155E+308")]
        [InlineData("NaN", "NaN", "NaN")]
        public void Test_02_neither(string a, string b, string c)
        {
            Assert.Equal(neither(), Program.TriangleType(a, b, c));
        }


        [Theory]
        [InlineData("4.94065645841247E-324")]
        [InlineData("1.79769313486232E+308")]
        [InlineData("1.7976931348623157E+308")]
        public void Test_02_equilateral(string a)
        {
            Assert.Equal(equilateral(), Program.TriangleType(a, a, a));
        }

        [Theory]
        [InlineData("1.79769313486232E+308", "4.94065645841247E-324")]
        public void Test_02_isosceles(string a, string b)
        {
            Assert.Equal(isosceles(), Program.TriangleType(a, a, b));
            Assert.Equal(isosceles(), Program.TriangleType(a, b, a));
            Assert.Equal(isosceles(), Program.TriangleType(b, a, a));
        }

    }
}
