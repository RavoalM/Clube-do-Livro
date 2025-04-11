using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloCaixas
{
    public class RepositorioCaixa
    {
        public Caixa[] caixas = new Caixa[100];
        public int contadorCaixas = 0;

        public void CadastrarCaixa(Caixa novaCaixa)
        {
            novaCaixa.Id = GeradorIds.GerarIdCaixa();

            caixas[contadorCaixas++] = novaCaixa;
        }

        public Caixa SelecionarCaixaPorId(int idCaixa)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                Caixa c = caixas[i];

                if (c == null)
                    continue;

                else if (c.Id == idCaixa)
                    return c;
            }

            return null;
        }

        public Caixa[] SelecionarCaixas()
        {
            return caixas;
        }

    }
}
