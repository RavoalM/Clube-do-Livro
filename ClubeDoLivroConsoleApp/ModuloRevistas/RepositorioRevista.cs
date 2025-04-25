using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class RepositorioRevista : RepositorioBase
    {
        public bool VerificarIndenfidicacaoRevista(Revista revistaVerificar)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;
                
                Revista revista = (Revista)registros[i];

                if (revistaVerificar.Titulo == revista.Titulo && revistaVerificar.NumeroEdicao == revista.NumeroEdicao)
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
    }
}
