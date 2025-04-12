using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class TelaRevista
    {
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista repositorioRevista;

        public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
            this.repositorioRevista = repositorioRevista;
        }

        public char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine("1 - Cadastro de Revista");
            Console.WriteLine("2 - Edição de Revista");
            Console.WriteLine("3 - Exclusão de Revista");
            Console.WriteLine("4 - Visualização de Revistas");
            Console.WriteLine("S - Voltar");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Digite um opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }

        public void CadastraRevista()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Revistas...");
            Console.WriteLine("--------------------------------------------");

            Revista novaRevista = ObterDadosRevista();

            repositorioRevista.CadastrarRevista(novaRevista);

            Console.WriteLine();
            Notificador.ExibirMensagem("A caixa foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public void VisualizarRevistas(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ExibirCabecalho();

                Console.WriteLine("Visualizando Revistas...");
                Console.WriteLine("--------------------------------------------");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -20} | {5, -20}",
                "Id", "Titulo", "Numero da Edicao", "Ano de Publicacao", "Status Emprestimo", "Caixa"
            );

            Revista[] revistasCadastradas = repositorioRevista.SelecionarRevistas();

            for (int i = 0; i < revistasCadastradas.Length; i++)
            {
                Revista r = revistasCadastradas[i];

                if (r == null) continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -20} | {5, -20}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.StatusEmprestimo, r.Caixa.Etiqueta
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Gestão de Caixas");
            Console.WriteLine("--------------------------------------------");
        }

        public void VisualizarCaixas()
        {
            Console.WriteLine("Visualizando Caixas...");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                "Id", "Etiqueta", "Cor", "Dias De Emprestimo"
            );

            Caixa[] caixasCadastradas = repositorioCaixa.SelecionarCaixas();

            for (int i = 0; i < caixasCadastradas.Length; i++)
            {
                Caixa c = caixasCadastradas[i];

                if (c == null) continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                    c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
                );
            }

            Console.WriteLine();
        }

        public Revista ObterDadosRevista()
        {
            Console.Write("Digite o titulo da Revista: ");
            string titulo = Console.ReadLine()!.Trim();

            Console.Write("Digite o numero da edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine()!.Trim());

            Console.Write("Digite o ano de publicação da revista: ");
            int AnoPublicacao = Convert.ToInt32(Console.ReadLine()!.Trim());

            Console.Write("Digite o status de emprestimo da revista: ");
            string statusEmprestimo = Console.ReadLine()!.Trim();

            VisualizarCaixas();

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa caixaSelecionada = repositorioCaixa.SelecionarCaixaPorId(idCaixa);

            Revista novaRevista = new Revista(titulo, numeroEdicao, AnoPublicacao, statusEmprestimo, caixaSelecionada);

            return novaRevista;
        }
    }

}






