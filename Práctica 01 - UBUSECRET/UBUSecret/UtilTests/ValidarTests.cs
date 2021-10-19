using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Tests
{
    [TestClass()]
    public class ValidarTests
    {
        [TestMethod()]
        public void PasswordTest()
        {
            Assert.IsFalse(Validar.Password(""));
            Assert.IsFalse(Validar.Password(null));
            Assert.IsFalse(Validar.Password("holaaa"));
            Assert.IsFalse(Validar.Password("Holaaa"));
            Assert.IsFalse(Validar.Password("Holaaa"));
            Assert.IsFalse(Validar.Password("hhhhhh"));
            Assert.IsFalse(Validar.Password("HHHHHH"));
            Assert.IsFalse(Validar.Password("123456"));
            Assert.IsFalse(Validar.Password("Hola1"));
            Assert.IsTrue(Validar.Password("Hola12"));
            Assert.IsTrue(Validar.Password("Hola123"));
        }
    }
}