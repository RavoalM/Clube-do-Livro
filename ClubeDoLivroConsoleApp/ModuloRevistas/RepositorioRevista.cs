using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class RepositorioRevista
    {
        public Revista[] revistas = new Revista[100];
        public int contadorRevistas = 0;

        public void CadastrarRevista(Revista novaRevista)
        {
            novaRevista.Id = GeradorIds.GerarIdRevista();
            novaRevista.Caixa.AdicionarRevista(novaRevista);
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

                    return true;
                }
            }

            return false;
        }

        public bool ExcluirRevista(int idRevistas)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;

                else if (revistas[i].Id == idRevistas)
                {
                    revistas[i].Caixa.RemoverRevista(revistas[i]);
                    revistas[i] = null;

                    return true;
                }
            }

            return false;
        }

        public bool VerificarIndenfidicacaoRevista(Revista revistaVerificar)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null)
                    continue;

                if (revistaVerificar.Titulo == revistas[i].Titulo && revistaVerificar.NumeroEdicao == revistas[i].NumeroEdicao)
                    return true;
            }

            return false;
        }

        public bool VerificarRevistaReservada(Revista revistaEscolhida)
        {
            if (revistaEscolhida.StatusEmprestimo == "Reservada")
                return true;
            else
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
