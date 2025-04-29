using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloReservas
{
    public class RepositorioReserva : RepositorioBase<Reserva>
    {
        public Reserva[] reservas = new Reserva[100];
        public int contadorReservas = 0;

        public bool VerificarReservaAtiva(Reserva reservaEscolhida)
        {
            if (reservaEscolhida.Status != "Ativa")
                return true;
            else
                return false;
        }

    }

}
