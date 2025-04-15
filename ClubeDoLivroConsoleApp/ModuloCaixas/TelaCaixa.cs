using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloCaixas
{
    public class TelaCaixa
    {
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista repositorioRevista;

        public TelaCaixa(RepositorioCaixa repositorioCaixa, RepositorioRevista repositorioRevista)
        {
            this.repositorioCaixa = repositorioCaixa;
            this.repositorioRevista = repositorioRevista;
        }

        public char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine("1 - Cadastro de Caixa");
            Console.WriteLine("2 - Edição de Caixa");
            Console.WriteLine("3 - Exclusão de Caixa");
            Console.WriteLine("4 - Visualização de Caixa");
            Console.WriteLine("5 - Visualização de Revistas na Caixa");
            Console.WriteLine("S - Voltar");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Digite um opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }

        public void CadastrarCaixa()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Caixa...");
            Console.WriteLine("--------------------------------------------");

            Caixa novaCaixa = ObterDadosCaixa();

            repositorioCaixa.CadastrarCaixa(novaCaixa);

            Console.WriteLine();
            Notificador.ExibirMensagem("A caixa foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public void EditarCaixa()
        {
            ExibirCabecalho();

            Console.WriteLine("Editando Caixa...");
            Console.WriteLine("--------------------------------------------");

            VisualizarCaixas(false);

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Caixa caixaEditada = ObterDadosCaixa();
        

            bool conseguiuEditar = repositorioCaixa.EditarCaixa(idSelecionado, caixaEditada);

            Console.WriteLine();
            Notificador.ExibirMensagem("A caixa foi editada com sucesso!", ConsoleColor.Green);
        }

        public void ExcluirCaixa()
        {
            ExibirCabecalho();

            Console.WriteLine("Excluindo Caixa...");
            Console.WriteLine("--------------------------------------------");

            VisualizarCaixas(false);

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = repositorioCaixa.ExcluirCaixa(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem("A caixa foi excluída com sucesso!", ConsoleColor.Green);
        }

        public void VisualizarCaixas(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ExibirCabecalho();

                Console.WriteLine("Visualizando Caixas...");
                Console.WriteLine("--------------------------------------------");
            }

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

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        public void VisualizarRevistasNaCaixa()
        {
            VisualizarCaixas(false);

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa caixaSelecionada = repositorioCaixa.SelecionarCaixaPorId(idCaixa);

            Console.WriteLine();
            Console.WriteLine("Visualizando Revistas...");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15} | {4, -20}",
                "Id", "Titulo", "Numero de edição", "Ano de publicação", "Status de empréstimo"
            );

            Revista[] revistasCadastradas = caixaSelecionada.ObterRevistas();

            for (int i = 0; i < revistasCadastradas.Length; i++)
            {
                Revista r = revistasCadastradas[i];
                if (r == null) continue;
                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -15} | {4, -20}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.StatusEmprestimo
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

        public Caixa ObterDadosCaixa()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine()!.Trim();

            Console.Write("Digite o nome ou o valor da cor: ");
            string cor = Console.ReadLine()!.Trim();

            Console.Write("Digite o dia limite de emprestimo da caixa: ");
            int diasDeEmprestimo = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa novaCaixa = new Caixa(etiqueta, cor, diasDeEmprestimo);

            return novaCaixa;
        }
    }
}
