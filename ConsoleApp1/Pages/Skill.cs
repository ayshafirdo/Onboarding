using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Pages
{
    public class Skill
    {
        public void AddNewSkill(IWebDriver driver, string skillName, string skillLevel)
        {
            // Wait for the elements to be visible and interactable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Navigate to skill
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();
            //Click on Add New Skill
            IWebElement addNewSkillButton = wait.Until(d => d.FindElement(By.XPath("//body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]/thead[1]/tr[1]/th[3]/div[1]")));
            addNewSkillButton.Click();
            //Enter skill name as Python
            IWebElement skillNameInput = wait.Until(d => d.FindElement(By.XPath("//input[contains(@placeholder,\"Add Skill\")]")));
            skillNameInput.Click();
            skillNameInput.SendKeys(skillName);
            //Choose skill level from dropdown
            IWebElement skillLevelDropdown = wait.Until(d => d.FindElement(By.XPath("//div[@id='account-profile-section']//select[@class='ui fluid dropdown']")));
            SelectElement selectSkillLevel = new SelectElement(skillLevelDropdown);
            selectSkillLevel.SelectByText(skillLevel);
            Thread.Sleep(5000);
            //Click on Add Skill Button
            IWebElement addSkillButton = wait.Until(d => d.FindElement(By.XPath("//body/div[@id='account-profile-section']/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/div[1]/span[1]/input[1]")));
            addSkillButton.Click();
            System.Threading.Thread.Sleep(5000);
        }
        public bool VerifySkillAdded(IWebDriver driver, string languageName)
        {
            try
            {
                // Initialize WebDriverWait
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait until the skill is displayed in the table
                IWebElement languageElement = wait.Until(d => d.FindElement(By.XPath($"//td[contains(text(),'java')]")));

                // If the skill element is found, return true
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
        public void EditSkill(IWebDriver driver, string newSkillName)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //Navigate to skill
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();
            Thread.Sleep(5000);
            // Locate the row containing the specific skill name
            IWebElement skillRow = wait.Until(d => d.FindElement(By.XPath($"//td[text()='java']/parent::tr")));
            Thread.Sleep(5000);
            // Find the "Edit" button within that row and click it
            IWebElement editButton = skillRow.FindElement(By.XPath("//tbody/tr[1]/td[3]/span[1]"));
            editButton.Click();

            // Wait for the skill name to become editable
            IWebElement skillInput = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value,'java')]")));
            skillInput.Click();
            skillInput.Clear();
            Thread.Sleep(3000);
            skillInput.SendKeys(newSkillName);

            // Optionally, click the save button if there is one

            IWebElement saveButton = skillRow.FindElement(By.XPath("//input[contains(@value,'Update')]"));
            saveButton.Click();

            // Optionally, wait to observe the action
            Thread.Sleep(2000);

        }
        public bool VerifySkillUpdated(IWebDriver driver, string oldSkillName, string newSkillName)
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


        public void DeleteSkill(IWebDriver driver, string skillName)
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
                IWebElement skillRow = wait.Until(d => d.FindElement(By.XPath($"//td[text()='Software Testing']/parent::tr")));

                // Find the delete button within the located row
                IWebElement delSkillButton = skillRow.FindElement(By.XPath("//td[3]/span[2]/i[1]"));

                // Click the delete button
                delSkillButton.Click();

                // Optionally, wait for a confirmation message or the skill row to be removed
                wait.Until(d => !d.FindElements(By.XPath($"//td[text()='Software Testing']/parent::tr")).Any());
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Skill 'Software Testing' not found within the specified timeout.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Skill 'Software Testing' not found.");
            }
        }
        public bool VerifySkillDeleted(IWebDriver driver, string skillName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            // Navigate to skills
            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();
            Thread.Sleep(5000);

            try
            {
                // Check if the skill name is not present
                return !driver.FindElements(By.XPath($"//td[text()='{skillName}']")).Any();
            }
            catch (NoSuchElementException)
            {
                // If the skill name is not found, it means the skill is deleted
                return true;
            }
        }



    }
}
