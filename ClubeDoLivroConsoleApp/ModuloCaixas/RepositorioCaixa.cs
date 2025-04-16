using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloRevistas;

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

        public bool EditarCaixa(int idCaixa, Caixa caixaEditada)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                if (caixas[i] == null) continue;

                else if (caixas[i].Id == idCaixa)
                {
                    caixas[i].Etiqueta = caixaEditada.Etiqueta;
                    caixas[i].Cor = caixaEditada.Cor;
                    caixas[i].DiasDeEmprestimo = caixaEditada.DiasDeEmprestimo;

                    return true;
                }
            }

            return false;
        }

        public bool ExcluirCaixa(int idCaixa)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                if (caixas[i] == null) continue;

                else if (caixas[i].Id == idCaixa)
                {
                    caixas[i] = null;

                    return true;
                }
            }

            return false;
        }

        public bool VerificarEtiquetas(Caixa caixaVerificar)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                if (caixas[i] == null)
                    continue;

                if (caixaVerificar.Etiqueta == caixas[i].Etiqueta)
                    return true;
            }

            return false;
        }

        public bool VerificarRevistasCaixa(Caixa caixaEscolhida)
        {
            if (caixaEscolhida.Revistas == null)
                return false;

            foreach (Revista r in caixaEscolhida.Revistas)
            {
                if (r != null)
                    return true; 
            }

            return false; 
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
