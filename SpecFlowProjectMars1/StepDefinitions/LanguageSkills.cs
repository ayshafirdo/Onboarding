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
using SpecFlowProjectMars1.Hooks1;

namespace SpecFlowProjectMars1.StepDefinitions
{
    [Binding]
    public class LanguageSkills:CommonDriver
    {
        
        Language languageobj = new Language();
        Skill skillobj=new Skill();
        

        [Given(@"the user is on the Languages page")]
        public void GivenTheUserIsOnTheLanguagesPage()
        {
            // Navigate to Languages page
            IWebElement languageButton = driver.FindElement(By.XPath("//a[contains(text(),'Languages')]"));
            languageButton.Click();
        }
        [Given(@"the user is on the Skills page")]
        public void GivenTheUserIsOnTheSkillsPage()
        {
            // Navigate to Skills page
            IWebElement skillButton = driver.FindElement(By.XPath("//a[contains(text(),'Skills')]"));
            skillButton.Click();
        }


        [Scope(Tag = "language")]
        [When(@"the user adds the language ""([^""]*)""  with level ""([^""]*)""")]
        public void WhenTheUserAddsTheLanguageWithLevel(string languages, string level)
        {
            languageobj.AddNewLanguage(languages,level);
            Hooks.AddLanguageTestData(languages);
            
        }
        [Then(@"the ""([^""]*)""should be added to the list")]
        public void ThenTheShouldBeAddedToTheList(string language)
        {
            if (!string.IsNullOrWhiteSpace(language))
            {
                bool isAdded = languageobj.VerifyLanguageAdded(language);
                Assert.IsTrue(isAdded, $"The language '{language}'  was not added to the list.");
            }
        }
        [Scope(Tag = "language")]
        [When(@"I add a language ""([^""]*)"" with level ""([^""]*)""")]
         public void WhenIAddALanguageWithLevel(string oldLang, string oldLevel)
         {
             languageobj.AddNewLanguage(oldLang,oldLevel);
         }

        [When(@"I update ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIUpdateTo(string oldLang, string newLang)
        {
            languageobj.EditLanguage(oldLang,newLang);
            Hooks.AddLanguageTestData(newLang);
        }

        [Then(@"""([^""]*)"" is added to list")]
        public void ThenIsAddedToList(string newLang)
        {
            languageobj.VerifyLanguageUpdated(newLang);
        }

        [When(@"I delete the language ""([^""]*)""")]
        public void WhenIDeleteTheLanguage(string language1)
        {
            languageobj.DeleteLanguage(language1);
        }

        [Then(@"the language ""([^""]*)"" is removed")]
        public void ThenTheLanguageIsRemoved(string language1)
        {
            languageobj.VerifyLanguageDeleted(language1);
        }
        [When(@"I attempt to add a language ""([^""]*)"" without selecting a level")]
        public void WhenIAttemptToAddALanguageWithoutSelectingALevel(string Arabic)
        {
            languageobj.AddNewLanguageWithoutLevel(Arabic);
            

        }

        [Given(@"the language ""([^""]*)"" with level ""([^""]*)"" is already present")]
        public void GivenTheLanguageWithLevelIsAlreadyPresent(string languageExisting, string levelExisting)
        {
            languageobj.AddNewLanguage(languageExisting,levelExisting);
            Hooks.AddLanguageTestData(languageExisting);
        }


        [Then(@"I should see an error message ""([^""]*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedErrorMessage)
        {
            bool isValidationMessageDisplayed = languageobj.VerifyErrorMessageDisplayed(expectedErrorMessage);
            Assert.IsTrue(isValidationMessageDisplayed, "Expected validation message was not displayed.");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cancelButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
            cancelButton.Click();
        }
    
        [When(@"I attempt to add the language ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAttemptToAddTheLanguageWithLevel(string languageAttempt, string levelAttempt)
        {
            languageobj.AddExistingLanguageAndLevel(languageAttempt,levelAttempt);
        }

        [Then(@"I should see an error message saying ""([^""]*)""")]
        public void ThenIShouldSeeAnErrorMessageSaying(string expectedErrorMessage)
        {
            languageobj.VerifyErrorMessageDisplayed(expectedErrorMessage);
        }   

        [When(@"I attempt to add a language with a large payload")]
        public void WhenIAttemptToAddALanguageWithALargePayload()
        {
            string largePayload = new string('A', 1000); 

            languageobj.AddNewLanguage(largePayload, "Fluent");
            Hooks.AddLanguageTestData(largePayload);
        }
        [Then(@"I should see an error message ""([^""]*)"" or the system should gracefully handle the input without crashing")]
        public void ThenIShouldSeeAnErrorMessageOrHandleGracefully(string expectedErrorMessage,string languageName)
        {
            try
            {
                bool isValidationMessageDisplayed = languageobj.VerifyErrorMessageDisplayed(expectedErrorMessage);
                if (isValidationMessageDisplayed)
                {
                    Assert.IsTrue(isValidationMessageDisplayed, "Expected validation message was not displayed.");
                }
                else
                {
                    // Alternatively, verify that the system did not crash
                    bool isSystemHandlingGracefully = languageobj.IsSystemHandlingGracefully(languageName);
                    Assert.IsTrue(isSystemHandlingGracefully, "The system crashed or did not handle the input gracefully.");
                }
            }
            catch (WebDriverException ex)
            {
                Assert.Fail($"WebDriver exception occurred: {ex.Message}");
            }
        }
         [Then(@"the system should gracefully handle the large payload without errors")]
         public void ThenTheSystemShouldGracefullyHandleTheLargePayloadWithoutErrors()
         {
             // Define a large payload language name
             string largeLanguageName = new string('a', 10000);


             // Verify that the language with the large name was added successfully
             bool isLanguageAdded = languageobj.IsSystemHandlingGracefully(largeLanguageName);

             // Assert that the language was added or handled gracefully
             Assert.IsTrue(isLanguageAdded, "The system did not handle the large payload gracefully.");


         }

        [Given(@"I have a language named ""([^""]*)"" with level ""([^""]*)"" in the system")]
        public void GivenIHaveALanguageNamedWithLevelInTheSystem(string tamil,string basic)
        {
            languageobj.AddNewLanguage(tamil, basic);
        }
        [When(@"I attempt to update the language name ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheLanguageNameTo(string tamil, string spanish)
        {
            languageobj.EditLanguage(tamil, spanish);
            Hooks.AddLanguageTestData("spanish");
        }

        [When(@"I attempt to update the language name ""([^""]*)""again to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheLanguageNameAgainTo(string spanish, string spanish1)
        {
            languageobj.EditLanguage(spanish, spanish);
        }


        [Then(@"the system should display an error message ""([^""]*)""")]
        public void ThenTheSystemShouldDisplayAnErrorMessage(string expectedErrorMessage)
        {
           languageobj.VerifyErrorMessageDisplayed(expectedErrorMessage);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cancelButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
            cancelButton.Click();
        }




        [Then(@"the system should gracefully handle the large skill payload without errors")]
        public void ThenTheSystemShouldGracefullyHandleTheLargeSkillPayloadWithoutErrors()
        {
            

            
            // Define a large payload skill name 
            string largeSkillName = new string('b', 10000);

            // Verify that the skill with the large name was added successfully
            bool isSkillHandledGracefully = skillobj.IsSystemHandlingGracefullySkill(largeSkillName);

            Assert.IsTrue(isSkillHandledGracefully, "The system did not handle the large skill payload gracefully.");
        }




        [Scope(Tag = "skill")]
        [When(@"the user adds a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenTheUserAddsASkillWithLevel(string skills, string levels)
        {
            skillobj.AddNewSkill(skills,levels);
            Hooks.AddSkillTestData(skills);
        }
        
        [Then(@"the ""([^""]*)"" should be added to the list")]
        public void ThenTheSkillBeAddedToTheList(string skills)
        {
            skillobj.VerifySkillAdded(skills);
        }
        [Scope(Tag = "skill")]
        [When(@"add a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenAddASkillWithLevel(string oldSkills, string oldLevels)
        {
            skillobj.AddNewSkill(oldSkills, oldLevels);
        }

        
        [When(@"I update the ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIUpdateTheTo(string oldSkills, string newSkills)
        {
            skillobj.EditSkill(oldSkills,newSkills);
            Hooks.AddSkillTestData(newSkills);
        }

        
        [Then(@"""([^""]*)""  is updated to ""([^""]*)""")]
        public void ThenIsUpdted(string newSkills,string oldSkills)
        {
            skillobj.VerifySkillUpdated(oldSkills,newSkills);
        }
        [Scope(Tag = "skill")]
        [When(@"I add a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAddSkillWithLevel(string skillName1, string levelName1)
        {
            skillobj.AddNewSkill(skillName1,levelName1);
        }

        [When(@"I delete the ""([^""]*)""")]
        public void WhenIDeleteThe(string skillName1)
        {
            skillobj.DeleteSkill(skillName1);
        }

        [Then(@"the skill ""([^""]*)"" is removed")]
        public void ThenTheSkillIsRemoved(string skillName1)
        {
            skillobj.VerifySkillDeleted(skillName1);
        }

        [When(@"I attempt to add a skill ""([^""]*)"" without selecting a skill level")]
        public void WhenIAttemptToAddASkillWithoutSelectingASkillLevel(string python)
        {
           skillobj.AddNewSkillWithoutLevel(python);
        }
        [Given(@"the skill ""([^""]*)"" with level ""([^""]*)"" is already present")]
        public void GivenTheSkillWithLevelIsAlreadyPresent(string skillExisting, string skillLevelExisting)
        {
            skillobj.AddNewSkill(skillExisting,skillLevelExisting);
            Hooks.AddSkillTestData(skillLevelExisting);
        }

        [When(@"I attempt to add the skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAttemptToAddTheSkillWithLevel(string skillAttempt, string skillLevelAttempt)
        {
            skillobj.AddExistingSkillAndLevel(skillAttempt,skillLevelAttempt);
        }
        [When(@"I attempt to add a skill with a large payload")]
        public void WhenIAttemptToAddASkillWithALargePayload()
        {
            string largePayload = new string('B', 1000);

            skillobj.AddNewSkill(largePayload, "Expert");
            Hooks.AddSkillTestData(largePayload);
        }
        [Given(@"I have a skill named ""([^""]*)"" with level ""([^""]*)"" in the system")]
        public void GivenIHaveASkillNamedWithLevelInTheSystem(string Logo, string Beginner)
        {
            skillobj.AddNewSkill(Logo,Beginner);
        }

        [When(@"I attempt to update the skill name ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheSkillNameTo(string Logo, string Web)
        {
            skillobj.EditSkill(Logo,Web);
            Hooks.AddSkillTestData(Web);
        }

        [When(@"I attempt to update the skill name ""([^""]*)""again to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheSkillNameAgainTo(string Web,string Webb)
        {
            skillobj.EditSkill(Web,Web);
        }





       
    }
}
    

