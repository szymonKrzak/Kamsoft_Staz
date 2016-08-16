using NUnit.Framework;
using StringCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTest
    {
        /**[TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("1,2", 3)]
        [TestCase("1\n2,3", 6)]*/
        public void ConverterPositive(string numbers, int expectedResult)
        {
            StringCalc calculator = new StringCalc();
            var result = calculator.Add(numbers);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("-1,-3", 1)]
        public void ConvertNegative(string numbers, int expectedResult)
        {
            StringCalc calculator = new StringCalc();
            Assert.Throws<NegativesNotAllowedException>(() => calculator.Add(numbers));
        }
    }
}
