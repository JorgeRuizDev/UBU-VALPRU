using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Edge;

namespace IntegrationTests
{
    [TestClass]
    public class PasswordChangeTests
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
        public void BotonCambiarContraseACancelarTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL + "/cambiarPassword.aspx");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();
                try
                {
                    Assert.AreEqual("Inicio de Sesi?n.", driver.FindElement(By.Id("ContentPlaceHolder1_Label2")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }

        [TestMethod]
        public void CambiarContrasenaNoCoincideTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_HyperLink1")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).SendKeys("Gestor1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Paco11");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("Paco1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Las contrase?as no coinciden", driver.FindElement(By.Id("ContentPlaceHolder1_ErrPassw")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }

        [TestMethod]
        public void TheCambiarMismaContraseATest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_HyperLink1")).Click();
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("juan@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Las contrase?as Son Igulaes", driver.FindElement(By.Id("ContentPlaceHolder1_ErrPassw")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }


        [TestMethod]
        public void TheCambioContraseACorrectoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("https://localhost:44319/default.aspx");
                driver.FindElement(By.Id("ContentPlaceHolder1_HyperLink1")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("juan@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("Juan112233");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Juan112233");
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div/div")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Hola Juan Carlos! (Cerrar Sesi?n)", driver.FindElement(By.Id("LblUsuario")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_HyperLink1")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("juan@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).SendKeys("Juan112233");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("Juan1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                driver.FindElement(By.Id("LblUsuario")).Click();
            }
        }

        [TestMethod]
        public void TheEmailCambioIncorrectoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_HyperLink1")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).SendKeys("Gestor1122");
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxOld")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("sadfasdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("asdfasdfa");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Formato de Email Incorrecto", driver.FindElement(By.Id("ContentPlaceHolder1_ErrMail")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }
    }
}
