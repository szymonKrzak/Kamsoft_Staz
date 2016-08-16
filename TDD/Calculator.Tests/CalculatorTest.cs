using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorTest
    {
        #region Add
        [Test]
        public void Add_TwoPositiveNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            int result = calculator.Add(2, 2);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Add_FirstPositiveSecondNegativeNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            int result = calculator.Add(2, -3);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Add_FirstNegativeSecondPositiveNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            int result = calculator.Add(-2, 3);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Add_TwoNegativeNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            int result = calculator.Add(-2, -2);
            Assert.AreEqual(-4, result);
        }
        #endregion

        [Test]
        public void Div_TwoIntNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            double result = calculator.Div(2, 2);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Div_DivideByZeroNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            Assert.Throws<DivideByZeroException>(() => calculator.Div(5,0));

        }

        [Test]
        public void Div_ZeroDividendNumbers_Calculated()
        {
            Calculator calculator = new Calculator();
            double result = calculator.Div(0, 5);
            //TestContext.WriteLine(result);
            Assert.AreEqual(0, result);
        }
    }
}
