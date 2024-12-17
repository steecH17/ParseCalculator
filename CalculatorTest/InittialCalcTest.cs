using System;
using System.Linq.Expressions;
using ParseCalculator;

namespace CalculatorTest
{
    public class InittialCalcTest
    {

        [Theory]
        [InlineData("2 + 2", "4")]
        [InlineData("2 - 2", "0")]
        [InlineData("2 / 2", "1")]
        [InlineData("2 * 2", "4")]
        public void CalculateOneOperationWithTwoNumbers(string expression, string result) => Check(expression, result);

        [Theory]
        [InlineData("3+4*4", "19")]
        [InlineData("10-16/4", "6")]
        [InlineData("3 * 10 / 2", "15")]
        [InlineData("20/4 - 4", "1")]
        [InlineData("20/5/4", "1")]
        [InlineData("10*20-400", "-200")]
        public void CalculateTwoOperationWithThreeNumbers(string expression, string result) => Check(expression, result);

        private void Check(string expression, string result)
        {
            var calculator = new StringCalculator(expression);

            Assert.Equal(result, calculator.Calculate());
        }

        
    }
}