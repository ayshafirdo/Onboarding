using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SpecFlowProjectMars1.Pages;
using SpecFlowProjectMars1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SeleniumExtras.WaitHelpers;

namespace SpecFlowProjectMars1.StepDefinitions
{
    [Binding]
    public class LanguageSkills:CommonDriver
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        Language languageobj = new Language();
        Skill skillobj=new Skill();

        [Given(@"the user logs into ProjectMars")]
        public void GivenTheUserLogsIntoProjectMars()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://localhost:5000/Home");

            LoginActions loginPageobj = new LoginActions();
            loginPageobj.Login(driver,"test123456@test.com", "test123456");
        }
        [Scope(Tag = "language")]
        [When(@"the user adds the language ""([^""]*)""  with level ""([^""]*)""")]
        public void WhenTheUserAddsTheLanguageWithLevel(string languages, string level)
        {
            languageobj.AddNewLanguage(driver,languages,level);  
        }
        [Then(@"the ""([^""]*)""should be added to the list")]
        public void ThenTheShouldBeAddedToTheList(string language)
        {
            if (!string.IsNullOrWhiteSpace(language))
            {
                bool isAdded = languageobj.VerifyLanguageAdded(driver, language);
                Assert.IsTrue(isAdded, $"The language '{language}'  was not added to the list.");
            }
        }
        [Scope(Tag = "language")]
        [When(@"I add a language ""([^""]*)"" with level ""([^""]*)""")]
         public void WhenIAddALanguageWithLevel(string oldLang, string oldLevel)
         {
             languageobj.AddNewLanguage(driver,oldLang,oldLevel);
         }

        [When(@"I update ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIUpdateTo(string oldLang, string newLang)
        {
            languageobj.EditLanguage(driver,oldLang,newLang);
        }

        [Then(@"""([^""]*)"" is added to list")]
        public void ThenIsAddedToList(string newLang)
        {
            languageobj.VerifyLanguageUpdated(driver,newLang);
        }

        [When(@"I delete the language ""([^""]*)""")]
        public void WhenIDeleteTheLanguage(string language1)
        {
            languageobj.DeleteLanguage(driver,language1);
        }

        [Then(@"the language ""([^""]*)"" is removed")]
        public void ThenTheLanguageIsRemoved(string language1)
        {
            languageobj.VerifyLanguageDeleted(driver,language1);
        }
        [Scope(Tag = "skill")]
        [When(@"the user adds a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenTheUserAddsASkillWithLevel(string skills, string levels)
        {
            skillobj.AddNewSkill(driver,skills,levels);
        }
        
        [Then(@"the ""([^""]*)"" should be added to the list")]
        public void ThenTheSkillBeAddedToTheList(string skills)
        {
            skillobj.VerifySkillAdded(driver,skills);
        }
        [Scope(Tag = "skill")]
        [When(@"add a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenAddASkillWithLevel(string oldSkills, string oldLevels)
        {
            skillobj.AddNewSkill(driver, oldSkills, oldLevels);
        }

        
        [When(@"I update the ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIUpdateTheTo(string oldSkills, string newSkills)
        {
            skillobj.EditSkill(driver,oldSkills,newSkills);
        }

        
        [Then(@"""([^""]*)""  is updated to ""([^""]*)""")]
        public void ThenIsUpdted(string newSkills,string oldSkills)
        {
            skillobj.VerifySkillUpdated(driver,oldSkills,newSkills);
        }
        [Scope(Tag = "skill")]
        [When(@"I add a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAddSkillWithLevel(string skillName1, string levelName1)
        {
            skillobj.AddNewSkill(driver,skillName1,levelName1);
        }

        [When(@"I delete the ""([^""]*)""")]
        public void WhenIDeleteThe(string skillName1)
        {
            skillobj.DeleteSkill(driver,skillName1);
        }

        [Then(@"the skill ""([^""]*)"" is removed")]
        public void ThenTheSkillIsRemoved(string skillName1)
        {
            skillobj.VerifySkillDeleted(driver,skillName1);
        }


        [AfterScenario]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
    

