using System;
using System.Security.Principal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowProjectMars1.Pages

{
    public class LoginActions
    {
        


        
        public void Login(IWebDriver driver, String email, String password)


        {


            try
            {
                // Navigate to a URL
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("http://localhost:5000/Home");

                
                Thread.Sleep(5000); 

                //Click on sign in button
                IWebElement loginButton = driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]"));
                loginButton.Click();
                Thread.Sleep(2000);
                IWebElement emailTextBox = driver.FindElement(By.XPath("//input[contains(@placeholder,\"Email address\")]"));
                emailTextBox.Click(); emailTextBox.SendKeys(email);
                IWebElement passwordTextBox = driver.FindElement(By.XPath("//input[contains(@placeholder,\"Password\")]"));
                passwordTextBox.Click(); passwordTextBox.SendKeys(password);
                IWebElement loginButton2 = driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
                loginButton2.Click();
                Thread.Sleep(5000);

                // Wait for the elements to be visible and interactable
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        

        public void VerifyLoggedInUser(IWebDriver driver)
        {
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement hiJohn;

            try
            {
                // Wait until the element is visible
                hiJohn = wait.Until(d => d.FindElement(By.XPath("//body/div[@id='account-profile-section']/div[1]/div[1]/div[2]/div[1]/span[1]")));

                // Print the text to debug
                string actualText = hiJohn.Text;
                Console.WriteLine("Actual text: " + actualText);

                // Check if the text matches
                if (actualText.Trim() == "Hi John")
                {
                    Console.WriteLine("Successfully logged in");
                }
                else
                {
                    Console.WriteLine("Not successfully logged in");
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Element not found within the specified timeout.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found.");
            }
        }


    }
}

