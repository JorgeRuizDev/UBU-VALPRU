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
    public class CP06RadioButtons
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
        public void TheCP06RadioButtonsTest()
        {   
            // Abre el formulario
            driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform");

            // Resetea el Formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();

            // Selecciona NO
            driver.FindElement(By.XPath("//div[@id='i8']/div[3]/div")).Click();
            Thread.Sleep(300);
            // Comprueba que NO esté marcado y SI no lo esté
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div[id=i5].isChecked")).Count);
            Assert.AreEqual(1, driver.FindElements(By.CssSelector("div[id=i8].isChecked")).Count);

            // Selecciona SI
            driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();
            // Comprueba que SI esté marcado y NO no lo esté
            Thread.Sleep(300);
            Assert.AreEqual(1, driver.FindElements(By.CssSelector("div[id=i5].isChecked")).Count);
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div[id=i8].isChecked")).Count);
            // Pulsa siguiente / Cambio de página
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();

            // Comprueba que no haya ningún Radio Button Marcado
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div.isChecked")).Count);

            // Clicka varias veces los Radio Buttons de cada fila
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[2]/div/div/div[2]/div/span/div/label[2]/div[2]/div/div/div[3]")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[2]/div/div/div[2]/div/span/div/label[5]/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[2]/div/div/div[2]/div/span/div/label[9]/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[2]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[2]/span/div[5]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[4]/span/div[3]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[4]/span/div[6]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[6]/span/div[4]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[6]/span/div[3]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[8]/span/div[3]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[8]/span/div[5]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[10]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[10]/span/div[6]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[12]/span/div[5]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[12]/span/div[3]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[12]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[3]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[4]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[5]/div/div/div[3]")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[6]/div/div/div[3]/div")).Click();
            Thread.Sleep(300);
            // Comrpueba que solo haya 8 radio buttons marcados en esta página
            Assert.AreEqual(8, driver.FindElements(By.CssSelector("div.isChecked")).Count);

            // Envía el formulario
            driver.FindElement(By.XPath("//div[@id='i10']/div[2]")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span")).Click();
            try
            {
                // comprueba que el formulario se ha enviado
                Assert.AreEqual("Se ha registrado tu respuesta", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Estudio Consumidores de ColaCao'])[2]/following::div[1]")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }

    }
}
