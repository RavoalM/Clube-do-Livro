﻿using ClubeDoLivroConsoleApp.Gerais;
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
