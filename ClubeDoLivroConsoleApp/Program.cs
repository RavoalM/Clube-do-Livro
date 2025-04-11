using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.Gerais;
using System.Reflection;

namespace ClubeDoLivroConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();

            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);
            TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);

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

                        case '2': telaAmigo.EditarAmigo(); break;

                        case '3': telaAmigo.ExcluirAmigo(); break;

                        case '4': telaAmigo.VisualizarAmigos(true); break;

                        default: break;
                    }
                }

                if (opcaoPrincipal == '2')
                {
                    char opcaoEscolhida = telaCaixa.ApresentarMenu();

                    switch (opcaoEscolhida)
                    {
                        case '1': telaCaixa.CadastrarCaixa(); break;

                        case '2': telaCaixa.EditarCaixa(); break;

                        case '3': telaCaixa.ExcluirCaixa(); break;

                        case '4': telaCaixa.VisualizarCaixas(true); break;

                        default: break;
                    }
                }
            }
        }
    }
}
