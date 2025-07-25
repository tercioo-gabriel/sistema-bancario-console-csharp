using System;
using System.Globalization;
using SistemaBancarioConsole.Data;
using SistemaBancarioConsole.Models;
using SistemaBancarioConsole.Services;

namespace SistemaBancarioConsole.Program
{
    internal class Program
    {
        private const int TIPO_CONTA_CORRENTE = 1;
        private const int TIPO_CONTA_POUPANCA = 2;

        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Sistema Bancário!");

            RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
            RepositorioConta repositorioConta = new RepositorioConta();

            ContaService contaService = new ContaService(repositorioConta);
            LoginService loginService = new LoginService(repositorioUsuario);

            Usuario? usuarioLogado = null;
            bool continuarExecutando = true;

            while (continuarExecutando) 
            {
                if (usuarioLogado == null) {
                    Console.WriteLine("\n--- Menu Principal ---");
                    Console.WriteLine("1. Registrar Novo Usuário");
                    Console.WriteLine("2. Fazer Login");
                    Console.WriteLine("3. Sair");
                    Console.Write("Digite sua opção: ");

                    string? opcao = Console.ReadLine();
                    int opcaoEscolhida;

                    if (int.TryParse(opcao, out opcaoEscolhida))
                    {
                        switch (opcaoEscolhida)
                        {
                            case 1: // Registrar Novo Usuário
                                Console.Write("Digite o nome de usuário que você quer registrar: ");
                                string nomeRegistro = Console.ReadLine();
                                Console.Write("Digite a senha que você quer registrar: ");
                                string? senhaRegistro = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nomeRegistro) || string.IsNullOrWhiteSpace(senhaRegistro))
                                {
                                    Console.WriteLine("ERRO: Nome de usuário e senha não podem ser vazios.");
                                } else loginService.RegistrarUsuario(nomeRegistro, senhaRegistro);
                                break;

                            case 2: // Fazer Login
                                Console.Write("Digite o nome de usuário: ");
                                string? nomeLogin = Console.ReadLine();
                                Console.Write("Digite a senha: ");
                                string? senhaLogin = Console.ReadLine();

                                usuarioLogado = loginService.ValidarLogin(nomeLogin, senhaLogin);

                                if (usuarioLogado != null)
                                {
                                    Console.WriteLine($"Login bem sucedido. Bem-vindo(a), {usuarioLogado.NomeUsuario}!");
                                }
                                break;

                            case 3: // Sair
                                Console.WriteLine("Saindo do sistema!");
                                continuarExecutando = false;
                                break;

                            default:
                                Console.WriteLine("ERRO: Opção inválida. Por favor, escolha uma opção válida.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERRO: Entrada inválida. Por favor, digite um número válido.");
                    }

                }
                else 
                {
                    Console.WriteLine($"\n--- Menu de Operações Bancárias ({usuarioLogado.NomeUsuario}) ---");
                    Console.WriteLine("1. Criar Conta Bancária (Corrente/Poupança)");
                    Console.WriteLine("2. Realizar Depósito");
                    Console.WriteLine("3. Realizar Saque");
                    Console.WriteLine("4. Realizar Transferência");
                    Console.WriteLine("5. Exibir Saldo");
                    Console.WriteLine("6. Fazer Logout");
                    Console.WriteLine("7. Sair do Sistema");
                    Console.Write("Digite sua opção: ");

                    string? opcao = Console.ReadLine();
                    int opcaoEscolhida;


                    if (int.TryParse(opcao, out opcaoEscolhida))
                    {
                        switch (opcaoEscolhida)
                        {
                            case 1: // Criar Conta Bancária
                                Console.Write("Digite o número da conta (3 a 5 dígitos): ");
                                string? numeroContaBase = Console.ReadLine();
                                Console.Write("Deseja criar uma conta poupança associada? (sim/não): ");
                                string? criarPoupancaStr = Console.ReadLine()?.ToLower();
                                bool criarPoupanca = criarPoupancaStr == "sim";

                                if (string.IsNullOrWhiteSpace(numeroContaBase))
                                {
                                    Console.WriteLine("ERRO: Número da conta não pode ser vazio.");
                                }
                                else
                                {
                                    repositorioConta.AdicionarConta(usuarioLogado.NomeUsuario, numeroContaBase, criarPoupanca);
                                }
                                break;

                            case 2: // Realizar Depósito
                                Console.Write("Digite o número da conta para depósito: ");
                                string? numContaDeposito = Console.ReadLine();
                                Console.Write("Digite o valor do depósito: ");
                                string? valorDepositoStr = Console.ReadLine();
                                decimal valorDeposito;

                                if (string.IsNullOrWhiteSpace(numContaDeposito) || !decimal.TryParse(valorDepositoStr, NumberStyles.Currency, CultureInfo.CurrentCulture, out valorDeposito))
                                {
                                    Console.WriteLine("ERRO: Entrada inválida. Verifique o número da conta e o valor.");
                                }
                                else
                                {
                                    contaService.RealizarDeposito(numContaDeposito, valorDeposito);
                                }
                                break;

                            case 3: // Realizar Saque
                                Console.Write("Digite o número da conta para saque: ");
                                string? numContaSaque = Console.ReadLine();
                                Console.Write("Digite o valor do saque: ");
                                string? valorSaqueStr = Console.ReadLine();
                                decimal valorSaque;

                                if (string.IsNullOrWhiteSpace(numContaSaque) || !decimal.TryParse(valorSaqueStr, NumberStyles.Currency, CultureInfo.CurrentCulture, out valorSaque))
                                {
                                    Console.WriteLine("ERRO: Entrada inválida. Verifique o número da conta e o valor.");
                                }
                                else
                                {
                                    contaService.RealizarSaque(numContaSaque, valorSaque);
                                }
                                break;

                            case 4: // Realizar Transferência
                                Console.Write("Digite o número da CONTA DE ORIGEM: ");
                                string? numContaOrigem = Console.ReadLine();
                                Console.Write("Digite o número da CONTA DE DESTINO: ");
                                string? numContaDestino = Console.ReadLine();
                                Console.Write("Digite o valor da transferência: ");
                                string? valorTransferenciaStr = Console.ReadLine();
                                decimal valorTransferencia;

                                if (string.IsNullOrWhiteSpace(numContaOrigem) || string.IsNullOrWhiteSpace(numContaDestino) || !decimal.TryParse(valorTransferenciaStr, NumberStyles.Currency, CultureInfo.CurrentCulture, out valorTransferencia))
                                {
                                    Console.WriteLine("ERRO: Entrada inválida. Verifique os números das contas e o valor.");
                                }
                                else
                                {
                                    contaService.RealizarTransferencia(numContaOrigem, numContaDestino, valorTransferencia);
                                }
                                break;

                            case 5: // Exibir Saldo
                                Console.Write("Digite o número da conta para exibir o saldo: ");
                                string? numContaExtrato = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(numContaExtrato))
                                {
                                    Console.WriteLine("Número da conta não pode ser vazio.");
                                }
                                else
                                {
                                    contaService.ExibirSaldo(numContaExtrato);
                                }
                                break;

                            case 6: // Fazer Logout
                                usuarioLogado = null;
                                Console.WriteLine("Logout realizado com sucesso.");
                                break;

                            case 7: // Sair do Sistema
                                Console.WriteLine("Saindo do sistema. Até logo!");
                                continuarExecutando = false;
                                break;

                            default:
                                Console.WriteLine("ERRO: Opção inválida. Por favor, escolha uma opção válida.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERRO: Entrada inválida. Por favor, digite um número válido.");
                    }
                }
            }
        }
    }
}