using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProjectMars1.Utilities;
using OpenQA.Selenium.Support.UI;
using SpecFlowProjectMars1.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectMars1.StepDefinitions
{
    [Binding]
    public class SkillStepDefinitions:CommonDriver
    {
        Skill skillobj = new Skill();

        [Given(@"the user is on the Skills page")]
        public void GivenTheUserIsOnTheSkillsPage()
        {
            // Navigate to Skills page
            IWebElement skillButton = driver.FindElement(By.XPath("//a[contains(text(),'Skills')]"));
            skillButton.Click();
        }
        [Scope(Tag = "skill")]
        [When(@"the user adds a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenTheUserAddsASkillWithLevel(string skills, string levels)
        {
            skillobj.AddNewSkill(skills, levels);
            skillobj.AddSkillTestData(skills);
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
            skillobj.EditSkill(oldSkills, newSkills);
            skillobj.AddSkillTestData(newSkills);
        }


        [Then(@"""([^""]*)""  is updated to ""([^""]*)""")]
        public void ThenIsUpdted(string newSkills, string oldSkills)
        {
            skillobj.VerifySkillUpdated(oldSkills, newSkills);
        }
        [Scope(Tag = "skill")]
        [When(@"I add a skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAddSkillWithLevel(string skillName1, string levelName1)
        {
            skillobj.AddNewSkill(skillName1, levelName1);
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
            skillobj.AddNewSkill(skillExisting, skillLevelExisting);
            skillobj.AddSkillTestData(skillLevelExisting);
        }

        [When(@"I attempt to add the skill ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAttemptToAddTheSkillWithLevel(string skillAttempt, string skillLevelAttempt)
        {
            skillobj.AddExistingSkillAndLevel(skillAttempt, skillLevelAttempt);
        }
        [When(@"I attempt to add a skill with a large payload")]
        public void WhenIAttemptToAddASkillWithALargePayload()
        {
            string largePayload = new string('B', 1000);

            skillobj.AddNewSkill(largePayload, "Expert");
            skillobj.AddSkillTestData(largePayload);
        }
        [Given(@"I have a skill named ""([^""]*)"" with level ""([^""]*)"" in the system")]
        public void GivenIHaveASkillNamedWithLevelInTheSystem(string Logo, string Beginner)
        {
            skillobj.AddNewSkill(Logo, Beginner);
        }

        [When(@"I attempt to update the skill name ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheSkillNameTo(string Logo, string Web)
        {
            skillobj.EditSkill(Logo, Web);
            skillobj.AddSkillTestData(Web);
        }

        [When(@"I attempt to update the skill name ""([^""]*)""again to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheSkillNameAgainTo(string Web, string Webb)
        {
            skillobj.EditSkill(Web, Web);
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
        [Then(@"I should see an error message ""([^""]*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedErrorMessage)
        {
            bool isValidationMessageDisplayed = skillobj.VerifyErrorMessageDisplayed(expectedErrorMessage);
            Assert.IsTrue(isValidationMessageDisplayed, "Expected validation message was not displayed.");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cancelButton = wait.Until(d => d.FindElement(By.XPath("//input[contains(@value, 'Cancel')]")));
            cancelButton.Click();
        }



    }
}
