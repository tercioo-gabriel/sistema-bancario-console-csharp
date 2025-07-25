using SistemaBancarioConsole.Models;
using SistemaBancarioConsole.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Data
{
    internal class RepositorioConta
    {
        private const int TIPO_CONTA_CORRENTE = 1;
        private const int TIPO_CONTA_POUPANCA = 2;

        private List<ContaBancaria> _contas;
        private const string NOME_ARQUIVO_CONTAS = "contas.txt";

        public RepositorioConta()
        {
            _contas = new List<ContaBancaria>();
            CarregarContas();
        }

        private void CarregarContas()
        {
            var linhasDoArquivo = ArquivoHelper.LerLinhas(NOME_ARQUIVO_CONTAS);
            _contas.Clear();
            foreach (var linha in linhasDoArquivo)
            {
                var partes = linha.Split(',');
                string saldoString = partes[1];
                decimal saldo;

                if (decimal.TryParse(saldoString, NumberStyles.Any, CultureInfo.InvariantCulture, out saldo))
                {
                    string numeroConta = partes[0];
                    string nomeUsuario = partes[2];
                    if (partes.Length == 3)
                    {
                        ContaBancaria conta = new ContaBancaria(numeroConta, saldo, nomeUsuario);
                        _contas.Add(conta);
                    }
                   
                }
                else
                {
                    Console.WriteLine($"ERRO: Erro ao carregar conta: Saldo inválido '{saldoString}' na linha '{linha}'.");
                }
            }
        }

        public void SalvarContas()
        {
            List<string> linhasParaEscrever = new List<string>();

            foreach (var conta in _contas)
            {
                string linhaDoArquivoParaSalvar = $"{conta.NomeUsuario},{conta.Saldo}, {conta.NumeroConta}";
                linhasParaEscrever.Add(linhaDoArquivoParaSalvar);
            }

            ArquivoHelper.EscreverLinhas(NOME_ARQUIVO_CONTAS, linhasParaEscrever);
        }
        
        public void AdicionarConta(string nomeUsuario, string numeroContaBase, bool criarContaPoupanca)
        {
            if(numeroContaBase.Length < 3 || numeroContaBase.Length > 5) 
            {
                Console.WriteLine("ERRO: Número da conta deve ter entre 3 e 5 dígitos.");
                return;
            }

            foreach (char c in numeroContaBase)
            {
                if (!char.IsDigit(c))
                {
                    Console.WriteLine("ERRO: O número da conta deve conter apenas dígitos.");
                    return;
                }
            }

            if (_contas.Any(c => c.NumeroConta == numeroContaBase || c.NumeroConta == "0" + numeroContaBase))
            {
                Console.WriteLine($"ERRO: Uma conta com o número {numeroContaBase} (ou sua poupança) já existe.");
                return;
            }

            ContaBancaria contaCorrente = new ContaBancaria(TIPO_CONTA_CORRENTE, numeroContaBase, 0m, nomeUsuario);
            _contas.Add(contaCorrente);
            Console.WriteLine($"Conta corrente {contaCorrente.NumeroConta} criada com sucesso para {nomeUsuario}! :)");

            if (criarContaPoupanca)
            {
                string numeroContaPoupanca = "0" + numeroContaBase;
                ContaBancaria contaPoupanca = new ContaBancaria(TIPO_CONTA_POUPANCA, numeroContaPoupanca, 0m, nomeUsuario);

                _contas.Add(contaPoupanca);
                Console.WriteLine($"Conta poupança {contaPoupanca.NumeroConta} associada criada com sucesso para {nomeUsuario}!");
            }

            SalvarContas();
        }

        public ContaBancaria? BuscarPorNumeroConta(string numConta)
        {
            foreach (var conta in _contas)
            {
                if (conta.NumeroConta.Equals(numConta, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Conta encontrada: {conta.NumeroConta}");
                    return conta;
                }
            }

            return null;
        }

        public void AtualizarConta(ContaBancaria contaAtualizada) 
        {
            for(int i = 0; i < _contas.Count; i++)
            {
                if (_contas[i].NumeroConta.Equals(contaAtualizada.NumeroConta, StringComparison.OrdinalIgnoreCase))
                {
                    _contas[i] = contaAtualizada;
                    SalvarContas();
                    Console.WriteLine("Conta atualizada com Sucesso");
                }
            }
        }
    }
}
