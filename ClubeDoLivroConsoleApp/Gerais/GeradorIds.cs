namespace ClubeDoLivroConsoleApp.Gerais
{
    public class GeradorIds
    {
        public static int IdAmigo;
        public static int IdCaixa;
        public static int IdRevista;
        public static int IdEmprestimo;
        public static int idReserva;
        public static int IdMulta;

        public static int GerarIdAmigo()
        {
            return ++IdAmigo;
        }

        public static int GerarIdCaixa()
        {
            return ++IdCaixa;
        }

        public static int GerarIdRevista()
        {
            return ++IdRevista;
        }

        public static int GerarIdEmprestimo()
        {
            return ++IdEmprestimo;
        }

        public static int GerarIdReserva()
        {
            return ++idReserva;
        }

        public static int GerarIdMulta()
        {
            return ++IdMulta;
        }
    }
}
