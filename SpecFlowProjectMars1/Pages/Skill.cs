using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V125.DOMDebugger;
using OpenQA.Selenium.Support.UI;
using SpecFlowProjectMars1.Hooks1;
using SpecFlowProjectMars1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectMars1.Pages
{
    public class Skill : CommonDriver
    {

        private readonly By skillButtonLocator = By.XPath("//a[contains(text(),'Skills')]");
        private readonly By addNewSkillButtonLocator = By.XPath("//body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]/thead[1]/tr[1]/th[3]/div[1]");
        private readonly By skillNameInputLocator = By.XPath("//input[contains(@placeholder,'Add Skill')]");
        private readonly By skillLevelDropdownLocator = By.XPath("//div[@id='account-profile-section']//select[@class='ui fluid dropdown']");
        private readonly By addSkillButtonLocator = By.XPath("//body/div[@id='account-profile-section']/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/div[1]/span[1]/input[1]");
        public void AddNewSkill(string skillName, string skillLevel)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Navigate to skill
            IWebElement skillButton = wait.Until(d => d.FindElement(skillButtonLocator));
            skillButton.Click();
            //Click on Add New Skill
            Thread.Sleep(1000);
            IWebElement addNewSkillButton = wait.Until(d => d.FindElement(addNewSkillButtonLocator));
            addNewSkillButton.Click();
            //Enter skill name
            IWebElement skillNameInput = wait.Until(d => d.FindElement(skillNameInputLocator));
            skillNameInput.Click();
            skillNameInput.SendKeys(skillName);
            //Choose skill level from dropdown
            IWebElement skillLevelDropdown = wait.Until(d => d.FindElement(skillLevelDropdownLocator));
            SelectElement selectSkillLevel = new SelectElement(skillLevelDropdown);
            selectSkillLevel.SelectByText(skillLevel);
            Thread.Sleep(5000);
            //Click on Add Skill Button
            IWebElement addSkillButton = wait.Until(d => d.FindElement(addSkillButtonLocator));
            addSkillButton.Click();
            System.Threading.Thread.Sleep(5000);
            // Verify the toast message for empty skill name
            if (string.IsNullOrWhiteSpace(skillName))
            {
                try
                {
                    IWebElement validationMessage = wait.Until(d => d.FindElement(By.XPath("//div[@class='ns-box-inner' and contains(text(), 'Please enter skill and experience level')]")));
                    Assert.AreEqual("Please enter skill and experience level", validationMessage.Text);
                    IWebElement cancelButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
                    cancelButton.Click();
                }
                catch (WebDriverTimeoutException)
                {
                    throw new Exception("Expected validation message 'Please enter skill and experience level' was not displayed.");
                }
            }
            else
            {
                // Wait for the skill to be added to the table
                wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'{skillName}')]")));
            }

            System.Threading.Thread.Sleep(8000);
        }
        public bool VerifySkillAdded(string skillName)
        {
            if (string.IsNullOrWhiteSpace(skillName))
            {
                // If the skill name is empty or whitespace, return false immediately
                Console.WriteLine("Skill name is empty or whitespace.");
                return false;
            }

            try
            {
                // Initialize WebDriverWait
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait until the skill is displayed in the table
                IWebElement skillElement = wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'{skillName}')]")));

                // If the skill element is found, return true
                return skillElement != null;
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
        public void EditSkill(string oldSkills, string newSkills)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //Navigate to skill
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();
            Thread.Sleep(5000);
            // Locate the row containing the specific skill name
            IWebElement skillRow = wait.Until(d => d.FindElement(By.XPath($"//td[text()='{oldSkills}']/parent::tr")));
            Thread.Sleep(5000);
            // Find the "Edit" button within that row and click it
            IWebElement editButton = skillRow.FindElement(By.XPath("./td[3]/span[1]"));
            editButton.Click();

            // Wait for the skill name to become editable
            IWebElement skillInput = wait.Until(d => d.FindElement(By.XPath($"//input[contains(@value,'{oldSkills}')]")));
            skillInput.Click();
            skillInput.Clear();
            Thread.Sleep(3000);
            skillInput.SendKeys(newSkills);

            IWebElement saveButton = skillRow.FindElement(By.XPath("//input[contains(@value,'Update')]"));
            saveButton.Click();

            Thread.Sleep(2000);

        }
        public bool VerifySkillUpdated(string oldSkillName, string newSkillName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            // Navigate to skills
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();
            Thread.Sleep(5000);

            try
            {
                // Check if the old skill name is not present
                bool oldSkillPresent = driver.FindElements(By.XPath($"//td[text()='{oldSkillName}']")).Count > 0;
                if (oldSkillPresent) return false;

                // Check if the new skill name is present
                IWebElement updatedSkill = wait.Until(d => d.FindElement(By.XPath($"//td[text()='{newSkillName}']")));
                return updatedSkill != null;
            }
            catch (NoSuchElementException)
            {
                // If the new skill name is not found, return false
                return false;
            }
        }


        public void DeleteSkill(string skillName1)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            try
            {
                //Navigate to skill
                IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
                skillButton.Click();
                Thread.Sleep(5000);

                // Locate the row containing the specific skill name
                IWebElement skillRow = wait.Until(d => d.FindElement(By.XPath($"//td[text()='{skillName1}']/parent::tr")));

                // Find the delete button within the located row
                IWebElement delSkillButton = skillRow.FindElement(By.XPath("./td[3]/span[2]/i[1]"));

                // Click the delete button
                delSkillButton.Click();

                // Optionally, wait for a confirmation message or the skill row to be removed
                wait.Until(d => !d.FindElements(By.XPath($"//td[text()='{skillName1}']/parent::tr")).Any());
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Skill '{skillName1}' not found within the specified timeout.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Skill '{skillName1}' not found.");
            }
        }
        public bool VerifySkillDeleted(string skillName1)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            // Navigate to skills
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();
            Thread.Sleep(5000);

            try
            {
                // Check if the skill name is not present
                return !driver.FindElements(By.XPath($"//td[text()='{skillName1}']")).Any();
            }
            catch (NoSuchElementException)
            {
                // If the skill name is not found, it means the skill is deleted
                return true;

            }
        }

        public void AddNewSkillWithoutLevel(string skillName)
        {

            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Navigate to skill
            IWebElement skillButton = wait.Until(d => d.FindElement(skillButtonLocator));
            skillButton.Click();
            //Click on Add New Skill
            Thread.Sleep(1000);
            IWebElement addNewSkillButton = wait.Until(d => d.FindElement(addNewSkillButtonLocator));
            addNewSkillButton.Click();
            //Enter skill name
            IWebElement skillNameInput = wait.Until(d => d.FindElement(skillNameInputLocator));
            skillNameInput.Click();
            skillNameInput.SendKeys(skillName);

            driver.FindElement(addSkillButtonLocator).Click();

        }
        public void AddExistingSkillAndLevel(string skillName, string skillLevel)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //Click on Add New Skill
            IWebElement addNewSkillButton = wait.Until(d => d.FindElement(addNewSkillButtonLocator));
            addNewSkillButton.Click();
            //Enter skill name
            IWebElement skillNameInput = wait.Until(d => d.FindElement(skillNameInputLocator));
            skillNameInput.Click();
            skillNameInput.SendKeys(skillName);
            //Choose skill level from dropdown
            IWebElement skillLevelDropdown = wait.Until(d => d.FindElement(skillLevelDropdownLocator));
            SelectElement selectSkillLevel = new SelectElement(skillLevelDropdown);
            selectSkillLevel.SelectByText(skillLevel);
            Thread.Sleep(1000);
            //Click on Add Skill Button
            IWebElement addSkillButton = wait.Until(d => d.FindElement(addSkillButtonLocator));
            addSkillButton.Click();
        }
        public bool IsSystemHandlingGracefullySkill(string skillName)
        {
            try
            {
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));


                
                IWebElement skillElement = wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'BBBB')]")));

                return skillElement != null;
            }
            catch (NoSuchElementException)
            {
                // If the element is not found, it indicates that the system might have encountered an issue
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                // If the wait times out, it may indicate a problem handling the payload
                return false;
            }
        }

    }
}
