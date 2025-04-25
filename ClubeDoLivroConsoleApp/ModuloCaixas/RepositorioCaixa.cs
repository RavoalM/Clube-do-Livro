using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloCaixas
{
    public class RepositorioCaixa : RepositorioBase
    {
        public bool VerificarEtiquetas(Caixa caixaVerificar)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;
                
                Caixa caixa = (Caixa)registros[i];

                if (caixaVerificar.Etiqueta == caixa.Etiqueta)
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
    }
}
