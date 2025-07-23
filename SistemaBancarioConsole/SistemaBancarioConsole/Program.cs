namespace SistemaBancarioConsole.Program
{
    using SistemaBancarioConsole.Data;
    using SistemaBancarioConsole.Models;
    using SistemaBancarioConsole.Services;
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();

            Console.WriteLine("Tentando adicionar um usuário de teste...");
            Usuario usuarioTeste = new Usuario("teste", "123");
            repositorio.AdicionarUsuario(usuarioTeste);

            Console.WriteLine("Sistema Bancário Console iniciado.");
            Console.ReadKey();
        }
    }
}