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
    public class LanguageStepDefinitions:CommonDriver
    {
        
        Language languageobj = new Language();
        
        

        [Given(@"the user is on the Languages page")]
        public void GivenTheUserIsOnTheLanguagesPage()
        {
            // Navigate to Languages page
            languageobj.LanguageSection();
            // Verify the user is on the Languages page
            bool isOnLanguagePage = languageobj.IsOnLanguageSection();
            Assert.IsTrue(isOnLanguagePage, "The user is not on the Languages page.");
        }
       


        [Scope(Tag = "language")]
        [When(@"the user adds the language ""([^""]*)""  with level ""([^""]*)""")]
        public void WhenTheUserAddsTheLanguageWithLevel(string languages, string level)
        {
            languageobj.AddNewLanguage(languages,level);
            languageobj.AddLanguageTestData(languages);
            
        }
        [Then(@"the validation message ""([^""]*)"" should be displayed")]
        public void ThenTheValidationMessageShouldBeDisplayed(string expectedMessage)
        {
            bool isMessageDisplayed = languageobj.IsValidationMessageDisplayed(expectedMessage);
            Assert.IsTrue(isMessageDisplayed, $"Expected validation message '{expectedMessage}' was not displayed.");
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
            languageobj.AddLanguageTestData(newLang);
        }

        [Then(@"""([^""]*)"" is added to list")]
        public void ThenIsAddedToList(string newLang)
        {
            
            bool isLanguageAdded = languageobj.IsLanguageUpdated(newLang);
            Assert.IsTrue(isLanguageAdded, $"Language '{newLang}' was not added to the list.");
        }

        [When(@"I delete the language ""([^""]*)""")]
        public void WhenIDeleteTheLanguage(string language1)
        {
            languageobj.DeleteLanguage(language1);
        }

        [Then(@"the language ""([^""]*)"" is removed")]
        public void ThenTheLanguageIsRemoved(string language1)
        {
            
            bool isDeleted = languageobj.IsLanguageDeleted(language1);
            Assert.IsTrue(isDeleted, $"The language '{language1}' was not removed as expected.");
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
            languageobj.AddLanguageTestData(languageExisting);
        }


        [Then(@"I should see an error message ""([^""]*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedErrorMessage)
        {
            bool isValidationMessageDisplayed = languageobj.VerifyErrorMessageDisplayed(expectedErrorMessage);
            Assert.IsTrue(isValidationMessageDisplayed, "Expected validation message was not displayed.");
            
        }
    
        [When(@"I attempt to add the language ""([^""]*)"" with level ""([^""]*)""")]
        public void WhenIAttemptToAddTheLanguageWithLevel(string languageAttempt, string levelAttempt)
        {
            languageobj.AddExistingLanguageAndLevel(languageAttempt,levelAttempt);
        }

        

        [When(@"I attempt to add a language with a large payload")]
        public void WhenIAttemptToAddALanguageWithALargePayload()
        {
            string largePayload = new string('A', 1000); 

            languageobj.AddNewLanguage(largePayload, "Fluent");
            languageobj.AddLanguageTestData(largePayload);
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
        public void WhenIAttemptToUpdateTheLanguageNameTo(string tamil, string Malay)
        {
            languageobj.EditLanguage(tamil, Malay);
            languageobj.AddLanguageTestData("Malay");
        }

        [When(@"I attempt to update the language name ""([^""]*)""again to ""([^""]*)""")]
        public void WhenIAttemptToUpdateTheLanguageNameAgainTo(string spanish, string spanish1)
        {
            languageobj.EditLanguage(spanish, spanish);
        }


       

       
    }
}
    

