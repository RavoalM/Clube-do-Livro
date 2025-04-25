using ClubeDoLivroConsoleApp.ModuloEmprestimo;

namespace ClubeDoLivroConsoleApp.ModuloMultas
{
    public class Multa
    {
        public Emprestimo Emprestimo;
        public int DiasAtraso;
        public double ValorMulta;
        public string Status;

        public Multa(Emprestimo emprestimo)
        {
            Emprestimo = emprestimo;
            DiasAtraso = CalcularDiasAtraso();
            ValorMulta = CalcularValorMulta();
            Status = "Pendente";
        }

        public double CalcularValorMulta()
        {
            return 2.0 * DiasAtraso;
        }
        public int CalcularDiasAtraso()
        {
            return DateTime.Now.Day - Emprestimo.ObterDataDevolucao().Day;
        }
        public void PagarMulta()
        {
            Status = "Quitada";
        }
    }
}
