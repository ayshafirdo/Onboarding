using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace SpecFlowProjectMars1.Pages
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

            // Verify the toast message for empty language name
            if (string.IsNullOrWhiteSpace(languageName))
            {
                try
                {
                    IWebElement validationMessage = wait.Until(d => d.FindElement(By.XPath("//div[@class='ns-box-inner' and contains(text(), 'Please enter language and level')]")));
                    Assert.AreEqual("Please enter language and level", validationMessage.Text);
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

        System.Threading.Thread.Sleep(8000); 
        }

        public bool VerifyLanguageAdded(IWebDriver driver, string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
            {
                // If the language name is empty or whitespace, return false immediately
                Console.WriteLine("Language name is empty or whitespace.");
                return false;
            }

            try
            {
                // Initialize WebDriverWait
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

        public void EditLanguage(IWebDriver driver,string oldLang, string newLang)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            //Click on edit button
            IWebElement editButtonLang = wait.Until(d => d.FindElement(By.XPath("//tbody/tr[1]/td[3]/span[1]/i[1]")));
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

        public bool VerifyLanguageUpdated(IWebDriver driver, string languageName)
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

        public void DeleteLanguage(IWebDriver driver,string language1)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Delete language
            IWebElement delLangButton = wait.Until(d => d.FindElement(By.XPath("//tbody/tr[1]/td[3]/span[2]/i[1]")));
            delLangButton.Click();
        }

        public bool VerifyLanguageDeleted(IWebDriver driver, string language1)
        {
            
            
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                try
                {
                // Ensure the page is fully loaded
                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                // Wait a bit to ensure any deletion action is completed
                Thread.Sleep(2000); 
                                    // Try to locate the language element in the table
                wait.Until(d => d.FindElement(By.XPath($"//td[text()='{language1}']")));
                    Thread.Sleep(1000);
                    // If the language element is found, return false
                    Console.WriteLine($"Language '{language1}' was found in the list.");
                    return false;
                }
                catch (WebDriverTimeoutException)
                {
                    // If the language element is not found within the timeout, return true
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

    }
}
