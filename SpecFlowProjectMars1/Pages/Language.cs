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
        private readonly By cancelButtonLocator = By.XPath("//input[contains(@value, 'Cancel')]");
        private readonly By updateButtonLocator = By.XPath("//input[contains(@value, 'Update')]");
        private readonly By langSectionLocator = By.XPath("//a[contains(text(),'Languages')]");
        private readonly By langIndicatorLocator = By.XPath("//h3[contains(text(),'Languages')]");
        
       
        // Track languages added during the test run
        private static List<string> addedLanguages = new List<string>();
        //set the limit to 3 languages
        public const int MaxLanguages = 3;

        WaitUtils utils = new WaitUtils();
        private IWebElement AddNewButton => utils.WaitToBeVisible(addNewButtonLocator,10);
        private IWebElement LanguageNameInput => utils.WaitToBeVisible(languageNameInputLocator,10);
        private IWebElement LanguageLevelDropdown => utils.WaitToBeVisible(languageLevelDropdownLocator,10);
        private IWebElement AddLanguageButton => utils.WaitToBeVisible(addLanguageButtonLocator, 10);
        private IWebElement ValidationMessage => utils.WaitToBeVisible(validationMessageLocator, 10);
        private IWebElement CancelButton => utils.WaitToBeVisible(cancelButtonLocator, 20);
        private IWebElement UpdateButton=>utils.WaitToBeVisible(updateButtonLocator, 10);
        private IWebElement LangSection => utils.WaitToBeVisible(langSectionLocator, 10);
        private IWebElement LangIndicator=>utils.WaitToBeVisible(langIndicatorLocator, 10);
        
        private By deleteButtonLocatorTemplate(string languageName) => By.XPath($"//td[contains(text(),'{languageName}')]/following-sibling::td//span[2]");
        private By editButtonLocatorTemplate(string oldLang) => By.XPath($"//tbody/tr[td[contains(text(),'{oldLang}')]]/td[3]/span[1]/i[1]");
        private IWebElement EditButton(string oldLang) => utils.WaitToBeVisible(editButtonLocatorTemplate(oldLang),10);
        
        private IWebElement DeleteButtons(string languageName) => utils.WaitToBeVisible(deleteButtonLocatorTemplate(languageName),10);
        private By delLangButtonTemp(string language1) => By.XPath($"//tbody/tr[td[1][contains(text(),'{language1}')]]/td[3]/span[2]/i[1]");
        private IWebElement DelLangButton(string language1)=>utils.WaitToBeVisible(delLangButtonTemp(language1),10);
        private By langElementTemp(string languageName) => By.XPath($"//td[text()='{languageName}']");
        private IWebElement LangElement(string languageName)=>utils.WaitToBeVisible(langElementTemp(languageName),10);
       

        //Navigate to Languages section
        public void LanguageSection()
        {
            LangSection.Click();
        }
        public bool IsOnLanguageSection()
        {
            try
            {
                return LangIndicator.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        // Method to add a new language
        public void AddNewLanguage(string languageName, string languageLevel)
        {
            Thread.Sleep(1000);

            // Click the add new button
            AddNewButton.Click();

            LanguageNameInput.Click();
            LanguageNameInput.SendKeys(languageName);

            // Select the language level from the dropdown
            SelectElement selectLanguageLevel = new SelectElement(LanguageLevelDropdown);
            selectLanguageLevel.SelectByText(languageLevel);

            // Click the add language button
            AddLanguageButton.Click();

            // Verify the toast message for empty language name
            if (string.IsNullOrWhiteSpace(languageName))
            {

                CancelButton.Click();
            }
            else
            {
                LangElement(languageName);
            }

            Thread.Sleep(4000);

        }
        public bool IsValidationMessageDisplayed(string expectedMessage)
        {
            try
            {
                IWebElement message = utils.WaitToBeVisible(validationMessageLocator, 10);
                return message.Text == expectedMessage;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
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
                var languageElement = LangElement(languageName);
                return languageElement != null;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Language '{languageName}' was not found in the list.");
                return false;

            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timeout while waiting for language '{languageName}' to be added to the list.");
                return false;
            }
        }

        public void EditLanguage(string oldLang, string newLang)
        {
          
            //Click on edit button
            EditButton(oldLang).Click();
            LanguageNameInput.Clear();
            LanguageNameInput.SendKeys(newLang);

            //Click on update button
           UpdateButton.Click();
            
        }

        public bool IsLanguageUpdated(string languageName)
        {
            try
            {
                var languageElement = LangElement(languageName);
                return languageElement != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void DeleteLanguage(string language1)
        {
             DelLangButton(language1).Click();
        }

        public bool IsLanguageDeleted(string language1)
        {

            try
            {
                Thread.Sleep(2000);

                utils.WaitToBeVisible(By.XPath($"//td[text()='{language1}']"), 10);
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
            // Adding Language
           AddNewButton.Click();
            Thread.Sleep(1000);
            // Locate the language name input field
            LanguageNameInput.Click();
            LanguageNameInput.SendKeys(languageName);

            // Leave the language level empty 
            // click the add language button
            AddLanguageButton.Click();
        }

        public void AddExistingLanguageAndLevel(string languageName, string languageLevel)
        {
            Thread.Sleep(1000);
            //Adding Language
            AddNewButton.Click();
            // Locate the language name input field
            
            LanguageNameInput.Clear();
            LanguageNameInput.SendKeys(languageName);

            // Locate the language level dropdown
            SelectElement selectLanguageLevel = new SelectElement(LanguageLevelDropdown);
            selectLanguageLevel.SelectByText(languageLevel);

            // Locate and click the add language button
            AddLanguageButton.Click();
            
        }
        

        // Method to verify if an error message is displayed
        public bool VerifyErrorMessageDisplayed(string expectedErrorMessage)
        {
            try
            {
                
                IWebElement errorMessageElement = utils.WaitToBeVisible(By.XPath($"//div[contains(@class,'ns-box-inner') and contains(text(),'{expectedErrorMessage}')]"), 10);
                // Capture the error message immediately
                string actualMessage = errorMessageElement.Text;
                CancelButton.Click();
                return actualMessage.Contains(expectedErrorMessage);
                
            
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
     
                IWebElement languageElement = utils.WaitToBeVisible(By.XPath($"//td[contains(text(),'AAAAA')]"), 10);
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
        public static List<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();

            // Locate the language elements in the list
            var languageElements = driver.FindElements(By.XPath("//th[contains(text(),'Language')]/ancestor::table//tbody/tr/td[1]"));
            
            // Extract the text (language name) from each element and add it to the list
            foreach (var element in languageElements)
            {
                languages.Add(element.Text);
            }

            return languages;
        }
        public void ClearAllLanguages()
        {
            // Retrieve the list of all languages currently present
            var allLanguages = GetAllLanguages();

            // Loop through the list and delete each language
            foreach (var language in allLanguages)
            {
                ClearLanguageTestData(language);
            }
        }
        
        public void ClearLanguageTestData(string languageName)
        {
            try
            {
               
                DeleteButtons(languageName).Click();
              
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"No delete button found for the language: {languageName}.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timeout while waiting for delete button for language: {languageName}.");
            }
            catch (WebDriverException ex)
            {
                Console.WriteLine($"Error interacting with delete button: {ex.Message}");
            }
        }

        // Method to add languages to the tracking list
        public void AddLanguageTestData(string languageName)
        {
            if (!addedLanguages.Contains(languageName))
            {
                if (addedLanguages.Count >= MaxLanguages)
                {
                    // Remove the oldest language or handle cleanup
                    string oldestLanguage = addedLanguages[1];
                    ClearLanguageTestData(oldestLanguage);
                    addedLanguages.RemoveAt(1);
                }
            }
            Console.WriteLine($"Adding language to track: {languageName}");
            addedLanguages.Add(languageName);
        }
        public void ClearAddedLanguages(string languageName)
        {
            Console.WriteLine("Languages to clear: " + string.Join(", ",addedLanguages));
            foreach (var langName in addedLanguages)
            {
                Console.WriteLine($"Clearing language: {langName}");
                ClearLanguageTestData(langName);
            }
            addedLanguages.Clear();
        }
        //Method to get the llist of added languages
        public List<string> GetAddedLanguages()
            { return  new List<string>(addedLanguages); 
        }

    }
}
