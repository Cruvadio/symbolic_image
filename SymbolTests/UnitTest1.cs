using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbol;

namespace SymbolTests
{
    [TestClass]
    public class UnitTest1
    {

        string str = @"x^2 + y^2 + a + b";
        [TestMethod]
        public void FirstExample()
        {
            
            double y = 1.0;
            double x = 1.0;
            FormulaeParser parser = new FormulaeParser();
            double actual = (double)parser.CreateFormula(str)?.Invoke(x, y, 0, 0);
            double expected = 2.0;
            double tolerance = .001;
            Assert.AreEqual(expected, actual, tolerance);
        }


        [TestMethod]
        public void SecondExample()
        {

            double y = 2.0;
            double x = 1.0;
            FormulaeParser parser = new FormulaeParser();
            double actual = (double)parser.CreateFormula(str)?.Invoke(x, y, 0, 0);
            double expected = 5.0;
            double tolerance = .001;
            Assert.AreEqual(expected, actual, tolerance);
        }
    }
}
