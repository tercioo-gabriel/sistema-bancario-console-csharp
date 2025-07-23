using SistemaBancarioConsole.Data;
using SistemaBancarioConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Services
{
    internal class LoginService
    {
        private RepositorioUsuario _repositorioUsuario;

        public LoginService(RepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }

        public void RegistrarUsuario(string nomeUsuario, string senhaUsuario) 
        {
            if (_repositorioUsuario.BuscarPorNome(nomeUsuario) != null) 
            {
                Console.WriteLine("Usuário já existe. Tente novamente.");
                return;
            }
            else 
            {
                Usuario registrarNovoUsuario = new Usuario(nomeUsuario, senhaUsuario);
                _repositorioUsuario.AdicionarUsuario(registrarNovoUsuario);
                Console.WriteLine($"Usuário {nomeUsuario} registrado com sucesso!");
            }
        }

        public Usuario ValidarLogin(string nomeUsuario, string senhaUsuario) 
        {
            Usuario usuarioEncontrado = _repositorioUsuario.BuscarPorNome(nomeUsuario);

            if (usuarioEncontrado == null)
            {
                Console.WriteLine("Usuário não encontrado. Tente novamente.");
                return null;
            }
            else
            {
                if (senhaUsuario.Equals(usuarioEncontrado.Senha))
                {
                    Console.WriteLine($"Bem-vindo, {usuarioEncontrado.Nome}!");
                    return usuarioEncontrado;
                }
                else
                {
                    Console.WriteLine("Senha incorreta. Tente novamente.");
                    return null;
                }
            }
        }
    }
}
