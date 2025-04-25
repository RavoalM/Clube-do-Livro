using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloReservas
{
    public class Reserva : EntidadeBase
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataReserva { get; set; }
        public string Status { get; set; }

        public Reserva(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataReserva = DateTime.Now;
            Status = "Ativa";
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

        public DateTime ObterDataValidade()
        {
            return DataReserva.AddDays(2);
        }

        public void Concluir()
        {
            Status = "Concluída";
        }

        public void Cancelar()
        {
            Revista.StatusEmprestimo = "Disponível";
        }

        public override void AtualizarRegistro(EntidadeBase registroEditado)
        {
            Reserva reservaEditada = (Reserva)registroEditado;

            Amigo = reservaEditada.Amigo;
            Revista = reservaEditada.Revista;
        }
    }
}
