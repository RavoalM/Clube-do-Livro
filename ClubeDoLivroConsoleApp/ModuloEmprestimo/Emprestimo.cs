using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int Id;
        public Amigo Amigo;
        public Revista Revista;
        public DateTime DataEmprestimo;
        public string Situacao;

        public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo, string situacao)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = dataEmprestimo;
            Situacao = situacao;
        }
    }
}
