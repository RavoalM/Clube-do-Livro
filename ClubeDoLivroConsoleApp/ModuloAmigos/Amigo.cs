using ClubeDoLivroConsoleApp.ModuloEmprestimo;

namespace ClubeDoLivroConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public int Id;
        public string Nome;
        public string Responsavel;
        public string Telefone;
        public Emprestimo Emprestimo;

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

            if (!Telefone.All(c => char.IsDigit(c) || c == ' ' || c == '-'))
            {
                erros += "O campo 'Telefone' deve conter apenas números."; 
            }

           
            return erros;
        }

    }
}
