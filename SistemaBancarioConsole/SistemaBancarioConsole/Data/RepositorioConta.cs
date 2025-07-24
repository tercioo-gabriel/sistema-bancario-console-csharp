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
        private List<ContaBancaria> _contas;
        private const string NOME_ARQUIVO_CONTAS = "contas.txt";

        public RepositorioConta()
        {
            _contas = new List<ContaBancaria>();
            //CarregarContas();
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
                    Console.WriteLine($"Erro ao carregar conta: Saldo inválido '{saldoString}' na linha '{linha}'.");
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

        public void AdicionarConta(ContaBancaria novaConta)
        {
            _contas.Add(novaConta);
            SalvarContas();
        }

        public ContaBancaria BuscarPorNumeroConta(string numConta)
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
    }
}
