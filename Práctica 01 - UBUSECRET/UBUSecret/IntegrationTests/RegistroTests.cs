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
    public class RegistroTests
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
        public void ThePruebaRegMailIncorrectoTest()
        {

            foreach (var driver in drivers) {

                driver.Navigate().GoToUrl("https://localhost:44319/");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSignUp")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxMail")).SendKeys("adsfnnsdfsadf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw")).SendKeys("lksadjfl");
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_BoxPassw2")).SendKeys("sldkjflkd");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName")).SendKeys("sdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxSurName")).SendKeys("sdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Click();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TextPhone")).SendKeys("dfsasdf");
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnSend")).Click();
                try
                {
                    Assert.AreEqual("Email Mal", driver.FindElement(By.Id("ContentPlaceHolder1_ErrMail")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("ContentPlaceHolder1_BtnCancel")).Click();

            }

            
        }









    }
    
}
