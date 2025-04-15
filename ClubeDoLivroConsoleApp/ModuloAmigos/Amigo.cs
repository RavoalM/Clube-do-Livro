using ClubeDoLivroConsoleApp.ModuloEmprestimo;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloAmigos
{
    public class Amigo
    {
        public int Id;
        public string Nome;
        public string Responsavel;
        public string Telefone;
        public Emprestimo[] Emprestimos = new Emprestimo[100];

        public Amigo(string nome, string responsavel, string telefone)
        {
            Nome = nome;
            Responsavel = responsavel;
            Telefone = telefone;
        }

        public string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(Nome))
            {
                erros += "O campo 'Nome' é obrigatório.\n";
            }

            if (Nome.Length < 3)
            {
                erros += "O campo 'Nome' deve ter pelo menos 3 caracteres.\n";
            }

            if (!Nome.All(char.IsLetter))
            {
                erros += "O campo 'Nome' deve conter apenas letras.\n";
            }

            if (string.IsNullOrWhiteSpace(Responsavel))
            {
                erros += "O campo 'Responsavel' é obrigatório.\n";
            }

            if (Responsavel.Length < 3)
            {
                erros += "O campo 'Responsavel' deve ter pelo menos 3 caracteres.\n";
            }

            if (!Responsavel.All(char.IsLetter))
            {
                erros += "O campo 'Responsavel' deve conter apenas letras.\n";
            }

            if (string.IsNullOrWhiteSpace(Telefone))
            {
                erros += "O campo 'Telefone' é obrigatório.\n";
            }

            if (Telefone.Length < 12)
            {
                erros += "O campo 'Telefone' deve seguir o formato 00 0000-0000.\n";
            }

            if (Telefone.Length < 12 || Telefone.Length > 13)
            {
                erros += "O campo 'Telefone' deve seguir os formatos 00 0000-0000 ou 00 00000-0000.\n";
            }

            if (Telefone.Length >= 3 && (!char.IsDigit(Telefone[0]) || !char.IsDigit(Telefone[1]) || Telefone[2] !=' '))
            {
                erros += "O campo 'Telefone' deve começar com dois números seguido de um espaço.\n";
            }

            if (Telefone.Length == 12 && Telefone[7] != '-')
            {
                erros += "O campo 'Telefone' está errado! Use os formatos (00 0000-0000 ou 00 00000-0000).\n";
            }

            if (Telefone.Length == 13 && Telefone[8] != '-')
            {
                erros += "O campo 'Telefone' está errado! Use os formatos (00 0000-0000 ou 00 00000-0000).\n";
            }

            if (!Telefone.All(c => char.IsDigit(c) || c == ' ' || c == '-'))
            {
                erros += "O campo 'Telefone' está errado! Use os formatos (00 0000-0000 ou 00 00000-0000).";
            }

            return erros;
        }

        public void AdicionarEmpréstimo(Emprestimo emprestimo)
        {
            for (int i = 0; i < Emprestimos.Length; i++)
            {
                if (Emprestimos[i] == null)
                {
                    Emprestimos[i] = emprestimo;
                    return;
                }
            }
        }

        public Emprestimo[] ObterEmprestimos()
        {
            return Emprestimos;
        }

    }
}
