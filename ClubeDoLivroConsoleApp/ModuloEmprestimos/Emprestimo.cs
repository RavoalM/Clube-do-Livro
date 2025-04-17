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

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            Situacao = "Aberta";
        }

        public string Validar()
        {
            string erros = "";

            if (Amigo == null)
            {
                erros += "O campo 'Amigo' é obrigatório.\n";
            }
            if (Revista == null)
            {
                erros += "O campo 'Revista' é obrigatório.\n";
            }
            
            return erros;
        }

        public DateTime ObterDataDevolucao()
        {
            return DataEmprestimo.AddDays(Revista.Caixa.DiasDeEmprestimo);
        }

        public void RegistrarDevolucao()
        {
            Situacao = "Concluído";
            Revista.Devolver();
        }
    }
}
