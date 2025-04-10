using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();

            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);

            TelaPrincipal.Intruducao();

            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                char opcaoPrincipal = telaPrincipal.ApresentarMenuPrincipal();

                if (opcaoPrincipal == '1')
                {
                    char opcaoEscolhida = telaAmigo.ApresentarMenu();

                    switch (opcaoEscolhida)
                    {
                        case '1': telaAmigo.CadastrarAmigo(); break;

                        case '2': break;

                        case '3': break;

                        case '4': telaAmigo.VisualizarAmigos(true); break;

                        default: break;
                    }
                }
            }
        }
    }
}
