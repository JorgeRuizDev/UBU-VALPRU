using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class LoginTests
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
        public void TheInicioUsuarioNoBBDDTest()
        {
            foreach(var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor2@buuu.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("hehehehJHAuadf");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("Usuario o Contraseña Incorrecto", driver.FindElement(By.Id("ContentPlaceHolder1_ErrPassw")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }

        [TestMethod]
        public void TheInicioSesionGestorCorrectoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
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
                try
                {
                    Assert.AreEqual("Panel de Administración", driver.FindElement(By.XPath("//a[@id='Admin']/span")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
            }
        }


        [TestMethod]
        public void TheInicioUsuarioNoHabilitadoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("pepe@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Pepe1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Pepe11");
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("Un administrador debe darte acceso manualmente", driver.FindElement(By.Id("ErrMsg")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }

        [TestMethod]
        public void TheInicioContrasenaIncorrectaTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor11225");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
            }
        }

        [TestMethod]
        public void TheInicioUsuarioNoCambiaContraseATest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("paco@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Paco11");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("No tienes permisos para realizar esta acción. ¿Has cambiado tu contraseña al menos una vez?", driver.FindElement(By.Id("ErrMsg")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }

        [TestMethod]
        public void TheInicioFormatoMailIncorrectoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("gestor@ubu");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                try
                {
                    Assert.AreEqual("Formato Email Incorrecto", driver.FindElement(By.Id("ContentPlaceHolder1_ErrMail")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }
    }
}
