using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.IO;

namespace CSGSIWebClient.SeleniumTests
{
    public class CSGSIWebClientUITests
    {
        private readonly bool _isHeadless = false;
        private readonly string _url = @"http://localhost:5000";

        [Fact]
        public void DataTablesTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("--headless");
            
            using (var driver = _isHeadless ? new FirefoxDriver(options) : new FirefoxDriver())
            {                
                Login(driver);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

                IWebElement page5 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div/div/div/div[3]/div[2]/div/ul/li[5]/a")));
                page5.Click();

                IWebElement previous = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div/div/div/div[3]/div[2]/div/ul/li[1]/a")));
                previous.Click();

                IWebElement next = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div/div/div/div[3]/div[2]/div/ul/li[8]/a")));
                next.Click();
                
                var comboBox = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div/div[1]/div[1]/div/label/select")));
                Thread.Sleep(1000);
                new SelectElement(comboBox).SelectByText("25");
                Thread.Sleep(1000);

                var dataTableSearchField = driver.FindElementByXPath("/html/body/div/div/div/div/div[1]/div[2]/div/label/input");
                dataTableSearchField.SendKeys("cache");

                IWebElement match7 = driver.FindElementByXPath("//*[@id=\"match__7\"]");

                var actions = new Actions(driver);
                actions.MoveToElement(match7);
                actions.Click();
                actions.Perform();
                
                Thread.Sleep(2000);
                
                driver.Close();
            }
        }
        
        [Fact]
        public void ProfileGraphsTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("--headless");
            
            using (var driver = _isHeadless ? new FirefoxDriver(options) : new FirefoxDriver())
            {                
                Login(driver);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

                IWebElement profileLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/nav/div/div[2]/form/ul/li[2]/a")));
                profileLink.Click();

                var comboBox = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"chart-selection\"]")));
                Thread.Sleep(1000);
                new SelectElement(comboBox).SelectByText("Minutes Played per Team");
                Thread.Sleep(1000);
                new SelectElement(comboBox).SelectByText("Kill Death Ratio by Map");
                Thread.Sleep(1000);
                new SelectElement(comboBox).SelectByText("Kill Death Ratio by Team");
                Thread.Sleep(1000);
                new SelectElement(comboBox).SelectByText("Wins by Team");
                Thread.Sleep(1000);
                
                driver.Close();
            }
        }

        private void Login(FirefoxDriver driver)
        {
            Credentials credentials = GetCredentials();
            driver.Navigate().GoToUrl(_url);                
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            
            var loginLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Log in")));            
            loginLink.Click();
            
            var loginUsernameTextField = wait.Until(ExpectedConditions.ElementExists(By.Id("input-username")));
            loginUsernameTextField.SendKeys(credentials.Username);
            
            IWebElement loginPasswordTextField = driver.FindElement(By.Id("input-password"));
            loginPasswordTextField.SendKeys(credentials.Password);
            
            IWebElement loginForm = driver.FindElementByClassName("form-horizontal");
            loginForm.Submit();
        }

        private Credentials GetCredentials()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string[] lines = System.IO.File.ReadAllLines($@"{projectDirectory}/credentials.txt");
            return new Credentials(){ Username = lines[0], Password = lines[1] };
        }

        private class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
