using OpenQA.Selenium;
using System.Globalization;
using TesteFrontAilos.ConfProjeto;

namespace TesteFrontAilos.PageObjects
{
    public class CheckoutOverview : DriverConf
    {
        private
            int QuantItens;
            string Descricao;
            float Valor;
            float ItemTotal;
            float Tax;
            float Total;

        public int quantItens   { get => QuantItens;    set => QuantItens = value; }
        public string descricao { get => Descricao;     set => Descricao  = value; }
        public float valor      { get => Valor;         set => Valor      = value; }
        public float itemTotal  { get => float.Parse(Static_ItemTotal.Text.Split("$")[1], CultureInfo.InvariantCulture.NumberFormat); set => ItemTotal  = value; }
        public float tax        { get => float.Parse(Static_Tax.Text.Split("$")[1]      , CultureInfo.InvariantCulture.NumberFormat); set => Tax        = value; }
        public float total      { get => float.Parse(Static_Total.Text.Split("$")[1]    , CultureInfo.InvariantCulture.NumberFormat); set => Total      = value; }

        IList<IWebElement> ListaDeProdutosNoCheckout = driver.FindElements(By.ClassName("cart_item"));
        
        IWebElement Button_Finish    = driver.FindElement(By.Id("finish"));
        IWebElement Static_ItemTotal = driver.FindElement(By.ClassName("summary_subtotal_label"));
        IWebElement Static_Tax       = driver.FindElement(By.ClassName("summary_tax_label"));
        IWebElement Static_Total     = driver.FindElement(By.ClassName("summary_total_label"));

        public void ValidaItensNoCheckout(List<Produtos> ListaDeProdutos)
        {
            string[] ProdutosNaoDesejados = { };

            foreach (var item in ListaDeProdutosNoCheckout)
            {
                string Titulo = item.FindElement(By.ClassName("inventory_item_name")).Text;
                bool encontrou = false;

                foreach (var Produto in ListaDeProdutos)
                {
                    if (Produto.titulo == Titulo)
                    {
                        encontrou = true;
                        break;
                    }
                }

                if (!encontrou)
                    ProdutosNaoDesejados[0] = Titulo;
            }

            if (ListaDeProdutos.Count > 0 || ProdutosNaoDesejados.Length > 0)
            {
                Assert.AreNotEqual(0, ListaDeProdutos.Count, $"Os seguintes produtos não estava na lista de desejados.{ProdutosNaoDesejados}");
            }
        }

        public void ValidaItemTotal(List<Produtos> ListaDeProdutos) 
        {
            float ValorStaticItemTotal = 0;

            foreach (var item in ListaDeProdutos)
            {
                ValorStaticItemTotal += item.valor;
            }
            
            if (this.itemTotal != ValorStaticItemTotal) 
            {
                Console.WriteLine($"O Valor dos produtos não confere com o valor item total. Valor Item Total: {this.itemTotal}, Valor dos produtos: {ValorStaticItemTotal}.");
                Assert.AreEqual(this.itemTotal, ValorStaticItemTotal);
            }
        }

        public void ValidaTotal()
        {
            if (this.tax + this.itemTotal != this.total)
            {
                Console.WriteLine($"O Valor do Static (Item Total + Tax) não confere com o Static Total. Valor Item Total + Tax: {this.tax + this.itemTotal} e o valor Total: {this.total}");
                Assert.AreEqual(this.total, (this.tax + this.itemTotal));
            }
        }

        public void Finish() => Button_Finish.Click();
    }
}
