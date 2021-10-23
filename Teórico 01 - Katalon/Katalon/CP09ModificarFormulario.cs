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
    public class CP09ModificarFormulario
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
        public void TheCP09ModificarFormularioTest()
        {
            // Abre el Formulario
            driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSeLqx5UUpNwRN5g6UmT_IUhleG1r_dwDtmAC-yGL0OdhqKZ1Q/viewform");
            // Resetea el Formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div[3]/div/span/span")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[2]/following::span[2]")).Click();
            // Comprueba que no haya elementos marcados
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div.isChecked")).Count);
            // Selecciona NO
            driver.FindElement(By.XPath("//div[@id='i8']/div[3]/div")).Click();
            
            // Cambia de Página
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();

            Thread.Sleep(300);
            // Envía el forumario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span/span")).Click();
            Thread.Sleep(300);
            // Clicka en Modificar Respuesta
            driver.FindElement(By.LinkText("Modificar tu respuesta")).Click();

            Thread.Sleep(300);
            // Comprueba que se han guardado las respuestas (QUe esté Seleccionado NO)
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div[id=i5].isChecked")).Count);
            Assert.AreEqual(1, driver.FindElements(By.CssSelector("div[id=i8].isChecked")).Count);
            try
            {
                // Comrpueba que estemos en modo editar al tener cierto mensaje en específico
                Assert.AreEqual("Estás editando tu respuesta. Si compartes esta URL, otros usuarios también podrán editar la respuesta.", driver.FindElement(By.Id("J9Hpafc0")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Selecciona SI
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div/div/div/div[2]/div/div/span/div/div/label/div")).Click();

            // Cambia de Página
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div/span/span")).Click();
            // Selecciona una opción del selector obligatorio con Checkboxes
            driver.FindElement(By.XPath("//div[@id='i10']/div[2]")).Click();
            // Rellena las 7 posiciones de la tabla de forma aleatoria
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[2]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[4]/span/div[2]/div/div/div[3]")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[6]/span/div[2]/div/div/div[3]")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[8]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[10]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[12]/span/div[2]/div/div/div[3]/div")).Click();
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[2]/div[6]/div/div/div[2]/div/div/div/div[14]/span/div[2]/div/div/div[3]/div")).Click();
            
            // Envía el Formulario
            driver.FindElement(By.XPath("//form[@id='mG61Hd']/div[2]/div/div[3]/div/div/div[2]/span/span")).Click();
            
            try
            {
                // Comprueba que se ha enviado el formulario
                Assert.AreEqual("Se ha registrado tu respuesta", driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Estudio Consumidores de ColaCao'])[2]/following::div[1]")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }

    }
}
