using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectMars1.Utilities
{
    public class ClearTest
    {
        public void ClearTestData(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            List<string> languagesToDelete = new List<string> { "Spanish12", "English"};

            foreach (var language in languagesToDelete)
            {
                Console.WriteLine($"Attempting to delete language: {language}");
                while (true)
                {
                    try
                    {
                        var deleteButton = driver.FindElement(By.XPath($"//td[text()='{language}']/following-sibling::td//i[@class='remove icon']"));
                        Console.WriteLine($"Found delete button for: {language}");
                        deleteButton.Click();
                        wait.Until(ExpectedConditions.StalenessOf(deleteButton));
                        Console.WriteLine($"Deleted: {language}");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine($"No more elements found for: {language}");
                        break;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine($"Timeout occurred while deleting: {language}");
                        break;
                    }
                }
            }
        }

        
    }
}
