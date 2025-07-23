using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancarioConsole.Utils
{
    internal class ArquivoHelper
    {
        public static List<string> LerLinhas(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                return new List<string>();
            }

            return File.ReadAllLines(caminhoArquivo).ToList();
        }

        public static void EscreverLinhas(string caminhoArquivo, List<string> linhas)
        {
            File.WriteAllLines(caminhoArquivo, linhas);
        }
    }
}
