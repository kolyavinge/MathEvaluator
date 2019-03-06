using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluator
{
    [TestClass]
    public class MathEvaluatorTest
    {
        private MathEvaluator _mathEvaluator;

        [TestInitialize]
        public void TestInit()
        {
            _mathEvaluator = new MathEvaluator();
        }

        [TestMethod]
        public void Eval_1()
        {
            var result = _mathEvaluator.Eval("1+2");
            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void Eval_2()
        {
            var result = _mathEvaluator.Eval("1+2+3");
            Assert.AreEqual(6.0, result);
        }

        [TestMethod]
        public void Eval_3()
        {
            var result = _mathEvaluator.Eval("1+2+3-4");
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void Eval_4()
        {
            var result = _mathEvaluator.Eval("2*3");
            Assert.AreEqual(6.0, result);
        }

        [TestMethod]
        public void Eval_5()
        {
            var result = _mathEvaluator.Eval("2*3/2");
            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void Eval_6()
        {
            var result = _mathEvaluator.Eval("1+2*3/6");
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void Eval_61()
        {
            var result = _mathEvaluator.Eval("2*3/6 + 1");
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void Eval_7()
        {
            var result = _mathEvaluator.Eval("(1+2)*(2+3)");
            Assert.AreEqual(15.0, result);
        }

        [TestMethod]
        public void Eval_8()
        {
            var result = _mathEvaluator.Eval("(1+2)*(2+3)*(3+4)");
            Assert.AreEqual(105.0, result);
        }

        [TestMethod]
        public void Eval_9()
        {
            var result = _mathEvaluator.Eval("(1+2)*(2+3)+(3+4)");
            Assert.AreEqual(22.0, result);
        }
    }

    [TestClass]
    public class ScannerTest
    {
        [TestMethod]
        public void GetToken_All()
        {
            var scanner = new Scanner("( ) + - * / 1");
            var tokens = scanner.GetAllTokens().ToList();
            Assert.AreEqual(7, tokens.Count);
            Assert.AreEqual(TokenKind.OpenBracket, tokens[0].Kind);
            Assert.AreEqual(TokenKind.CloseBracket, tokens[1].Kind);
            Assert.AreEqual(TokenKind.Add, tokens[2].Kind);
            Assert.AreEqual(TokenKind.Sub, tokens[3].Kind);
            Assert.AreEqual(TokenKind.Mul, tokens[4].Kind);
            Assert.AreEqual(TokenKind.Div, tokens[5].Kind);
            Assert.AreEqual(TokenKind.Number, tokens[6].Kind);
            Assert.AreEqual(1, tokens[6].Value);
        }

        [TestMethod]
        public void GetToken_100()
        {
            var scanner = new Scanner("100");
            var tokens = scanner.GetAllTokens().ToList();
            Assert.AreEqual(100.0, tokens[0].Value);
        }

        [TestMethod]
        public void GetToken_123()
        {
            var scanner = new Scanner("123");
            var tokens = scanner.GetAllTokens().ToList();
            Assert.AreEqual(123.0, tokens[0].Value);
        }

        [TestMethod]
        public void GetToken_1230()
        {
            var scanner = new Scanner("123.0");
            var tokens = scanner.GetAllTokens().ToList();
            Assert.AreEqual(123.0, tokens[0].Value);
        }

        [TestMethod]
        public void GetToken_12301()
        {
            var scanner = new Scanner("123.01");
            var tokens = scanner.GetAllTokens().ToList();
            Assert.AreEqual(123.01, tokens[0].Value);
        }

        [TestMethod]
        public void GetToken_123456()
        {
            var scanner = new Scanner("123.456");
            var tokens = scanner.GetAllTokens().ToList();
            Assert.AreEqual(123.456, tokens[0].Value);
        }
    }
}
