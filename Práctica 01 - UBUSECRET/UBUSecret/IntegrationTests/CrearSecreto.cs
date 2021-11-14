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
    public class CrearSecreto
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
        public void TheMensajeVacioTest()
        {
            foreach (var driver in drivers)
            {
                //driver.Navigate().GoToUrl("https://localhost:44319/registro.aspx");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_CrearSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Debe haber caracteres en el mensaje", driver.FindElement(By.Id("ContentPlaceHolder1_ErrSecreto")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
            }


        }

        [TestMethod]
        public void TheMensajeLargoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("https://localhost:44319/default.aspx");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_CrearSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).Click();
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div/div/section[4]")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).SendKeys("dsalñknkkvkkkkkckkkkkkkkkkkkkkkkkkkkkkkkkkkkkckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckckcc");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("No se puede superar 250 caracteres", driver.FindElement(By.Id("ContentPlaceHolder1_ErrSecreto")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_Label1")).Click();
                driver.FindElement(By.Id("LblUsuario")).Click();
            }
        }

        [TestMethod]
        public void TheCorreoIncorrectoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("https://localhost:44319/default.aspx");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor1122");
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_CrearSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).SendKeys("asdfasdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).SendKeys("dfsd");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Email/s no validos", driver.FindElement(By.Id("ContentPlaceHolder1_ErrMails")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
            }
        }



        [TestMethod]
        public void TheCrearSecretoTest()
        {

            foreach (var driver in drivers)
            {


                driver.Navigate().GoToUrl("https://localhost:44319/default.aspx");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("Gestor1122");
                driver.FindElement(By.XPath("//form[@id='form1']/div[3]/div")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_Send")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_CrearSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMails")).SendKeys("gestor@ubusecret.es");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxSecreto")).SendKeys("prueba");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                driver.FindElement(By.XPath("//div[@id='ContentPlaceHolder1_Secretos']/a[1]/section/div[1]")).Click();
                try
                {
                    Assert.AreEqual("Sin Titulo", driver.FindElement(By.Id("ContentPlaceHolder1_titulo")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                try
                {
                    Assert.AreEqual("Gestor", driver.FindElement(By.Id("ContentPlaceHolder1_autor")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                try
                {
                    Assert.AreEqual("prueba", driver.FindElement(By.Id("ContentPlaceHolder1_secreo")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("LblUsuario")).Click();
            }
        }
    }
    
}
