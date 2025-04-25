using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.Utils;
using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;

namespace ClubeDoLivroConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal.Intruducao();

            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.ApresentarMenuPrincipal();

                TelaBase telaSelecionada = telaPrincipal.ObterTela();

                char opcaoEscolhida = telaSelecionada.ApresentarMenu();


                if (telaSelecionada is TelaAmigo)
                {
                    TelaAmigo telaAmigo = (TelaAmigo)telaSelecionada;

                    if (opcaoEscolhida == '5')
                    {
                        telaAmigo.VisualizarEmprestimosAmigo();
                        continue;
                    }
                }

                if (telaSelecionada is TelaCaixa)
                {
                    TelaCaixa telaCaixa = (TelaCaixa)telaSelecionada;

                    if (opcaoEscolhida == '5')
                    {
                        telaCaixa.VisualizarRevistasNaCaixa();
                        continue;
                    }
                }

                if (telaSelecionada is TelaEmprestimo)
                {
                    TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaSelecionada;

                    if (opcaoEscolhida == '5')
                    {
                        telaEmprestimo.RegistrarDevolucao();
                        continue;
                    }
                }

                switch (opcaoEscolhida)
                {
                    case '1': telaSelecionada.CadastrarRegistro(); break;

                    case '2': telaSelecionada.EditarRegistro(); break;

                    case '3': telaSelecionada.ExcluirRegistro(); break;

                    case '4': telaSelecionada.VisualizarRegistros(true); break;

                    default: break;
                }
            }
        }
    }
}
