using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TesteFrontAilos.ConfProjeto
{
    public class DriverConf
    {
        public static IWebDriver driver { get; set; } = null;

        public static void CriarInstancia() {

            if (driver == null) 
            {
                var option = new ChromeOptions()
                {
                    BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
                };

                driver = new ChromeDriver(option);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.saucedemo.com");
            }
        }

        public static void FecharInstancia() 
        {
            driver.Quit();
            driver.Dispose();
            driver = null;
        }
    }
}
