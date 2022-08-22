using OpenQA.Selenium;
using System.Globalization;
using TesteFrontAilos.ConfProjeto;

namespace TesteFrontAilos.PageObjects
{
    public class Carrinho : DriverConf
    {
        private
            int QuantItens;
            string Descricao;
            float Valor;

        public int    quantItens { get => QuantItens; set => QuantItens  = value; }
        public string descricao  { get => Descricao;  set => Descricao   = value; }
        public float  valor      { get => Valor;      set => Valor       = value; }

        IList<IWebElement> ListaDeProdutosNoCarrinho = driver.FindElements(By.ClassName("cart_item"));
        IWebElement Button_Checkout = driver.FindElement(By.Id("checkout"));
        
        public void ValidaItensNoCarrinho(List<Produtos> ListaDeProdutos) 
        {
            string[] ProdutosNaoDesejados = {};

            foreach (var item in ListaDeProdutosNoCarrinho)
            {
                string Titulo  = item.FindElement(By.ClassName("inventory_item_name")).Text;
                bool encontrou = false;

                foreach (var Produto in ListaDeProdutos)
                {
                    if (Produto.titulo == Titulo)
                    {
                        encontrou = true;
                        break;
                    }
                }

                if(!encontrou)
                  ProdutosNaoDesejados[0] = Titulo;
            }

            if(ListaDeProdutos.Count > 0 || ProdutosNaoDesejados.Length > 0) 
            {
                Assert.AreNotEqual(0, ListaDeProdutos.Count, $"Os seguintes produtos não estava na lista de desejados.{ProdutosNaoDesejados}");
            }
        }

        public void Checkout() => Button_Checkout.Click();

    }
}
