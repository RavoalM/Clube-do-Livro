using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloCaixas;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class RepositorioRevista
    {
        public Revista[] revistas = new Revista[100];
        public int contadorRevistas = 0;

        public void CadastrarRevista(Revista novaRevista)
        {
            novaRevista.Id = GeradorIds.GerarIdRevista();

            revistas[contadorRevistas++] = novaRevista;
        }

        public bool EditarRevista(int idRevista, Revista revistaEditada)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null)
                {
                    continue;
                }

                else if (revistas[i].Id == idRevista)
                {
                    revistas[i].Titulo = revistaEditada.Titulo;
                    revistas[i].NumeroEdicao = revistaEditada.NumeroEdicao;
                    revistas[i].AnoPublicacao = revistaEditada.AnoPublicacao;
                    revistas[i].StatusEmprestimo = revistaEditada.StatusEmprestimo;
                    revistas[i].Caixa = revistaEditada.Caixa;

                    return true;
                }
            }

            return false;
        }

        public bool ExcluirRevista(int idCaixa)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;

                else if (revistas[i].Id == idCaixa)
                {
                    revistas[i] = null;

                    return true;
                }
            }

            return false;
        }

        public Revista SelecionarRevistaPorId(int idRevistas)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                Revista r = revistas[i];

                if (r == null)
                    continue;

                else if (r.Id == idRevistas)
                    return r;
            }

            return null;
        }

        public Revista[] SelecionarRevistas()
        {
            return revistas;
        }
    }
}
