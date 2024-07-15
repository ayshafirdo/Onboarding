using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Pages;
using ConsoleApp1.Utilities;
using OpenQA.Selenium.Support.UI;


namespace ConsoleApp1.Tests
{

    [TestFixture]
    public class LanguageSkillTests : CommonDriver
    {

        [SetUp]
        public void SetUpProjectMars()
        {

            //Open Chrome Browser
            driver = new ChromeDriver();

            //Login Page Object initialization and definition
            Login loginPageobj = new Login();
            loginPageobj.LoginActions(driver, "test123456@test.com", "test123456");
            //Verification
            loginPageobj.VerifyLoggedInUser(driver);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ClearTest clearTestobj = new ClearTest();
            clearTestobj.ClearTestData(driver);

        }
        [Test, Order(1)]
        public void AddLanguage()
        {
            Language languageobj = new Language();
            languageobj.AddNewLanguage(driver, "Spanish", "Fluent");
            languageobj.AddNewLanguage(driver, "German", "Basic");
            languageobj.AddNewLanguage(driver, "English", "Conversational");
            //Verify language is added to list
            Language verifyLangobj = new Language();
            verifyLangobj.VerifyLanguageAdded(driver, "German");


        }

        [Test, Order(2)]
        public void EditLanguage()
        {
            Language languageobj = new Language();
            languageobj.EditLanguage(driver);
            //Verify language is updated
            Language verifyUpdateLangobj = new Language();
            verifyUpdateLangobj.VerifyLanguageUpdated(driver, "Tamil");
        }

        [Test, Order(3)]
        public void DeleteLanguage()
        {
            Language languageobj = new Language();
            languageobj.DeleteLanguage(driver);
            //Verify language is deleted
            Language verifyDelLangobj = new Language();
            verifyDelLangobj.VerifyLanguageDeleted(driver, "Tamil");
        }

        [Test, Order(4)]
        public void AddSkill()
        {
            Skill skillobj = new Skill();
            skillobj.AddNewSkill(driver, "java", "Expert");
            //Verify skill is added to list
            Skill verifySkillobj = new Skill();
            verifySkillobj.VerifySkillAdded(driver, "java");
        }

        [Test, Order(5)]
        public void EditSkill()
        {
            Skill skillobj = new Skill();
            skillobj.EditSkill(driver, "Software Testing");
            Skill verifySkillUpdatedobj = new Skill();
            verifySkillUpdatedobj.VerifySkillUpdated(driver, "java", "Software Testing");
        }


        [Test, Order(6)]
        public void DeleteSkill()
        {
            Skill skillobj = new Skill();
            skillobj.DeleteSkill(driver, "Software Testing");
            Skill verifySkillDeletedobj = new Skill();
            verifySkillDeletedobj.VerifySkillDeleted(driver, "Software Testing");
        }




        [TearDown]
        public void CloseTestRun()
        {
            ClearTest clearTestobj = new ClearTest();
            clearTestobj.ClearTestData(driver);

            driver.Quit();

        }
    }
}
