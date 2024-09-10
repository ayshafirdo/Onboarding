﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectMars1.Utilities
{
    public class CommonDriver
    {
       public static IWebDriver driver;
        public CommonDriver()
        {
            Initialise();
        }
        public void Initialise()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
                driver.Navigate().GoToUrl("http://localhost:5000/Home");
                driver.Manage().Window.Maximize();
            }
           

           

        }
      
        public void CloseDriver()
        {
            driver.Quit();
        }

    }



}

