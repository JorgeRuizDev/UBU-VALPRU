using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace SeleniumTests
{
    [TestClass]
    public class CP03BotonBorrado
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;

        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

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
        public void TheCP03BotNBorradoTest()
        {
            // Abre la web
            driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/formResponse");
            // Resetea el Formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
            // Selecciona SI
            driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();
            // SIguiente
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();

            // Escribe "prueba"
            driver.FindElement(By.XPath("//input[@type='text']")).Click();
            driver.FindElement(By.XPath("//input[@type='text']")).Clear();
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("prueba");
            
            // Pulsa Borrar
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();

            // Espera a que aparezca el modal
            Thread.Sleep(500);
            
            // Cancela
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Esta acción eliminará tus respuestas a todas las preguntas y no se puede deshacer.'])[1]/following::span[2]")).Click();
            try
            {
                // Comprueba que no se han borrado los datos del campo "prueba"
                Assert.AreEqual("prueba", driver.FindElement(By.XPath("//input[@type='text']")).GetAttribute("value"));
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            
            // Pulsa borrar
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            // Borra
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
            // Pasa a la siguiente Página
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div/div/div/div[2]/div/div/span/div/div/label/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
            try
            {
                // Comprueba que el campo se ha reseteado 
                Assert.AreEqual("", driver.FindElement(By.XPath("//input[@type='text']")).GetAttribute("value"));
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Resetea el formulario
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
        }

    }
}
