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
    public class RegistroTests
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

        [TestMethod]
        public void ThePruebaRegMailIncorrectoTest()
        {

            foreach (var driver in drivers) {

                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSignUp")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("adsfnnsdfsadf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("lksadjfl");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("sldkjflkd");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).SendKeys("sdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).SendKeys("sdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).SendKeys("dfsasdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Email Mal", driver.FindElement(By.Id("ContentPlaceHolder1_ErrMail")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();

            }
        }

        [TestMethod]
        public void ThePruebaRegFuerzaContaseATest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSignUp")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("asdfahlkfn");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("gfdkjshd");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("sdkjfks");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).SendKeys("dfs");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).SendKeys("sd");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).SendKeys("sdfs");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("La contraseña no es lo suficientemente fuerte", driver.FindElement(By.Id("ContentPlaceHolder1_ErrPassw")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();

            }
        }


        [TestMethod]
        public void ThePruebaRegContaseANoCoincideTest()
        {

            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSignUp")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("asdfahlkfn");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("gfdkjshd");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("sdkjfks");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).SendKeys("dfs");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).SendKeys("sd");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).SendKeys("sdfs");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Las contraseñas no coinciden", driver.FindElement(By.Id("ContentPlaceHolder1_ErrPassw2")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();
            }
        }


        [TestMethod]
        public void ThePruebaRegCancelarTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSignUp")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();
                try
                {
                    Assert.AreEqual("Inicio de Sesión.", driver.FindElement(By.Id("ContentPlaceHolder1_Label2")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }


        [TestMethod]
        public void TheRegTelefonoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSignUp")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("daSDA");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("ASDasd");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("asd");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).SendKeys("asdsadf");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).SendKeys("asda");
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div/div")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).SendKeys("23esadf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Telefono erroneo", driver.FindElement(By.Id("ContentPlaceHolder1_ErrPhone")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();
            }
        }














    }
    
}
