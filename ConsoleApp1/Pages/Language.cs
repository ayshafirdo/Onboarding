using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp1.Pages
{
    public class Language
    {
       
        public void AddNewLanguage(IWebDriver driver, String languageName,string languageLevel)
        {
            Thread.Sleep(1000);
            //Adding Language
            IWebElement addNewButton = driver.FindElement(By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div"));
            addNewButton.Click();
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Locate the language name input field
            IWebElement languageNameInput = wait.Until(d => d.FindElement(By.XPath("//input[contains(@placeholder,\"Add Language\")]")));
            languageNameInput.Clear();
            languageNameInput.SendKeys(languageName); 

            // Locate the language level dropdown
            IWebElement languageLevelDropdown = wait.Until(d => d.FindElement(By.XPath("//div[@id='account-profile-section']//select[@class='ui dropdown']")));
            SelectElement selectLanguageLevel = new SelectElement(languageLevelDropdown);
            Thread.Sleep(1000);
            selectLanguageLevel.SelectByText(languageLevel); 

            // Locate and click the add language button
            IWebElement addLanguageButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Add')]")));
            addLanguageButton.Click();

            wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'{languageName}')]")));

            // wait for a few seconds to see the result
            System.Threading.Thread.Sleep(8000); 
        }

        public bool VerifyLanguageAdded(IWebDriver driver, string languageName)
        {
            try
            {
                // Initialize WebDriverWait
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait until the language is displayed in the table
                IWebElement languageElement = wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'German')]")));

                // If the language element is found, return true
                return languageElement != null;
            }
            catch (NoSuchElementException)
            {
                // If the element is not found, return false
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                // If the wait times out, return false
                return false;
            }
        }

        public void EditLanguage(IWebDriver driver)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            
            //Click on edit button
            IWebElement editButtonLang = wait.Until(d => d.FindElement(By.XPath("//tbody/tr[1]/td[3]/span[1]/i[1]")));
            editButtonLang.Click();

            IWebElement languageNameInput2 = wait.Until(d => d.FindElement(By.XPath("//input[contains(@placeholder,\"Add Language\")]")));
            languageNameInput2.Clear();
            languageNameInput2.SendKeys("Tamil");

            System.Threading.Thread.Sleep(5000);

            //Click on update button
            IWebElement updateLangButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Update')]")));
            updateLangButton.Click();
            System.Threading.Thread.Sleep(5000);
        }

        public bool VerifyLanguageUpdated(IWebDriver driver, string languageName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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

        public void DeleteLanguage(IWebDriver driver)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Delete language
            IWebElement delLangButton = wait.Until(d => d.FindElement(By.XPath("//tbody/tr[1]/td[3]/span[2]/i[1]")));
            delLangButton.Click();
        }

        public bool VerifyLanguageDeleted(IWebDriver driver, string languageName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                // Try to locate the language element in the table
                wait.Until(d => d.FindElement(By.XPath($"//td[text()='{languageName}']")));
                // If the language element is found, return false
                return false;
            }
            catch (NoSuchElementException)
            {
                // If the language element is not found, return true
                return true;
            }
        }

    }
}
