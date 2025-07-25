using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Models
{
    internal class Usuario
    {
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }


        public Usuario(string nomeUsuario, string senhaUsuario)
        {
            NomeUsuario = nomeUsuario;
            SenhaUsuario = senhaUsuario;
        }
      
    }
}
