using OpenQA.Selenium;
using TesteFrontAilos.ConfProjeto;

namespace TesteFrontAilos.PageObjects
{
    public class TelaDeLogin : DriverConf
    {
        IWebElement Edit_Usuario => driver.FindElement(By.Name("user-name"));
        IWebElement Edit_Senha   => driver.FindElement(By.Name("password"));
        IWebElement Button_Login => driver.FindElement(By.Name("login-button"));
        IWebElement Static_Erro  => driver.FindElement(By.ClassName("error-message-container"));

        public void EfetuaLogin() => Button_Login.Click();

        public void InformaUsuario(string Usuario)
        {
            Edit_Usuario.Clear();
            Edit_Usuario.SendKeys(Usuario);
        } 
        public void InformaSenha(string Senha) 
        {
            Edit_Senha.Clear();
            Edit_Senha.SendKeys(Senha);
        } 
        
        public string ValidaLogin()
        {
            if(Static_Erro.Text != "")
                return Static_Erro.Text;
            
            return string.Empty;
        }
    }
}
