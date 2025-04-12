using ClubeDoLivroConsoleApp.Gerais;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo
    {
        public Emprestimo[] emprestimos = new Emprestimo[100];
        public int contadorEmprestimos = 0;

        public void CadastrarEmprestimo(Emprestimo novoEmprestimo)
        {
            novoEmprestimo.Id = GeradorIds.GerarIdEmprestimo();

            emprestimos[contadorEmprestimos++] = novoEmprestimo;
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
