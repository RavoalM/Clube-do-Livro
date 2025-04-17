using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloRevistas;
using System.Runtime.Serialization;

namespace ClubeDoLivroConsoleApp.ModuloReservas
{
    public class Reserva
    {
        public int Id;
        public Amigo Amigo;
        public Revista Revista;
        public DateTime DataReserva;
        public string Status;

        public Reserva(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataReserva = DateTime.Now;
            Status = "Ativa";
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

        public void Concluir()
        {
            Status = "Concluída";
        }
        public void Cancelar()
        {
            Revista.StatusEmprestimo = "Disponível";
        }
    }
}
