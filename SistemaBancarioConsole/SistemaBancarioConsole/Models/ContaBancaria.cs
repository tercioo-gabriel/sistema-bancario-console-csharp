using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Models
{
    internal class ContaBancaria
    {
        public int TipoConta { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; }
        public string NomeUsuario { get; set; }
        
        public ContaBancaria(int tipoConta, string numeroConta, decimal saldo, string nomeUsuario)
        {
            TipoConta = tipoConta;
            NumeroConta = numeroConta;
            Saldo = saldo;
            NomeUsuario = nomeUsuario;
        }

        public ContaBancaria(string numeroConta, decimal saldo, string nomeUsuario)
        {
            NumeroConta = numeroConta;
            Saldo = saldo;
            NomeUsuario = nomeUsuario;

            TipoConta = numeroConta.StartsWith("0") ? 2 : 1;
        }
    }
}
