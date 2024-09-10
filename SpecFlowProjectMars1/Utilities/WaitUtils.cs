using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectMars1.Utilities
{
    public class WaitUtils :CommonDriver
    {
        public IWebElement WaitToBeVisible(By locator,int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

        }
    }
}
