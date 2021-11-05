using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SeleniumTests
{
    [TestClass]
    public class CP01TenerLasRespuestasObligatoriasMarcadas
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
        public void TheCP01TenerLasRespuestasObligatoriasMarcadasTest()
        {
            //Abrimos la web
            driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform");

            //Clic en si consumimos colacao
            driver.FindElement(By.XPath("//div[@id='i5']/div[3]/div")).Click();

            //Marcamos algunas preguntas dejando obligatorias sin marcar
            
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span")).Click();

            
            driver.FindElement(By.XPath("//div[@id='i10']/div[2]")).Click();

            //Clic en continuar
            
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span/span")).Click();

            //Comprobamos que aparece el texto de error
            

            
            try
            {
                Assert.AreEqual(" En esta pregunta debes introducir una respuesta por fila", driver.FindElement(By.Id("i39")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
             
             

            //Assert.AreEqual("En esta pregunta debes introducir una respuesta por fila", driver.FindElement(By.Id("i39")).Text);
        
        }

    }
}
