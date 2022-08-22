using OpenQA.Selenium;
using TesteFrontAilos.ConfProjeto;

namespace TesteFrontAilos.PageObjects
{
    public class CheckoutComplete : DriverConf
    {

        IWebElement Button_BackHome = driver.FindElement(By.Id("back-to-products"));

        public void BackHome() => Button_BackHome.Click();  
    }
}
