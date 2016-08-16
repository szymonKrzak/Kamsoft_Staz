using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOddEvenKata;
using FluentAssertions;

namespace TheOddEvenKata.Tests
{
    [TestFixture]
    public class OddEvenTest
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /*  #region Numbers
          [Test]
          public void NumbersFromRangeTest()
          {
              OddEven oddEven = new OddEven();
              var result = oddEven.NumbersFromRange();
              Assert.IsTrue(result > 0 && result <= 100);
          }

          [Test]
          public void EvenNumberTest()
          {
              OddEven oddEven = new OddEven();
              var result = oddEven.EvenNumber();
              Assert.AreEqual("Even", result);
          }

          [Test]
          public void OddNumberTest()
          {
              OddEven oddEven = new OddEven();
              var result = oddEven.OddNumber();
              Assert.AreEqual("Odd", result);
          }

          [TestCase(1, "Odd")]
          [TestCase(16, "Even")]
          [TestCase(31, "31")]
          public void MainFunctionTest(int num, string expectedResult)
          {
              OddEven oddEven = new OddEven();
              var result = oddEven.MainFunction(num);
              Assert.AreEqual(expectedResult, result);
          }

          #endregion*/

        /*#region Password
        [TestCase("Qwertyui1", true)]
        public void VerifyTest(string password, bool expectedResult)
        {
            PasswordVerifier passwordVer = new PasswordVerifier();
            var result = passwordVer.Verify(password);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void VerifyTestException1()
        {
            PasswordVerifier passwordVer = new PasswordVerifier();
            Assert.Throws<NoUpperLetterException>(() => passwordVer.Verify("qwertyui1"));
        }

        [Test]
        public void VerifyTestException2()
        {
            PasswordVerifier passwordVer = new PasswordVerifier();
            Assert.Throws<NoLowerLetterException>(() => passwordVer.Verify("QWERTYUI1"));
        }

        [Test]
        public void VerifyTestException3()
        {
            PasswordVerifier passwordVer = new PasswordVerifier();
            Assert.Throws<EmptyStringException>(() => passwordVer.Verify(""));
        }

        [Test]
        public void VerifyTestException4()
        {
            PasswordVerifier passwordVer = new PasswordVerifier();
            Assert.Throws<NoDigitException>(() => passwordVer.Verify("Qwertyuiop"));
        }

        [Test]
        public void VerifyTestException5()
        {
            PasswordVerifier passwordVer = new PasswordVerifier();
            Assert.Throws<ToShortStringException>(() => passwordVer.Verify("Qwe"));
        }

        #endregion*/

        [Test]
        public void ChopTest1()
        {
            KarateChop KC = new KarateChop();
            Assert.AreEqual(-1, KC.Chop(3, []));
        }

    }
}
