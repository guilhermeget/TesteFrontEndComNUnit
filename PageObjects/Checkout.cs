using OpenQA.Selenium;
using TesteFrontAilos.ConfProjeto;

namespace TesteFrontAilos.PageObjects
{
    public class Checkout : DriverConf
    {
        private
              string Nome;
        string SobreNome;
        string PostalCode;

        public string nome { get => Nome; set => Nome = value; }
        public string sobreNome { get => SobreNome; set => SobreNome = value; }
        public string postalCode { get => PostalCode; set => PostalCode = value; }

        IWebElement Edit_Nome = driver.FindElement(By.Id("first-name"));
        IWebElement Edit_SobreNome = driver.FindElement(By.Id("last-name"));
        IWebElement Edit_PostalCode = driver.FindElement(By.Id("postal-code"));
        IWebElement Button_Continuar = driver.FindElement(By.Id("continue"));


        public void InformaNome(string Nome)
        {
            Edit_Nome.Clear();
            Edit_Nome.SendKeys(Nome);
        }

        public void InformaSobreNome(string SobreNome)
        {
            Edit_SobreNome.Clear();
            Edit_SobreNome.SendKeys(SobreNome);
        }

        public void InformaPostalCode(string PostalCode)
        {
            Edit_PostalCode.Clear();
            Edit_PostalCode.SendKeys(PostalCode);
        }

        public void Continuar() => Button_Continuar.Click();
    }
}
