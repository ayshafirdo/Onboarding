using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectMars1.Utilities
{
    public class WaitUtils
    {
        public static void WaitToBeVisible(IWebDriver driver,string tmOptionLocator,string locatorValue,int seconds)
        {
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
           
        }
    }
}
