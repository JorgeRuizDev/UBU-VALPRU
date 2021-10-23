using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestClass]
    public class CP10Progreso
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
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
        public void TheCP10ProgresoTest()
        {

            try
            {
                // Abre el formulario
                driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform");
                // Comprueba que estemos en la primera Página
                Assert.AreEqual("Página 1 de 2", driver.FindElement(By.Id("lpd4pf")).Text);

                // Selecciona SI
                driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();
                // Cambia de Página
                driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();

                // Comprueba que el progreso se ha actualizado
                Assert.AreEqual("Página 2 de 2", driver.FindElement(By.Id("lpd4pf")).Text);

                // Retrocede la página
                driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
                
                // Comprueba que estemos en la primera Página
                Assert.AreEqual("Página 1 de 2", driver.FindElement(By.Id("lpd4pf")).Text);

                // Selecciona NO (En este caso hemos pulsado en el texto)
                driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div/div/div/div[2]/div/div/span/div/div[2]/label/div")).Click();

                // cambia de página
                driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();

                // Comprueba que el progreso se ha actualizado

                Assert.AreEqual("Página 2 de 2", driver.FindElement(By.Id("lpd4pf")).Text);

                // Envía el formulario
                driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span/span")).Click();

                // Comprueba que el formulario se ha enviado
                Assert.AreEqual("Se ha registrado tu respuesta", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Estudio Consumidores de ColaCao'])[2]/following::div[1]")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }

    }
}
