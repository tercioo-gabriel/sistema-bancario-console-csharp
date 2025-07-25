using SistemaBancarioConsole.Data;
using SistemaBancarioConsole.Models;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Services 
{
    internal class ContaService
    {
        private const int TIPO_CONTA_CORRENTE = 1;
        private const int TIPO_CONTA_POUPANCA = 2;

        private const decimal TAXA_FIXA_TRANSACAO = 2.00m;
        private RepositorioConta _repositorioConta;

        public ContaService(RepositorioConta repoConta)
        {
            _repositorioConta = repoConta;
        }

        public void RealizarDeposito(string numeroConta, decimal valor) 
        {
            if (valor <= 0)
            {
                Console.WriteLine("ERRO: Valor de depósito deve ser maior que zero.");
                return;
            }

            ContaBancaria conta = _repositorioConta.BuscarPorNumeroConta(numeroConta);

            if (conta == null)
            {
                Console.WriteLine("ERRO: Conta não encontrada.");
                return;
            }
            
            ContaBancaria contaAtualizada = new ContaBancaria(conta.NumeroConta, conta.Saldo + valor, conta.NomeUsuario);
            _repositorioConta.AtualizarConta(contaAtualizada);
            Console.WriteLine($"Depósito de {valor:C} realizado com sucesso na conta {numeroConta}. Novo saldo: {contaAtualizada.Saldo:C}");
        }

        public void RealizarSaque(string numeroConta, decimal valor) 
        {
            if (valor <= 0)
            {
                Console.WriteLine("ERRO: Valor de saque deve ser maior que zero.");
                return;
            }

            ContaBancaria conta = _repositorioConta.BuscarPorNumeroConta(numeroConta);

            if (conta == null)
            {
                Console.WriteLine("ERRO: Conta não encontrada.");
                return;
            }

            if (conta.Saldo < valor)
            {
                Console.WriteLine("ERRO: Saldo insuficiente para realizar o saque.");
                return;
            }

            ContaBancaria contaAtualizada = new ContaBancaria(conta.NumeroConta, conta.Saldo - valor, conta.NomeUsuario);
            _repositorioConta.AtualizarConta(contaAtualizada);
            Console.WriteLine($"Saque de {valor:C} realizado com sucesso na conta {numeroConta}. Novo saldo: {contaAtualizada.Saldo:C}");
        }

        public void ExibirSaldo(string numeroConta) 
        {
            ContaBancaria conta = _repositorioConta.BuscarPorNumeroConta(numeroConta);

            if (conta == null)
            {
                Console.WriteLine("ERRO: Conta não encontrada.");
                return;
            }

            Console.WriteLine($"A conta: {numeroConta} possui: {conta.Saldo:C} de saldo atual.");
        }

        public void RealizarTransferencia(string numeroContaOrigem, string numeroContaDestino, decimal valor) 
        {

            Console.WriteLine("Iniciando transferência...");
            Console.WriteLine($"Taxa Fixa de transferência {TAXA_FIXA_TRANSACAO:C}");

            if (valor <= 0)
            {
                Console.WriteLine("ERRO: Valor de transferência deve ser maior que zero.");
                return;
            }

            ContaBancaria contaOrigem = _repositorioConta.BuscarPorNumeroConta(numeroContaOrigem);

            if (contaOrigem == null)
            {
                Console.WriteLine("ERRO: Conta de origem não encontrada.");
                return;
            }

            if (contaOrigem.TipoConta != TIPO_CONTA_CORRENTE)
            {
                Console.WriteLine($"ERRO: A conta de origem {numeroContaOrigem} deve ser do tipo Corrente para realizar transferências.");
                return;
            }

            ContaBancaria contaDestino = _repositorioConta.BuscarPorNumeroConta(numeroContaDestino);

            if (contaDestino == null)
            {
                Console.WriteLine("Conta de destino não encontrada.");
                return;
            }
            if (contaDestino.TipoConta != TIPO_CONTA_CORRENTE)
            {
                Console.WriteLine($"ERRO: A conta de destino {numeroContaDestino} deve ser do tipo Corrente para receber transferências.");
                return;
            }

            if (contaOrigem.NumeroConta.Equals(contaDestino.NumeroConta, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("ERRO: Não é possível transferir para a mesma conta de origem.");
                return;
            }

            if (contaOrigem.Saldo < valor + TAXA_FIXA_TRANSACAO)
            {
                Console.WriteLine("ERRO: Saldo insuficiente para realizar a transferência.");
                Console.WriteLine($"TAXA FIXA DE TRANSFERÊNCIA DE QUALQUER VALOR: `{TAXA_FIXA_TRANSACAO:C}");
                return;
            }

            ContaBancaria contaOrigemAtualizada = new ContaBancaria(contaOrigem.NumeroConta, contaOrigem.Saldo - valor - TAXA_FIXA_TRANSACAO, contaOrigem.NomeUsuario);
            _repositorioConta.AtualizarConta(contaOrigemAtualizada);

            ContaBancaria contaDestinoAtualizada = new ContaBancaria(contaDestino.NumeroConta, contaDestino.Saldo + valor, contaDestino.NomeUsuario);
            _repositorioConta.AtualizarConta(contaDestinoAtualizada);

            Console.WriteLine($"Transferência de {valor:C} realizada com sucesso da conta {numeroContaOrigem} para a conta {numeroContaDestino}!");
        }
    }
}