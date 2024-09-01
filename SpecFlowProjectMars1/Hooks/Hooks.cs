using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowProjectMars1.Pages;
using SpecFlowProjectMars1.Utilities;
using TechTalk.SpecFlow;

namespace SpecFlowProjectMars1.Hooks1
{


    [Binding]
    public class Hooks : CommonDriver
    {
        // Track languages added during the test run
        private static List<string> addedLanguages = new List<string>();
        //Track skills added during the test
        private static List<string> addedSkills= new List<string>();
        //set the limit to 3 languages
        private const int MaxLanguages = 3;

        
        

        // Method to add languages to the tracking list
        public static void AddLanguageTestData(string languageName)
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
        public static void AddSkillTestData(string skillName)
        {
            Console.WriteLine($"Adding skill to track: {skillName}");
            addedSkills.Add(skillName);
        }


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Perform login before all tests
            LoginActions loginPageobj = new LoginActions();
            loginPageobj.Login("test123456@test.com", "test123456");
            ClearAllLanguages();
            ClearAllSkills();
            
        }
        
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Languages to clear: " + string.Join(", ", addedLanguages));
            foreach (var languageName in addedLanguages)
            {
                Console.WriteLine($"Clearing language: {languageName}");
                ClearLanguageTestData(languageName);
            }
            Console.WriteLine("Skills to clear: " + string.Join(", ", addedSkills));
            foreach (var skillName in addedSkills)
            {
                Console.WriteLine($"Clearing skill: {skillName}");
                ClearSkillTestData(skillName);
            }

            driver.Quit();
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

        public static List<string> GetAllSkills()
        {
            List<string> skills = new List<string>();
            //Navigate to skills section
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement skillButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Skills')]")));
            skillButton.Click();

            // Locate the skill elements in the list
            var skillElements = driver.FindElements(By.XPath("//th[contains(text(),'Skill')]/ancestor::table//tbody/tr/td[1]"));

            // Extract the text (skill name) from each element and add it to the list
            foreach (var element in skillElements)
            {
                skills.Add(element.Text);
            }

            return skills;
        }
        public static void ClearAllLanguages()
        {
            // Retrieve the list of all languages currently present
            var allLanguages = GetAllLanguages();

            // Loop through the list and delete each language
            foreach (var language in allLanguages)
            {
                ClearLanguageTestData(language);
            }
        }
        public static void ClearAllSkills()
        {
            // Retrieve the list of all skills currently present
            var allSkills = GetAllSkills();

            // Loop through the list and delete each skill
            foreach (var skill in allSkills)
            {
                ClearSkillTestData(skill);
            }
        }






        private static void ClearSkillTestData(string skillName)
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
        private static void ClearLanguageTestData(string languageName)
        {
            
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            var countLang = driver.FindElements(By.XPath($"//td[contains(text(),'{languageName}')]/following-sibling::td//span[2]")).Count;
            // Clear all languages
            for (int i = 0; i < countLang; i++)
            {


                try
                {

                    var langDeleteButtons = driver.FindElements(By.XPath($"//td[contains(text(), '{languageName}')]/following-sibling::td//span[2]"));
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


