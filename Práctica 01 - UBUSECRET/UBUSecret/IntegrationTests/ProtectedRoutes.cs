using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace IntegrationTests
{
    [TestClass]
    public class ProtectedRoutesTests
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
            baseURL = "https://localhost:44319";
            Data.DBPruebas.ObtenerInstacia().SoftReset();
        }

        [ClassCleanup]
        public static void CleanupClass()
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
        public void ThePortectedRouteCrearSecretoNoLogTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/crearSecreto.aspx");
            try
            {
                Assert.AreEqual("Usuario no Autenticado", driver.FindElement(By.Id("ErrMsg")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }

        [TestMethod]
        public void TheProtectedRouteDetallesSecretoNoLogTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/secreto.aspx");
            try
            {
                Assert.AreEqual("Usuario no autenticado", driver.FindElement(By.Id("ErrMsg")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        [TestMethod]
        public void TheProtectedRouteNoLogGestorTest()
        {
            driver.Navigate().GoToUrl(baseURL +"/gestor.aspx");
            try
            {
                Assert.AreEqual("Usuario No Autenticado", driver.FindElement(By.Id("ErrMsg")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        [TestMethod]
        public void TheProtectedRouteNoGestorAccesoAGestorTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div")).Click();
            driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("juan@ubusecret.es");
            driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Juan1122");
            driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
            driver.Navigate().GoToUrl(baseURL + "/gestor.aspx");
            try
            {
                Assert.AreEqual("No tienes permisos para realizar esta acción.", driver.FindElement(By.Id("ErrMsg")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("LblUsuario")).Click();
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div")).Click();
        }


        [TestMethod]
        public void TheProtectedRouteListadoSecretosNoLogTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/secretos.aspx");
            
            try
            {
                Assert.AreEqual("Usuario No Autenticado", driver.FindElement(By.Id("ErrMsg")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }

    }
}
