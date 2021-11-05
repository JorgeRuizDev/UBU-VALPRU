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
    public class CP11VariasRespuestasPorUsuario
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;
        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
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
        public void TheCP11VariasRespuestasPorUsuarioTest()
        {
            driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform");
            driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("//div[@id='i10']/div[2]")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[2]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[4]/span/div[3]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[6]/span/div[4]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[8]/span/div[5]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[10]/span/div[6]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[12]/span/div[5]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[4]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span/span")).Click();
            try
            {
                Assert.AreEqual("Se ha registrado tu respuesta", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Estudio Consumidores de ColaCao'])[2]/following::div[1]")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Enviar otra respuesta")).Click();
            driver.FindElement(By.XPath("//div[@id='i8']/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span/span")).Click();
            try
            {
                Assert.AreEqual("Se ha registrado tu respuesta", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Estudio Consumidores de ColaCao'])[2]/following::div[1]")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
 
    }
}
