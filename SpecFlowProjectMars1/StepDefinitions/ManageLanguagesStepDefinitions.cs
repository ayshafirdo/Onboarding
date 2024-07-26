using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SpecFlowProjectMars1.Pages;
using System;
using System.Reflection.Emit;
using TechTalk.SpecFlow;
using SpecFlowProjectMars1.Utilities;

namespace SpecFlowProjectMars1.StepDefinitions
{
    [Binding]
    public class ManageLanguagesStepDefinitions:CommonDriver
    {
        [Given(@"the language ""([^""]*)"" with level ""([^""]*)"" is already present")]
        public void GivenTheLanguageWithLevelIsAlreadyPresent(string language, string level)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Navigate to the languages section if not already there
            IWebElement languageSectionButton = driver.FindElement(By.XPath("//a[contains(text(),'Languages')]"));
            languageSectionButton.Click();

            // Wait for the languages section to load
            wait.Until(d => d.FindElement(By.XPath("//h3[text()='Languages']")));

            try
            {
                // Check if the language with the specified level is already in the list
                IWebElement existingLanguage = driver.FindElement(By.XPath($"//td[text()='{language}']/following-sibling::td[text()='{level}']"));
                if (existingLanguage != null)
                {
                    // The language is already in the list with the specified level
                    Console.WriteLine($"The language '{language}' with level '{level}' is already in the list.");
                    return;
                }
            }
            catch (NoSuchElementException)
            {
                // Language with the specified level is not found in the list, so we need to add it
                Console.WriteLine($"The language '{language}' with level '{level}' is not in the list. Adding it now.");

                // Click on the "Add New" button
                IWebElement addNewButton = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'ui teal button') and text()='Add New']")));
                addNewButton.Click();

                // Enter the language name
                IWebElement languageNameInput = wait.Until(d => d.FindElement(By.XPath("//input[contains(@placeholder,'Add Language')]")));
                languageNameInput.Clear();
                languageNameInput.SendKeys(language);

                // Select the language level from the dropdown
                IWebElement languageLevelDropdown = wait.Until(d => d.FindElement(By.XPath("//select[@class='ui dropdown']")));
                SelectElement selectLanguageLevel = new SelectElement(languageLevelDropdown);
                selectLanguageLevel.SelectByText(level);

                // Click the "Add" button
                IWebElement addButton = driver.FindElement(By.XPath("//input[@value='Add']"));
                addButton.Click();

                // Verify that the language is added
                wait.Until(d => d.FindElement(By.XPath($"//td[text()='{language}']/following-sibling::td[text()='{level}']")));
            }
        }
    }
}
