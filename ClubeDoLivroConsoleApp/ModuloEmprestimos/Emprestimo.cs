using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase<Emprestimo>
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public string Situacao { get; set; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            Situacao = "Aberta";
        }

        public override string Validar()
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

        public override void AtualizarRegistro(Emprestimo emprestimoEditado)
        {
            Amigo = emprestimoEditado.Amigo;
            Revista = emprestimoEditado.Revista;
        }
    }
}
