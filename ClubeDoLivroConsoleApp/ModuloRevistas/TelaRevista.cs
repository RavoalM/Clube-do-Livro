using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.Utils;

namespace ClubeDoLivroConsoleApp.ModuloRevistas
{
    public class TelaRevista : TelaBase
    {
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista repositorioRevista;
        public RepositorioAmigo repositorioAmigo;

        public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, RepositorioAmigo repositorioAmigo) : base("Revista", repositorioRevista)
        {
            this.repositorioCaixa = repositorioCaixa;
            this.repositorioRevista = repositorioRevista;
            this.repositorioAmigo = repositorioAmigo;
        }

        public override char ApresentarMenu()
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
            char opcaoEscolhida = Convert.ToChar(Console.ReadLine()!);

            return opcaoEscolhida;
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Revistas...");
            Console.WriteLine("--------------------------------------------");

            Revista novaRevista = (Revista)ObterDados();

            string erros = novaRevista.Validar();

            if (repositorioRevista.VerificarIndenfidicacaoRevista(novaRevista))
            {
                Notificador.ExibirMensagem("Esta titulo já pertence a outra revista.", ConsoleColor.Red);
                CadastrarRegistro();
                return;
            }

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                CadastrarRegistro();
                return;
            }

            novaRevista.Caixa.AdicionarRevista(novaRevista);
            repositorioRevista.CadastrarRegistro(novaRevista);

            Console.WriteLine();
            Notificador.ExibirMensagem("A revista foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public override void EditarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Editando Revista...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Revista[] revistasCadastradas = new Revista[registros.Length];

            if (!revistasCadastradas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há revistas cadastradas para edição.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRegistros(false);

            Console.Write("Digite o ID da revista que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Revista revistaOriginal = (Revista)repositorioRevista.SelecionarRegistroPorId(idSelecionado);
            Caixa caixaAntiga = revistaOriginal.Caixa;

            Console.WriteLine();

            Revista revistaEditada = (Revista)ObterDados();

            Caixa caixaEditada = revistaEditada.Caixa;

            bool conseguiuEditar = repositorioRevista.EditarRegistro(idSelecionado, revistaEditada);

            if (caixaAntiga != caixaEditada)
            {
                caixaAntiga.RemoverRevista(revistaOriginal);
                caixaEditada.AdicionarRevista(revistaOriginal);
            }

            Console.WriteLine();
            Notificador.ExibirMensagem("A revista foi editada com sucesso!", ConsoleColor.Green);
        }

        public override void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Excluindo revista...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Revista[] revistasCadastradas = new Revista[registros.Length];

            if (!revistasCadastradas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há revistas cadastradas para exclusão.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRegistros(false);

            Console.Write("Digite o ID da revista que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idSelecionado);
            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idSelecionado);

            if (repositorioAmigo.VerificarEmprestimosAmigo(amigoSelecionado))
            {
                Notificador.ExibirMensagem("A revista ainda está em um empréstimos em aberto e não pode ser excluída.", ConsoleColor.Red);
                return;
            }

            if (repositorioRevista.VerificarRevistaReservada(revistaSelecionada))
            {
                Notificador.ExibirMensagem("A revista ainda está reservada e não pode ser excluída.", ConsoleColor.Red);
                return;
            }

            bool conseguiuExcluir = repositorioRevista.ExcluirRegistro(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem("A revista foi excluída com sucesso!", ConsoleColor.Green);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
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

            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Revista[] revistasCadastradas = new Revista[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                revistasCadastradas[i] = (Revista)registros[i];
            }

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

        public void VisualizarCaixas()
        {
            Console.WriteLine("Visualizando Caixas...");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                "Id", "Etiqueta", "Cor", "Dias De Emprestimo"
            );
            EntidadeBase[] registros = repositorioCaixa.SelecionarRegistros();
            Caixa[] caixasCadastradas = new Caixa[registros.Length];
            for (int i = 0; i < registros.Length; i++)
            {
                caixasCadastradas[i] = (Caixa)registros[i];
            }
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

        public override EntidadeBase ObterDados()
        {
            Console.Write("Digite o titulo da Revista: ");
            string titulo = Console.ReadLine()!.Trim();

            Console.Write("Digite o numero da edição da revista: ");
            string numeroEdicao = Console.ReadLine()!.Trim();

            Console.Write("Digite o ano de publicação da revista: ");
            int AnoPublicacao = Convert.ToInt32(Console.ReadLine()!.Trim());

            EntidadeBase[] registros = repositorioCaixa.SelecionarRegistros();
            Caixa[] caixasCadastradas = new Caixa[registros.Length];

            VisualizarCaixas();

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

            Revista novaRevista = new Revista(titulo, numeroEdicao, AnoPublicacao, caixaSelecionada);

            return novaRevista;
        }
    }

}






