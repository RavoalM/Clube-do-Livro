using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo
    {
        public Emprestimo[] emprestimos = new Emprestimo[100];
        public int contadorEmprestimos = 0;

        public void CadastrarEmprestimo(Emprestimo novoEmprestimo)
        {
            novoEmprestimo.Id = GeradorIds.GerarIdEmprestimo();
            novoEmprestimo.Revista.Emprestar();
            emprestimos[contadorEmprestimos++] = novoEmprestimo;
        }

        public bool EditarEmprestimo(int idEmprestimo, Emprestimo emprestimoEditado)
        {
            for (int i = 0; i < emprestimos.Length; i++)
            {
                if (emprestimos[i] == null) continue;

                else if (emprestimos[i].Id == idEmprestimo)
                {
                    emprestimos[i].Amigo = emprestimoEditado.Amigo;
                    emprestimos[i].Revista = emprestimoEditado.Revista;

                    return true;
                }
            }

            return false;
        }

        public bool ExcluirEmprestimo(int idEmprestimo)
        {
            for (int i = 0; i < emprestimos.Length; i++)
            {
                if (emprestimos[i] == null) continue;

                else if (emprestimos[i].Id == idEmprestimo)
                {
                    emprestimos[i] = null;

                    return true;
                }
            }

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

        public Emprestimo SelecionarEmprestimoPorId(int idEmprestimo)
        {
            for (int i = 0; i < emprestimos.Length; i++)
            {
                Emprestimo e = emprestimos[i];

                if (e == null)
                    continue;

                else if (e.Id == idEmprestimo)
                    return e;
            }

            return null;
        }

        public Emprestimo[] SelecionarEmprestimos()
        {
            return emprestimos;
        }
    }
}
