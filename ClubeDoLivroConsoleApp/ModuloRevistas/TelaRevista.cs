using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class TelaRevista
    {
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista repositorioRevista;
        public RepositorioAmigo repositorioAmigo;

        public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, RepositorioAmigo repositorioAmigo)
        {
            this.repositorioCaixa = repositorioCaixa;
            this.repositorioRevista = repositorioRevista;
            this.repositorioAmigo = repositorioAmigo;
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

            string erros = novaRevista.Validar();

            if (repositorioRevista.VerificarIndenfidicacaoRevista(novaRevista))
            {
                Notificador.ExibirMensagem("Esta titulo já pertence a outra revista.", ConsoleColor.Red);
                CadastraRevista();
                return;
            }

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                CadastraRevista();
                return;
            }

            repositorioRevista.CadastrarRevista(novaRevista);

            Console.WriteLine();
            Notificador.ExibirMensagem("A revista foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public void EditarRevista()
        {
            ExibirCabecalho();

            Console.WriteLine("Editando Revista...");
            Console.WriteLine("--------------------------------------------");

            Revista[] revistas = repositorioRevista.SelecionarRevistas();

            if (!revistas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há revistas cadastradas para edição.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRevistas(false);

            Console.Write("Digite o ID da revista que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Revista revistaOriginal = repositorioRevista.SelecionarRevistaPorId(idSelecionado);
            Caixa caixaAntiga = revistaOriginal.Caixa;

            Console.WriteLine();

            Revista revistaEditada = ObterDadosRevista();

            Caixa caixaEditada = revistaEditada.Caixa;

            bool conseguiuEditar = repositorioRevista.EditarRevista(idSelecionado, revistaEditada);

            if (caixaAntiga != caixaEditada)
            {
                caixaAntiga.RemoverRevista(revistaOriginal);
                caixaEditada.AdicionarRevista(revistaOriginal);
            }

            Console.WriteLine();
            Notificador.ExibirMensagem("A revista foi editada com sucesso!", ConsoleColor.Green);
        }

        public void ExcluirRevista()
        {
            ExibirCabecalho();

            Console.WriteLine("Excluindo revista...");
            Console.WriteLine("--------------------------------------------");

            Revista[] revistas = repositorioRevista.SelecionarRevistas();

            if (!revistas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há revistas cadastradas para exclusão.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRevistas(false);

            Console.Write("Digite o ID da revista que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = repositorioAmigo.SelecionarAmigoPorId(idSelecionado);
            Revista revistaSelecionada = repositorioRevista.SelecionarRevistaPorId(idSelecionado);

            if (repositorioAmigo.VerificarEmprestimosAmigo(amigoSelecionado))
            {
                Notificador.ExibirMensagem("A revista ainda está em um empréstimos em aberto e não pode ser excluída.", ConsoleColor.Red);
                return;
            }

            if (repositorioRevista.VerificarRevistaReservada(revistaSelecionada))
            {
                Notificador.ExibirMensagem("A revista ainda está em um empréstimos em aberto e não pode ser excluída.", ConsoleColor.Red);
                return;
            }

            bool conseguiuExcluir = repositorioRevista.ExcluirRevista(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem("A revista foi excluída com sucesso!", ConsoleColor.Green);
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
                "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -25} | {5, -20}",
                "Id", "Titulo", "Numero da Edicao", "Ano de Publicacao", "Status Emprestimo", "Caixa"
            );

            Revista[] revistasCadastradas = repositorioRevista.SelecionarRevistas();

            for (int i = 0; i < revistasCadastradas.Length; i++)
            {
                Revista r = revistasCadastradas[i];

                if (r == null) continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -25} | {5, -20}",
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
            Console.WriteLine("Controle de Revistas");
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
            string numeroEdicao = Console.ReadLine()!.Trim();

            Console.Write("Digite o ano de publicação da revista: ");
            int AnoPublicacao = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa[] caixas = repositorioCaixa.SelecionarCaixas();

            VisualizarCaixas();

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa caixaSelecionada = repositorioCaixa.SelecionarCaixaPorId(idCaixa);

            Revista novaRevista = new Revista(titulo, numeroEdicao, AnoPublicacao, caixaSelecionada);

            return novaRevista;
        }
    }

}






