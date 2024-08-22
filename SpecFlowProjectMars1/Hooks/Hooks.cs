﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowProjectMars1.Pages;
using SpecFlowProjectMars1.Utilities;
using TechTalk.SpecFlow;

namespace SpecFlowProjectMars1.Hooks
{

    [Binding]
    public class Hooks: CommonDriver
    {

       
        [BeforeScenario("language")]
        public void BeforeScenarioLanguage()
        {

            LoginActions loginPageobj = new LoginActions();
            loginPageobj.Login("test123456@test.com", "test123456");
            ClearLanguageTestData();
        }
        [BeforeScenario("skill")]
        public void BeforeScenarioSkill()
        {
            LoginActions loginPageobj = new LoginActions();
            loginPageobj.Login("test123456@test.com", "test123456");
            ClearSkillTestData();
        }

        [AfterScenario("language")]
        public void AfterScenarioLanguage()
        {
            ClearLanguageTestData();
            driver.Quit();
        }

        [AfterScenario("skill")]
        public void AfterScenarioSkill()
        {
            ClearSkillTestData();
            driver.Quit();
        }

        private void ClearSkillTestData()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            //Navigate to skill
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();

            var count = driver.FindElements(By.XPath("//tbody/tr[1]/td[3]/span[2]")).Count;
            // Clear all skills
            for (int i = 0; i < count; i++)
            {


                try
                {

                    var skillDeleteButtons = driver.FindElements(By.XPath("//tbody/tr[1]/td[3]/span[2]"));
                    {
                        try
                        {
                            skillDeleteButtons[0].Click();
                        }
                        catch (WebDriverException ex)
                        {
                            Console.WriteLine($"Error interacting with delete button: {ex.Message}");
                        }


                    }
                }
                catch (NoSuchElementException)
                {
                    // If no elements are found, ignore
                    Console.WriteLine("No skill delete buttons found.");
                }
                catch (WebDriverTimeoutException)
                {
                    // If the wait times out, ignore
                    Console.WriteLine("Timeout while waiting for skill delete buttons.");
                }
            }
        }
        private void ClearLanguageTestData()
        {
            
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            var countLang = driver.FindElements(By.XPath("//tbody/tr[1]/td[3]/span[2]")).Count;
            // Clear all languages
            for (int i = 0; i < countLang; i++)
            {


                try
                {

                    var langDeleteButtons = driver.FindElements(By.XPath("//tbody/tr[1]/td[3]/span[2]"));
                    {
                        try
                        {
                            langDeleteButtons[0].Click();
                        }
                        catch (WebDriverException ex)
                        {
                            Console.WriteLine($"Error interacting with delete button: {ex.Message}");
                        }


                    }
                }
                catch (NoSuchElementException)
                {
                    // If no elements are found, ignore
                    Console.WriteLine("No language delete buttons found.");
                }
                catch (WebDriverTimeoutException)
                {
                    // If the wait times out, ignore
                    Console.WriteLine("Timeout while waiting for language delete buttons.");
                }
            }


            Thread.Sleep(2000);

        }

    }

}


