using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class GestorTests
    {
        private StringBuilder verificationErrors;
        private static string baseURL;
        private static List<IWebDriver> drivers;
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            drivers = new List<IWebDriver>();
            drivers.Add(new ChromeDriver());
            drivers.Add(new EdgeDriver());
            baseURL = "https://localhost:44319";
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            foreach (var driver in drivers)
            {
                try
                {
                    //driver.Quit();// quit does not close the window
                    driver.Close();
                    driver.Dispose();
                }
                catch (Exception)
                {
                    // Ignore errors if unable to close the browser
                }
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        /**
         * Habilita y deshabilita al usuario Alberto
         */
        [TestMethod]
        public void TheGestorHabilitarAlbertoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("alberto@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Alberto1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("Un administrador debe darte acceso manualmente", driver.FindElement(By.Id("ErrMsg")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("Hola Gestor Ubusecret! (Cerrar Sesión)", driver.FindElement(By.Id("LblUsuario")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.XPath("//a[@id='Admin']/span")).Click();
                try
                {
                    Assert.AreEqual("Alberto Hernández", driver.FindElement(By.XPath("//div[@id='ContentPlaceHolder1_3']/span")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                try
                {
                    Assert.AreEqual("alberto@ubusecret.es", driver.FindElement(By.XPath("//div[@id='ContentPlaceHolder1_3']/span[2]")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                try
                {
                    Assert.AreEqual("Deshabilitado", driver.FindElement(By.Id("ContentPlaceHolder1_alberto@ubusecret.es")).GetAttribute("value"));
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_alberto@ubusecret.es")).Click();
                new SelectElement(driver.FindElement(By.Id("ContentPlaceHolder1_alberto@ubusecret.es"))).SelectByText("Usuario");
                try
                {
                    Assert.AreEqual("Usuario", driver.FindElement(By.Id("ContentPlaceHolder1_alberto@ubusecret.es")).GetAttribute("value"));
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("alberto@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Alberto1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("Hola Alberto Hernández! (Cerrar Sesión)", driver.FindElement(By.Id("LblUsuario")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                driver.FindElement(By.XPath("//a[@id='Admin']/span")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_alberto@ubusecret.es")).Click();
                new SelectElement(driver.FindElement(By.Id("ContentPlaceHolder1_alberto@ubusecret.es"))).SelectByText("Deshabilitado");
                driver.FindElement(By.Id("LblUsuario")).Click();
            }
        }
    }
}
