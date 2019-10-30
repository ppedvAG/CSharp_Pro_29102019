using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_3_and_4_results_7()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_results_0()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calc_Sum_MAX_and_1_throws()
        {

            Calc calc = new Calc();
            //

            //c̵̨̡̳̳͚̣̼̠̦̺͍̮̺̻̗̥̗̤͖̹̋ͧ̓̄̒̓̆ͪ̍ͬ̾̓ͣ̄͋ͤ̐̀#̳̼̦͇͓͋̀͋̃ͭ͂̅̕ ͥͧ̑͋̇̀̄̿̐͌ͬ̽̈́̆ͪ͒̄͐̾͘͏̶̧̹̝͕͙͓̙̣͙̳̰̻̺̼̭̦̺ͅp̧̱͖̖̤̱̜̳͈̖̼̞͍͋̂̂ͧ̈̈ͣ̊̈ͤ͊̄ͣͫ̈ͬ͜͜r̡̬͇̞͕̗̠̞ͯͦͫ͒̽̈̇ͪ̾́͑̊̇̾̎̐ͫ̉̿͝o̷̧̮͉͈͇̱̖͙ͨ͂̇̒ͮ͊̀


            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));

        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(3, 4, 7)]
        [DataRow(100, 20, 120)]
        [DataRow(-109, 6, -103)]
        [DataRow(20, -30, -10)]
        public void Calc_Sum(int a, int b, int soll)
        {
            Calc calc = new Calc();

            Assert.AreEqual(soll, calc.Sum(a, b));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            Calc calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 10, 28);
                Assert.IsFalse(calc.IsWeekend());

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 10, 29);
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 10, 30);
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 10, 31);
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 11, 1);
                Assert.IsFalse(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 11, 2);
                Assert.IsTrue(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 11, 3);
                Assert.IsTrue(calc.IsWeekend());
            }
        }
    }
}
