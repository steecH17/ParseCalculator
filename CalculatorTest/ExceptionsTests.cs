using ParseCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    public class ExceptionsTests
    {
        [Theory]
        [InlineData("(2*3))+4")]
        [InlineData("4*((10/2)")]
        [InlineData("((2+2)*2))")]
        [InlineData(")(2*2)(")]
        [InlineData("()(2*2)")]
        [InlineData("(2*2)()")]
        [InlineData("(34*23+5)((3+5)(+4)")]
        public void BracketExceptions(string expression) => TrowInvalidOperationException(expression);
        

        [Theory]
        [InlineData("2 + 3 ? 5")]
        [InlineData("4*3 + 5 * g4")]
        [InlineData("3 % 4")]
        public void UnsupportedOperation(string expression) => TrowInvalidOperationException(expression);
        

        [Fact]
        public void DivisionByZero()
        {
            var calculator = new StringCalculator("10 / 0");

            Assert.Throws<DivideByZeroException>(() => calculator.Calculate());
        }

        [Fact]
        public void EmptyExpression() => TrowInvalidOperationException("");


        [Theory]
        [InlineData("+ 2 + 2")]
        [InlineData("2 + 2 +")]
        [InlineData("2 * + 2")]
        public void InvalidExpression(string expression) => TrowInvalidOperationException(expression);
        

        private void TrowInvalidOperationException(string exception) => Assert.Throws<InvalidOperationException>(() => new StringCalculator(exception).Calculate());
    }
}
