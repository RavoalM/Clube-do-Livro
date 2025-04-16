using ClubeDoLivroConsoleApp.ModuloCaixas;
using System.Globalization;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class Revista
    {
        public int Id;
        public string Titulo;
        public string NumeroEdicao;
        public int AnoPublicacao;
        public string StatusEmprestimo;
        public Caixa Caixa;

        public Revista(string titulo, string numeroEdicao, int anoPublicao, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicao;
            StatusEmprestimo = "Disponivel";
            Caixa = caixa;
        }

        public string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(Titulo))
            {
                erros += "O campo 'Titulo' é obrigatório.\n";
            }

            if (Titulo.Length < 2)
            {
                erros += "O campo 'Titulo' precisa conter ao menos 2 caracteres\n";
            }

            if (Titulo.Length > 100)
            {
                erros += "O campo 'Titulo' não pode ter mais que 100 caracteres.\n";
            }

            if (string.IsNullOrWhiteSpace(NumeroEdicao))
            {
                erros += "O campo 'Número da Edição' é obrigatório.\n";
            }

            bool somenteNumeros = true;
            foreach (char c in NumeroEdicao)
            {
                if (!char.IsDigit(c))
                {
                    somenteNumeros = false;
                    break;
                }
            }

            if (NumeroEdicao.Length < 2 || !somenteNumeros)
            {
                erros += "O campo 'Número da Edição' precisa conter ao menos 2 caracteres e apenas números.\n";
            }

            if (string.IsNullOrWhiteSpace(AnoPublicacao.ToString()))
            {
                erros += "O campo 'Ano de Publicação' é obrigatório.\n";
            }

            if (!DateTime.TryParse($"01/01/{AnoPublicacao}", CultureInfo.InvariantCulture, out DateTime anoPublicacao))
            {
                erros += "O campo 'Ano de Publicação' está inválido! Insira somente o ano (yyyy).\n";
            }

            if (AnoPublicacao < 1000)
            {
                erros += "O campo 'Ano de Publicação' não pode ser anterior a 1000.\n";
            }
                

            if (anoPublicacao > DateTime.Now)
            {
                erros += "O campo 'Ano de Publicação' não pode ser um ano futurístico.\n";
            }

            return erros;
        }

        public void Emprestar()
        {
            StatusEmprestimo = "Emprestada";
        }
        public void Devolver()
        {
            StatusEmprestimo = "Disponível";
        }
        public void Reservar()
        {
            StatusEmprestimo = "Reservada";
        }
    }
}
