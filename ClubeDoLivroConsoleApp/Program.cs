using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloRevistas;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;

namespace ClubeDoLivroConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
            RepositorioRevista repositorioRevista = new RepositorioRevista();
            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

            TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
            TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa, repositorioRevista);
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioRevista, repositorioAmigo, repositorioCaixa);

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

                        case '5': telaAmigo.VisualizarEmprestimosAmigo(); break;

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

                        case '5': telaCaixa.VisualizarRevistasNaCaixa(); break;

                        default: break;
                    }
                }

                if (opcaoPrincipal == '3')
                {
                    char opcaoEscolhida = telaRevista.ApresentarMenu();

                    switch (opcaoEscolhida)
                    {
                        case '1': telaRevista.CadastraRevista(); break;

                        case '2': telaRevista.EditarRevista(); break;

                        case '3': telaRevista.ExcluirRevista(); break;

                        case '4': telaRevista.VisualizarRevistas(true); break;

                        default: break;
                    }
                }

                if (opcaoPrincipal == '4')
                {
                    char opcaoEscolhida = telaEmprestimo.ApresentarMenu();

                    switch (opcaoEscolhida)
                    {
                        case '1': telaEmprestimo.CadastrarEmprestimo(); break;

                        case '2': telaEmprestimo.EditarEmprestimo(); break;

                        case '3': telaEmprestimo.ExcluirEmprestimo(); break;

                        case '4': telaEmprestimo.VisualizarEmprestimos(true); break;

                        case '5': telaEmprestimo.RegistrarDevolucao(); break;

                        default: break;
                    }
                }
            }
        }
    }
}
