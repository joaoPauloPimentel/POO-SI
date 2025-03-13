namespace XulambsFoods_2025_1.src {
    internal class XulambsPizza {
        #region static Pedidos
        const int MaxPedidos = 100;
 
        static Pedido[] _pedidos = new Pedido[MaxPedidos];
        static int _quantPedidos = 0;
        #endregion
 
        #region CLI
        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA v0.2\n================");
        }
 
        static void Pausa() {
            Console.WriteLine("Digite enter para continuar...");
            Console.ReadLine();
        }
 
        static int ExibirMenuPrincipal() {
            Cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Alterar Pedido");
            Console.WriteLine("3 - Retorio do Pedido");
            Console.WriteLine("4 - Fechar Pedido");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }
 
        static int ExibirMenuIngredientes(Pizza pizza) {
            Cabecalho();
            Console.WriteLine("Personalizar a Pizza\n");
            MostrarNota(pizza);
            Console.WriteLine("\n1 - Acrescentar ingredientes");
            Console.WriteLine("2 - Retirar ingredientes");
            Console.WriteLine("0 - Não quero alterar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }
 
        static int ExibirMenuLocalizacao() {
            Cabecalho();
            Console.WriteLine("Localizando o pedido: ");
            Console.Write("Digite o número do pedido: ");
            return int.Parse(Console.ReadLine());
        }
 
        #endregion
 
        static Pedido AbrirPedido() {
            Pedido novo = new Pedido();
            IncluirPizzasPedido(novo);
            return novo;
        }
 
        static void IncluirPizzasPedido(Pedido pedido) {
            string conf;
            do {
                Pizza novaPizza = ComprarPizza();
                pedido.Adicionar(novaPizza);
                Console.Write("\nQuer uma nova pizza (S/N)? ");
                conf = Console.ReadLine().ToUpper();
            } while (conf.Equals("S"));
        }

 
        static Pizza ComprarPizza() {
            Cabecalho();
            Console.WriteLine("Comprando uma nova pizza:");
            Pizza novaPizza = new Pizza();
            EscolherIngredientes(novaPizza);
            Console.WriteLine();
            MostrarNota(novaPizza);
            return novaPizza;
        }
 
        static void EscolherIngredientes(Pizza pizza) {
            int opcao = ExibirMenuIngredientes(pizza);
            while(opcao!=0){
                Console.Write("Quantos ingredientes? ");
                int adicionais = int.Parse(Console.ReadLine());
                switch(opcao) {
                    case 1: pizza.AdicionarIngredientes(adicionais);
                        break;
                    case 2: pizza.RetirarIngredientes(adicionais);
                        break;
                };
                Console.WriteLine();
                MostrarNota(pizza);
                Pausa();
                opcao = ExibirMenuIngredientes(pizza);
            } 
        }
 
        static void MostrarNota(Pizza pizza) {
            Console.WriteLine("Comprando: ");
            Console.WriteLine(pizza.NotaDeCompra());
 
        }
 
        static void MostrarPedido(Pedido pedido) {
            Cabecalho();
            Console.WriteLine(pedido.Relatorio());
        }
 
        static void ArmazenarPedido(Pedido novo) {
            if(_quantPedidos < MaxPedidos) {
                _pedidos[_quantPedidos] = novo;
                _quantPedidos++;
            }
        }
 
        static Pedido AlterarPedido() {
            Pedido localizado = LocalizarPedido();
            if(localizado == null) {
                Console.WriteLine("Pedido não encontrado");
            }
            else {
                IncluirPizzasPedido(localizado);
            }
            return localizado;
        }
 
        static Pedido LocalizarPedido() {
            int numero = ExibirMenuLocalizacao();
            Pedido buscado = null;
            for (int i = 0; i < _quantPedidos && buscado == null; i++) {
                if (_pedidos[i].GetID() == numero)
                    buscado = _pedidos[i];
            }
            return buscado;
        }



 
        static void Main(string[] args) {
            int opcao = -1;
            do {
                opcao = ExibirMenuPrincipal();
                switch (opcao) {
                    case 1:
                        Pedido novo = AbrirPedido();
                        MostrarPedido(novo);
                        ArmazenarPedido(novo);
                        break;
                    case 2: 
                        Pedido alterado = AlterarPedido();
                        if(alterado != null)
                            MostrarPedido(alterado);
                        else
                            System.Console.WriteLine("Pedido não encontrado");
                        break;
                    case 3:
                        Pedido localizado = LocalizarPedido();
                        if(localizado != null)
                            MostrarPedido(localizado);
                        break;
                    case 4: 
                        Pedido fechado = LocalizarPedido();
                        if(fechado != null)
                            fechado.FecharPedido();
                            System.Console.WriteLine("Fechado");
                            
                        break;
                    case 0: Console.WriteLine("FLW VLW OBG VLT SMP.");
                        break;
                }
                Console.ReadKey();
            } while (opcao != 0);
        }
    }
}