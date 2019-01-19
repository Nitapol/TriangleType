# TriangleType
C# code with UnitTest to test and brake "public static string TriangleType(string a, string b, string c)"

I started this project to refresh my C# skills with Visual Studio for Mac Community and learn how to unit test with this tool. The task was to create and test a simple function that takes three parameters A, B, C and returns the type of a triangle these parameters create: equilateral, isosceles, or neither.

I realized after the simple implementation of “project1” that A, B, C must be real numbers,  not any strings. Abstract symbols don’t make much sense since they can represent anything. A, A, A, input returns “equilateral” type, but A can be 0 or negative number.

“Project2” implemented with internal conversion input string to double. That created several breaking test cases:
	1. Overflow if input >= 1.7976931348623157E+308
	2. Loss of precision after 15 figures 
	3. Underflow if input 4.94065645841247E-324

If A=4e-324, B=5e-324, C=8e-324. Thus, this tiny triangle side ratio is 4, 5, and 8, but the program recognizes it as “equilateral”. Will you drive a car with this program inside? :-)

To deal with that the third project “Project3” was created and a class BidDecimals was introduced. Sorry, it is a little bit more complicated than project 1 or 2. On a plus side: it had passed all the existed test so far created.

Test results.
Project # Success # Failed #
	1	19	19
	2	30	8
	3	38	0

Please, invent the test that will fail in project3 or find the flaw in this code.

Thank you,
Nitapol 
