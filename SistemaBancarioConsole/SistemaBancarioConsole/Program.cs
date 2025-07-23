namespace SistemaBancarioConsole.Program
{
    using SistemaBancarioConsole.Data;
    using SistemaBancarioConsole.Models;
    using SistemaBancarioConsole.Services;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Sistema Bancário!");
            RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
            LoginService loginService = new LoginService(repositorioUsuario);
            Usuario usuarioLogado = null;

            bool continuarExecutando = true;

            while (continuarExecutando) 
            {
                Console.WriteLine("\n--- Menu Principal ---");
                Console.WriteLine("1. Registrar Novo Usuário");
                Console.WriteLine("2. Fazer Login");
                Console.WriteLine("3. Sair");
                Console.Write("Digite sua opção: ");

                string opcao = Console.ReadLine();
                int opcaoEscolhida;

                if (int.TryParse(opcao, out opcaoEscolhida))
                {
                    switch(opcaoEscolhida)
                    {
                        case 1:
                            Console.Write("Digite o nome de usuário que você quer registrar: ");
                            string nomeRegistro = Console.ReadLine();
                            Console.Write("Digite a senha que você quer registrar: ");
                            string senhaRegistro = Console.ReadLine();

                            loginService.RegistrarUsuario(nomeRegistro, senhaRegistro);
                            repositorioUsuario.SalvarUsuarios();
                            break;
                        case 2:
                            Console.Write("Digite o nome de usuário: ");
                            string nomeLogin = Console.ReadLine();
                            Console.Write("Digite a senha: ");
                            string senhaLogin = Console.ReadLine();

                            usuarioLogado = loginService.ValidarLogin(nomeLogin, senhaLogin);

                            if (usuarioLogado != null)
                            {
                                Console.WriteLine("Login bem-sucedido. Voltando ao menu principal.");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Saindo do sistema!");
                            continuarExecutando = false;
                            break;

                        default:
                            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número válido.");
                }

            }
        }
    }
}