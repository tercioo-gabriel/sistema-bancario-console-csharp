using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaBancarioConsole.Models; // Importa a classe Usuario
using SistemaBancarioConsole.Utils;  // Importa a classe ArquivoHelper
using System.Collections.Generic;

namespace SistemaBancarioConsole.Data
{
    internal class RepositorioUsuario
    {
        private List<Usuario> _usuarios;
        private const string NOME_ARQUIVO_USUARIOS = "usuarios.txt";

        public RepositorioUsuario()
        {
            _usuarios = new List<Usuario>();
            CarregarUsuarios();
        }

        private void CarregarUsuarios()
        {
            var linhasDoArquivo = ArquivoHelper.LerLinhas(NOME_ARQUIVO_USUARIOS);
            _usuarios.Clear();
            foreach (var linha in linhasDoArquivo) 
            {
                var partes = linha.Split(',');
                if (partes.Length == 2)
                {
                    var usuario = new Usuario(partes[0], partes[1]);
                    _usuarios.Add(usuario);
                }
            }
        }

        public void AdicionarUsuario(Usuario novoUsuario) 
        {
            _usuarios.Add(novoUsuario);
        }

        public void SalvarUsuarios() 
        { 
            List<string> linhasParaEscrever = new List<string>();

            foreach (var usuarios in _usuarios) 
            { 
                string linhaDoArquivoParaSalvar = $"{usuarios.Nome},{usuarios.Senha}";
                linhasParaEscrever.Add(linhaDoArquivoParaSalvar);
            }

            ArquivoHelper.EscreverLinhas(NOME_ARQUIVO_USUARIOS, linhasParaEscrever);
        }

        public Usuario BuscarPorNome(string nomeUsuario) 
        {
            foreach (var usuario in _usuarios)
            {
                if (usuario.Nome.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase)) 
                { 
                    Console.WriteLine($"Usuário encontrado: {usuario.Nome}");
                    return usuario;
                }
            }

            return null;
        }
    }
}
