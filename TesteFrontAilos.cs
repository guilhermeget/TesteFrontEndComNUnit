using TesteFrontAilos.ConfProjeto;
using TesteFrontAilos.PageObjects;

namespace TesteFront
{
    [TestFixture]
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            DriverConf.CriarInstancia();
        }

        [Test]
        public void TestLogin()
        {
            TelaDeLogin TelaLogin = new TelaDeLogin();

            //Usuário sem informações.
            TelaLogin.InformaUsuario("");
            TelaLogin.EfetuaLogin();

            //Validando se é apresentado uma mensagem que para efetuar o login deve conter o username
            Assert.That("Epic sadface: Username is required", Is.EqualTo(TelaLogin.ValidaLogin()), "Mensagem não confere com o esperado.");

            //Informando o usuário locked_out_user
            TelaLogin.InformaUsuario("locked_out_user");

            //Deixando o compa Password vazio para validar que não pode ser feito login sem o password
            TelaLogin.InformaSenha("");
            TelaLogin.EfetuaLogin();

            Assert.That("Epic sadface: Password is required", Is.EqualTo(TelaLogin.ValidaLogin()), "Mensagem não confere com o esperado.");

            TelaLogin.InformaSenha("secret_sauce");
            TelaLogin.EfetuaLogin();

            Assert.That("Epic sadface: Sorry, this user has been locked out.", Is.EqualTo(TelaLogin.ValidaLogin()), "Mensagem não confere com o esperado.");
        }

        [Test]
        public void ComprandoTresItens() 
        {
            TelaDeLogin TelaLogin = new TelaDeLogin();

            //Informando usuário com permissão para comprar
            TelaLogin.InformaUsuario("standard_user");
            TelaLogin.InformaSenha("secret_sauce");
            TelaLogin.EfetuaLogin();

            Produtos produtos = new Produtos();

            //Informando qual o produtos serão adicionados no carrinho.
            string[] arrayItens = { "Sauce Labs Backpack", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie" };
            List<Produtos> ListaProdutos = produtos.AdicionarProdutoNoCarrinho(arrayItens);

            //Verificando a lista de carrinho se foi adicionado os itens.
            produtos.VerCarrinhoDeCompra();

            //Verificando os itens no carrinho
            Carrinho carrinho = new Carrinho();
            carrinho.ValidaItensNoCarrinho(ListaProdutos);
            carrinho.Checkout();

            //Informando um Checkout com algumas informações.
            Checkout checkout = new Checkout();
            checkout.InformaNome("Guilherme");
            checkout.InformaSobreNome("Get");
            checkout.InformaPostalCode("88000 – 88099");
            checkout.Continuar();

            //Verificando os produtos e os valores de todos os produtos.
            CheckoutOverview checkoutOverview = new CheckoutOverview();
            checkoutOverview.ValidaItensNoCheckout(ListaProdutos);
            checkoutOverview.ValidaItemTotal(ListaProdutos);
            checkoutOverview.ValidaTotal();
            checkoutOverview.Finish();

            CheckoutComplete checkoutComplete = new CheckoutComplete();
            checkoutComplete.BackHome();
        }

        [Test]
        public void ComprandoTodosItens()
        {
            TelaDeLogin TelaLogin = new TelaDeLogin();

            //Informando usuário com permissão para comprar
            TelaLogin.InformaUsuario("standard_user");
            TelaLogin.InformaSenha("secret_sauce");
            TelaLogin.EfetuaLogin();

            //Selecionando todos os produtos e finalizando as vendas.
            Produtos Todosprodutos = new Produtos();

            string[] arrayTodosItens = { "Sauce Labs Backpack", "Sauce Labs Bike Light", "Sauce Labs Bolt T-Shirt", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie", "Test.allTheThings() T-Shirt (Red)" };
            List<Produtos> ListaProdutos = Todosprodutos.AdicionarProdutoNoCarrinho(arrayTodosItens);

            //Verificando a lista de carrinho se foi adicionado os itens.
            Todosprodutos.VerCarrinhoDeCompra();

            //Verificando os itens no carrinho
            Carrinho carrinho = new Carrinho();
            carrinho.ValidaItensNoCarrinho(ListaProdutos);
            carrinho.Checkout();

            //Informando um Checkout com algumas informações.
            Checkout checkout = new Checkout();
            checkout.InformaNome("Guilherme");
            checkout.InformaSobreNome("Get");
            checkout.InformaPostalCode("88000 – 88099");
            checkout.Continuar();

            //Verificando os produtos e os valores de todos os produtos.
            CheckoutOverview checkoutOverview = new CheckoutOverview();
            checkoutOverview.ValidaItensNoCheckout(ListaProdutos);
            checkoutOverview.ValidaItemTotal(ListaProdutos);
            checkoutOverview.ValidaTotal();
            checkoutOverview.Finish();

            CheckoutComplete checkoutComplete = new CheckoutComplete();
            checkoutComplete.BackHome();
        }

        [TearDown]
        public void FecharJanela() => DriverConf.FecharInstancia();
    }
}