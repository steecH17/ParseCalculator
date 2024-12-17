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
        [InlineData("(34*23+5)((3+5)(+4)")]
        public void BracketExceptions(string expression)
        {
            var calculator = new StringCalculator(expression);

            Assert.Throws<InvalidOperationException>(() => calculator.Calculate());
        }

        [Theory]
        [InlineData("2 + 3 ? 5")]
        [InlineData("4*3 + 5 * g4")]
        [InlineData("3 % 4")]
        public void UnsupportedOperation(string expression)
        {
            var calculator = new StringCalculator(expression);

            Assert.Throws<InvalidOperationException>(() => calculator.Calculate());
        }

        [Fact]
        public void DivisionByZero()
        {
            var calculator = new StringCalculator("10 / 0");

            Assert.Throws<DivideByZeroException>(() => calculator.Calculate());
        }

        [Fact]
        public void EmptyExpression()
        {
            var calculator = new StringCalculator("");

            Assert.Throws<InvalidOperationException>(() => calculator.Calculate());
        }

        [Fact]
        public void InvalidExpression()
        {
            var calculator = new StringCalculator("5 + + 4");

            Assert.Throws<InvalidOperationException>(() => calculator.Calculate());
        }
    }
}
