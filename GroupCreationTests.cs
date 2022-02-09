using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            InitNewGropCreation();
            FillGroupForm(new GroupData("Lol", "Kek", "Cheburek"));
            SubmitGroupCreation();
            ReturnToGroupPage();
            Logout();
        }

        private void Logout()
        {
            driver.FindElement(By.CssSelector("a")).Click();
        }

        private void ReturnToGroupPage()
        {
            driver.FindElement(By.CssSelector("a[href='group.php']")).Click();
        }

        private void SubmitGroupCreation()
        {
            driver.FindElement(By.CssSelector("input[name=\"submit\"]")).Click();
        }

        private void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.CssSelector("input[name='group_name']")).SendKeys(group.Name);
            driver.FindElement(By.CssSelector("textarea[name=\"group_header\"]")).SendKeys(group.Header);
            driver.FindElement(By.CssSelector("textarea[name=\"group_footer\"]")).SendKeys(group.Footer);
        }

        private void InitNewGropCreation()
        {
            driver.FindElement(By.CssSelector("input[name=\"new\"]")).Click();
        }

        private void GoToGroupsPage()
        {
            driver.FindElement(By.CssSelector("a[href='group.php']")).Click();
        }

        private void Login(AccountData account)
        {
            driver.FindElement(By.CssSelector("input[name=\"user\"]")).SendKeys(account.Username);
            driver.FindElement(By.CssSelector("input[name=\"pass\"]")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
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

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}

