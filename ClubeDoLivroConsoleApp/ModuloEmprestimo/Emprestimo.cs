using ClubeDoLivroConsoleApp.ModuloAmigo;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public Amigo Amigo;
        public Revista Revista;
        public DateTime Data;
        public string Situacao;

        public Emprestimo(DateTime data, string situacao)
        {
            Data = data;
            Situacao = situacao;
        }
    }
}
