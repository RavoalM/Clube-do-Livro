using ClubeDoLivroConsoleApp.ModuloCaixas;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class Revista
    {
        public int Id;
        public string Titulo;
        public int NumeroEdicao;
        public int AnoPublicacao;
        public string StatusEmprestimo;
        public Caixa Caixa;

        public Revista(string titulo, int numeroEdicao, int anoPublicao, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicao;
            StatusEmprestimo = "Disponivel";
            Caixa = caixa;
        }
        public void Emprestar()
        {
            StatusEmprestimo = "Emprestada";
        }
        public void Devolver()
        {
            StatusEmprestimo = "Disponível";
        }
        public void Reservar()
        {
            StatusEmprestimo = "Reservada";
        }
    }
}
