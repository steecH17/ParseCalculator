﻿using ParseCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    public class CalculateDifferentExpressionTest
    {
        [Theory]
        [InlineData("(2*3)+4","10")]
        [InlineData("4*(10/2)", "20")]
        [InlineData("(3+10) + (2*8)", "29")]
        [InlineData("(34-29)* (10/2)", "25")]
        public void ExpressionWithBracket(string expression, string result) => Check(expression, result);

        [Theory]
        [InlineData("4+12/4", "7")]
        [InlineData("35-10*2", "15")]
        [InlineData("(35-10)*2", "50")]
        [InlineData("10*3/2", "15")]
        public void PriorityTest(string expression, string result) => Check(expression, result);

        [Theory]
        [InlineData("1/2 + 8/2", "4,5")]
        [InlineData("35-10/4", "32,5")]
        [InlineData("4,35 * 2,2 + 10/4", "12,07")]
        [InlineData("20,5/4,1", "5")]
        public void FractionalNumbersTest(string expression, string result) => Check(expression, result);

        [Theory]
        [InlineData("-4", "-4")]
        [InlineData("-(-1 + 2)", "-1")]
        [InlineData("(10*3 + 5) + (-10 + 5)", "30")]
        [InlineData("-(7+11)", "-18")]
        [InlineData("5-3", "2")]
        public void UnaryMinusTest(string expression, string result) => Check(expression, result);


        private void Check(string expression, string result)
        {
            var calculator = new StringCalculator(expression);

            Assert.Equal(result, calculator.Calculate());
        }
    }
}
