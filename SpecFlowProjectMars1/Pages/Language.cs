using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using SpecFlowProjectMars1.Utilities;
using SpecFlowProjectMars1.Hooks1;

namespace SpecFlowProjectMars1.Pages
{
    public class Language: CommonDriver
    {
        // Locators
        private readonly By addNewButtonLocator = By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div");
        private readonly By languageNameInputLocator = By.XPath("//input[contains(@placeholder,'Add Language')]");
        private readonly By languageLevelDropdownLocator = By.XPath("//div[@id='account-profile-section']//select[@class='ui dropdown']");
        private readonly By addLanguageButtonLocator = By.XPath("//input[contains(@value, 'Add')]");
        private readonly By validationMessageLocator = By.XPath("//div[@class='ns-box-inner' and contains(text(), 'Please enter language and level')]");

        

        // Method to add a new language
        public void AddNewLanguage(string languageName, string languageLevel)
        {
            Thread.Sleep(1000);

            // Click the add new button
            driver.FindElement(addNewButtonLocator).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement languageNameInput = wait.Until(d => d.FindElement(languageNameInputLocator));
            languageNameInput.Clear();
            languageNameInput.SendKeys(languageName);

            // Select the language level from the dropdown
            IWebElement languageLevelDropdown = wait.Until(d => d.FindElement(languageLevelDropdownLocator));
            SelectElement selectLanguageLevel = new SelectElement(languageLevelDropdown);
            Thread.Sleep(1000);
            selectLanguageLevel.SelectByText(languageLevel);

            // Click the add language button
            driver.FindElement(addLanguageButtonLocator).Click();

            // Verify the toast message for empty language name
            if (string.IsNullOrWhiteSpace(languageName))
            {
                try
                {
                    IWebElement validationMessage = wait.Until(d => d.FindElement(validationMessageLocator));
                    Assert.AreEqual("Please enter language and level", validationMessage.Text);
                    IWebElement cancelButton=wait.Until(d=>d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
                    cancelButton.Click();
                }
                catch (WebDriverTimeoutException)
                {
                    throw new Exception("Expected validation message 'Please enter language and level' was not displayed.");
                }
            }
            else
            {
                // Wait for the language to be added to the table
                wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'{languageName}')]")));
            }

            Thread.Sleep(8000);
          
            
        }
       

        


        public bool VerifyLanguageAdded(string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
            {
                // If the language name is empty or whitespace, return false immediately
                Console.WriteLine("Language name is empty or whitespace.");
                return false;
            }

            try
            {
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait until the language is displayed in the table
                IWebElement languageElement = wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'{languageName}')]")));

                // If the language element is found, return true
                return languageElement != null;
            }
            catch (NoSuchElementException)
            {
                // If the element is not found, return false
                Console.WriteLine($"Language '{languageName}' was not found in the list.");
                return false;
                
            }
            catch (WebDriverTimeoutException)
            {
                // If the wait times out, return false
                Console.WriteLine($"Timeout while waiting for language '{languageName}' to be added to the list.");
                return false;
            }
        }

        public void EditLanguage(string oldLang, string newLang)
        {
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //Click on edit button
            IWebElement editButtonLang = wait.Until(d => d.FindElement(By.XPath($"//tbody/tr[td[contains(text(),'{oldLang}')]]/td[3]/span[1]/i[1]")));
            editButtonLang.Click();

            IWebElement languageNameInput2 = wait.Until(d => d.FindElement(By.XPath("//input[contains(@placeholder,\"Add Language\")]")));
            languageNameInput2.Clear();
            languageNameInput2.SendKeys(newLang);

            System.Threading.Thread.Sleep(5000);

            //Click on update button
            IWebElement updateLangButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Update')]")));
            updateLangButton.Click();
            System.Threading.Thread.Sleep(5000);
        }

        public bool VerifyLanguageUpdated(string languageName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            try
            {
                // Check if the updated language name is displayed in the table
                IWebElement languageElement = wait.Until(d => d.FindElement(By.XPath($"//td[text()='{languageName}']")));
                return languageElement != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void DeleteLanguage(string language1)
        {
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Delete language
            IWebElement delLangButton = wait.Until(d => d.FindElement(By.XPath($"//tbody/tr[td[1][contains(text(),'{language1}')]]/td[3]/span[2]/i[1]")));
            delLangButton.Click();
        }

        public bool VerifyLanguageDeleted(string language1)
        {

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                try
                {
                
                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                
                Thread.Sleep(2000); 
                                    
                wait.Until(d => d.FindElement(By.XPath($"//td[text()='{language1}']")));
                    Thread.Sleep(1000);
                    // If the language element is found, return false
                    Console.WriteLine($"Language '{language1}' was found in the list.");
                    return false;
                }
                catch (WebDriverTimeoutException)
                {
                    
                    Console.WriteLine($"Language '{language1}' was not found in the list.");
                    return true;
                }
            catch (NoSuchElementException)
            {
                // If the language element is not found, return true
                Console.WriteLine($"Language '{language1}' was not found in the list.");
                return true;
            }

        }

        public void AddNewLanguageWithoutLevel(string languageName)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cancelButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
            cancelButton.Click();

            // Adding Language
            driver.FindElement(addNewButtonLocator).Click();
            Thread.Sleep(1000);
          

            // Locate the language name input field
            IWebElement languageNameInput = wait.Until(d => d.FindElement(languageNameInputLocator));
            languageNameInput.Clear();
            languageNameInput.SendKeys(languageName);

            // Leave the language level empty 
            // click the add language button
            driver.FindElement(addLanguageButtonLocator).Click();
        }
        
        

        public void AddExistingLanguageAndLevel(string languageName, string languageLevel)
        {
            Thread.Sleep(1000);
            //Adding Language
            driver.FindElement(addNewButtonLocator).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Locate the language name input field
            IWebElement languageNameInput = wait.Until(d => d.FindElement(languageNameInputLocator));
            languageNameInput.Clear();
            languageNameInput.SendKeys(languageName);

            // Locate the language level dropdown
            IWebElement languageLevelDropdown = wait.Until(d => d.FindElement(languageLevelDropdownLocator));
            SelectElement selectLanguageLevel = new SelectElement(languageLevelDropdown);
            Thread.Sleep(1000);
            selectLanguageLevel.SelectByText(languageLevel);

            // Locate and click the add language button
            driver.FindElement(addLanguageButtonLocator).Click();
            
        }
        public void VerifyValidation()
        { 

            // Verify that a validation message or error appears
            try
            {
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement validationMessage = wait.Until(d => d.FindElement(By.XPath("//div[@class='ns-box-inner' and contains(text(), 'This language is already exist in your language list')]")));
                Assert.AreEqual("This language is already exist in your language list.", validationMessage.Text);
                Console.WriteLine("Test passed: User cannot add a existing language and level.");
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Expected validation message 'This language is already exists in your language list' was not displayed.");
            }

            System.Threading.Thread.Sleep(8000);
        }

        // Method to verify if an error message is displayed
        public bool VerifyErrorMessageDisplayed(string expectedErrorMessage)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                IWebElement errorMessageElement = wait.Until(d => d.FindElement(By.XPath($"//div[contains(@class,'ns-box-inner') and contains(text(),'{expectedErrorMessage}')]")));
                // Capture the error message immediately
                string actualMessage = errorMessageElement.Text;
                return actualMessage.Contains(expectedErrorMessage);
                IWebElement cancelButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
                cancelButton.Click();
                
            
            }
           
        
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            

        }
        
        public bool IsSystemHandlingGracefully(string languageName)
        {
            try
            {
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                
                // Check if a substring of the language name is present
                IWebElement languageElement = wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'AAAAA')]")));

                return languageElement != null;
            }
            catch (NoSuchElementException)
            {
                
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                
                return false;
            }
        }







    }
}
