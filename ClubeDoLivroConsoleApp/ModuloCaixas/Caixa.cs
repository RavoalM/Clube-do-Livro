namespace ClubeDoLivroConsoleApp.ModuloCaixas
{
    public class Caixa
    {
        public int Id;
        public string Etiqueta;
        public string Cor;
        public DateTime DataDeEmprestimo;
   
        public Caixa(string etiqueta, string cor, DateTime dataDeEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DataDeEmprestimo = dataDeEmprestimo;
        }
    }
}
