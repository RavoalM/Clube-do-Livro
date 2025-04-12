using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class RepositorioRevista
    {
        public Revista[] revistas = new Revista[100];
        public int contadorRevistas = 0;

        public void CadastrarRevista(Revista novaRevista)
        {
            novaRevista.Id = GeradorIds.GerarIdCaixa();

            revistas[contadorRevistas++] = novaRevista;
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
