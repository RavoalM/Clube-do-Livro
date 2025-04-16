using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloCaixas
{
    public class Caixa
    {
        public int Id;
        public string Etiqueta;
        public string Cor;
        public int DiasDeEmprestimo;
        public Revista[] Revistas = new Revista[40];

        public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
        }

        public string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(Etiqueta))
            {
                erros += "O campo 'Etiqueta' é obrigatório.\n";
            }

            if (Etiqueta.Length > 50)
            {
                erros += "O campo 'Etiqueta' não pode ter mais que 50 caracteres.\n";
            }

            if (string.IsNullOrWhiteSpace(Cor))
            {
                erros += "O campo 'Cor' é obrigatório.\n";
            }

            if (string.IsNullOrWhiteSpace(DiasDeEmprestimo.ToString()))
            {
                erros += "O campo 'DiasDeEmprestimo' é obrigatório.\n";
            }

            if (DiasDeEmprestimo != 3 && DiasDeEmprestimo != 7)
            {
                erros += "O campo 'Dias de Emprestimo' está inválido! Verifique novamente a raridade da caixa!\n";
            }

            return erros;
        }

        public void AdicionarRevista(Revista revista)
        {
            for (int i = 0; i < Revistas.Length; i++)
            {
                if (Revistas[i] == null)
                {
                    Revistas[i] = revista;
                    return;
                }
            }
        }

        public void RemoverRevista(Revista revista)
        {
            for (int i = 0; i < Revistas.Length; i++)
            {
                if (Revistas[i] == null) continue;

                else if (Revistas[i] == revista)
                {
                    Revistas[i] = null;
                    return;
                }
            }
        }

        public Revista[] ObterRevistas()
        {
            return Revistas;
        }
    }
}
