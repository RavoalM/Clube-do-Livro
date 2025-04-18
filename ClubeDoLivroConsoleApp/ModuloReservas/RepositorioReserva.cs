using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloReservas
{
    public class RepositorioReserva
    {
        public Reserva[] reservas = new Reserva[100];
        public int contadorReservas = 0;

        public void CadastrarReserva(Reserva novaReserva)
        {
            novaReserva.Id = GeradorIds.GerarIdReserva();
            novaReserva.Revista.Reservar();
            reservas[contadorReservas++] = novaReserva;
        }

        public bool CancelarReserva(Reserva reservaEscolhida)
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] == null) continue;

                else if (reservas[i].Id == reservaEscolhida.Id)
                {
                    reservas[i] = null;
                    reservaEscolhida.Cancelar();
                    return true;
                }
            }
            return false;
        }

        public bool VerificarReservaAtiva(Reserva reservaEscolhida)
        {
            if (reservaEscolhida.Status != "Ativa")
                return true;
            else
                return false;
        }

        public Reserva SelecionarReservaPorId(int idCaixa)
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                Reserva r = reservas[i];

                if (r == null)
                    continue;

                else if (r.Id == idCaixa)
                    return r;
            }

            return null;
        }

        public Reserva[] SelecionarReservas()
        {
            return reservas;
        }

    }

}
