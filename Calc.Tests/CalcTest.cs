using System;
using Xunit;
using CalculatorProgram;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace Calc.Tests
{
    public class CalcTest
    {
        [Fact]
        public void IsRightDoOperation()
        {
            Calculator calc = new Calculator();
            Assert.Equal(7,calc.DoOperation(2, 5, "a"));
            Assert.Equal(-3, calc.DoOperation(2, 5, "s"));
            Assert.Equal(10, calc.DoOperation(2, 5, "m"));
            Assert.Equal(0.4, calc.DoOperation(2, 5, "d"));
            Assert.Equal(double.NaN, calc.DoOperation(2, 5, "e"));
            calc.Finish();
        }
        [Fact]
        public void IsRightMain()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(@"f
300
f
50
d
n");
            Console.SetIn(input);

            Program.Main(new string[] { });

            var expectedOutput = "Console Calculator in C#" +
                                 "------------------------" +
                                 "Type a number, and then press Enter: " +
                                 "This is not valid input. Please enter an integer value: "+
                                 "Type another number, and then press Enter: " +
                                 "This is not valid input. Please enter an integer value: " +
                                 "Choose an operator from the following list:" +
                                 "a - Add" +
                                 "s - Subtract" +
                                 "m - Multiply" +
                                 "d - Divide" +
                                 "Your option? " +
                                 "Your result: 6" +
                                 "------------------------" +
                                 "Press 'n' and Enter to close the app, or press any other key and Enter to continue: ";

            Assert.Equal(expectedOutput, Regex.Replace(output.ToString(), @"[\r\t\n]+", string.Empty));
        }
    }
}
