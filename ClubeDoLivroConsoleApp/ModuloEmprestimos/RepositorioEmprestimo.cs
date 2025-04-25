using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public bool VerificarEmprestimosAmigo(Amigo amigoEscolhido)
        {
            int emprestimos = 0;

            if (amigoEscolhido.Emprestimos == null)
                return false;

            foreach (Emprestimo e in amigoEscolhido.Emprestimos)
            {
                if (e != null && e.Situacao != "Concluído")
                    return true;
            }

            if (emprestimos > 0)
                return true;
            else
                return false;
        }

        public void VerificarEmprestimosAtrasados(Emprestimo[] emprestimosRegistrados)
        {
            foreach (Emprestimo e in emprestimosRegistrados)
            {
                if (e == null)
                    continue;

                if (e.Situacao == "Concluído")
                    continue;

                if (DateTime.Now > e.ObterDataDevolucao())
                {
                    ConsoleColor Red;
                    e.Situacao = "ATRASADO";
                    Console.ResetColor();
                }

            }
        }
    }
}
