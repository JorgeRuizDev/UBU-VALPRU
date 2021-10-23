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
    public class CP08BotonAtras
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
        public void TheCP08BotNAtrSTest()
        {
            // Abre la web
            driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform?fbzx=1625049101332484430");
            
            // Resetea el Formulaio
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
            // Comrpueba que no haya ningún elemento marcado
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div.isChecked")).Count);
            // Selecciona NO
            driver.FindElement(By.XPath("//div[@id='i8']/div[3]/div")).Click();
            // Comrpueba que Haya un botón marcado
            Assert.AreEqual(1, driver.FindElements(By.CssSelector("div.isChecked")).Count);
            // Cambia de página
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
            // Retrocede la página
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
            // Comrpeuba que SI no esté marcado y NO lo esté
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div[id=i5].isChecked")).Count);
            Assert.AreEqual(1, driver.FindElements(By.CssSelector("div[id=i8].isChecked")).Count);
            // Resetea el Formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
        }

    }
}
