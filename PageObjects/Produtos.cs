using OpenQA.Selenium;
using System.Globalization;
using TesteFrontAilos.ConfProjeto;

namespace TesteFrontAilos.PageObjects
{
    public class Produtos : DriverConf
    {

        private
            string Titulo;
            string Descricao;
            float  Valor;

        public string titulo    { get => Titulo;    set => Titulo    = value; }
        public string descricao { get => Descricao; set => Descricao = value; }
        public float valor      { get => Valor;     set => Valor     = value; }

        IList<IWebElement> TodosProdutos = driver.FindElements(By.ClassName("inventory_item"));
        IWebElement ToolBar_Carrinho     = driver.FindElement(By.Id("shopping_cart_container"));
        IWebElement ToolBar_Menu         = driver.FindElement(By.Id("react-burger-menu-btn"));
        

        public List<Produtos> AdicionarProdutoNoCarrinho(string[] ProdutoASerComprado)
        {
            List<Produtos> lista = new List<Produtos>();

            foreach (var item in TodosProdutos)
            {
                string Titulo = item.FindElement(By.ClassName("inventory_item_name")).Text;

                if (ProdutoASerComprado.Contains(Titulo))
                {
                    IWebElement Button_ADDTOCART = item.FindElement(By.ClassName("btn"));

                    if (Button_ADDTOCART.Text == "ADD TO CART")
                    {
                        Produtos produto = new Produtos();
                        produto.valor     = float.Parse(item.FindElement(By.ClassName("inventory_item_price")).Text.Replace("$", ""), CultureInfo.InvariantCulture.NumberFormat);
                        produto.descricao = item.FindElement(By.ClassName("inventory_item_desc")).Text;
                        produto.titulo    = Titulo;

                        lista.Add(produto);

                        Button_ADDTOCART.Click();
                    }
                }
            }

            return lista;
        }

        public void VerCarrinhoDeCompra() => ToolBar_Carrinho.Click();
    }
}
