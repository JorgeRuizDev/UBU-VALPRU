using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class CP02FechasYHoras
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;
        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
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
        public void TheCP02FechasYHorasTest()
        {
            //Abrimos la web
            driver.
                Navigate().
                GoToUrl(
                "https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform"
                );

            //Reseteamos el formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
            Thread.Sleep(300);

            //Clic en si consumimos colacao
            driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();
            
            //Clic en continuar
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
            
            //Introducimos 23 en el dia
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div/div/div[2]/div/div/div/input")).SendKeys("23");
            
            //Introducimos 22 en el mes
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/input")).SendKeys("22");
            
            //Introducimos 1999 en el año
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[5]/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[5]/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[5]/div/div[2]/div/div/div/input")).SendKeys("1999");

            //Comprobamos que aparezca el mensaje fecha no valida
            Thread.Sleep(300);
            driver.FindElement(By.Id("i30")).Click();
            try
            {
                Assert.AreEqual(" Fecha no válida", driver.FindElement(By.Id("i30")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            //Reseteamos el formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
            Thread.Sleep(300);

            //Clic en si consumimos colacao
            driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();

            //Clic en continuar
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();

            //Introducimos 23 en el dia
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div/div/div[2]/div/div/div/input")).SendKeys("23");

            //Introducimos 03 en el mes
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/input")).SendKeys("03");
            
            //Introducimos 1999 en el año
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[5]/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[5]/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div/div[5]/div/div[2]/div/div/div/input")).SendKeys("1999");

            //Introducimos 33 en la hora
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div[2]/div/div/div/div[2]/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div[2]/div/div/div/div[2]/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div[2]/div/div/div/div[2]/div/div/div/input")).SendKeys("33");

            //Introducimos 33 en el minuto
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div[2]/div/div/div[3]/div/div/div/div/input")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div[2]/div/div/div[3]/div/div/div/div/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div/div[2]/div/div/div[3]/div/div/div/div/input")).SendKeys("33");
            
            //Comprobamos que aparezca un mensaje de hora incorrecta
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[4]/div/div/div[2]/div")).Click();
            try
            { 
                Assert.AreEqual(" Hora no válida", driver.FindElement(By.Id("i30")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        
        }


        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
