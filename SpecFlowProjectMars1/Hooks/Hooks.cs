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
       

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Perform login before all tests
            LoginActions loginPageobj = new LoginActions();
            loginPageobj.Login("test123456@test.com", "test123456");

            Language languageobj = new Language();
            Skill skillobj = new Skill();

            languageobj.ClearAllLanguages();
            skillobj.ClearAllSkills();
            
        }
        
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Language languageobj = new Language();
            List<string> languagesToClear = languageobj.GetAddedLanguages();
            foreach (var languageName in languagesToClear)
            {
                languageobj.ClearAddedLanguages(languageName);
            }

            Skill skillobj = new Skill();

            List<string> skillsToClear = skillobj.GetAddedSkills(); 
            foreach (var skillName in skillsToClear)
            {
                skillobj.ClearAddedSkills(skillName);
            }
           
            

            driver.Quit();
        }


    }

}


