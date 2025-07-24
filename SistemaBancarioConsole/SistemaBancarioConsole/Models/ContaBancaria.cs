using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Models
{
    internal class ContaBancaria
    {
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; }
        public string NomeUsuario { get; set; }
        
        public ContaBancaria(string numeroConta, decimal saldo, string nomeUsuario)
        {
            NumeroConta = numeroConta;
            Saldo = 0m;
            NomeUsuario = nomeUsuario;
        }
    }
}
