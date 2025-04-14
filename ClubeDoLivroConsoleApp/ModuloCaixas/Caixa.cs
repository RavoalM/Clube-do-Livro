using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloCaixas
{
    public class Caixa
    {
        public int Id;
        public string Etiqueta;
        public string Cor;
        public int DiasDeEmprestimo;
        public Revista[] Revistas = new Revista[40];

        public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
        }

        public void AdicionarRevista(Revista revista)
        {
            for (int i = 0; i < Revistas.Length; i++)
            {
                if (Revistas[i] == null)
                {
                    Revistas[i] = revista;
                    return;
                }
            }
        }

        public void RemoverRevista(Revista revista)
        {
            for (int i = 0; i < Revistas.Length; i++)
            {
                if (Revistas[i] == null) continue;

                else if (Revistas[i] == revista)
                {
                    Revistas[i] = null;
                    return;
                }
            }
        }

        public Revista[] ObterRevistas()
        {
            return Revistas;
        }
    }
}
