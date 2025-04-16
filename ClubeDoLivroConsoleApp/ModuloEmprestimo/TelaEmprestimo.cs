using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo
    {
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;
        public RepositorioEmprestimo repositorioEmprestimo;
        public RepositorioCaixa repositorioCaixa;

        public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo, RepositorioCaixa repositorioCaixa)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }

        public char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine("1 - Cadastro de Empréstimo");
            Console.WriteLine("2 - Edição de Empréstimo");
            Console.WriteLine("3 - Exclusão de Empréstimo");
            Console.WriteLine("4 - Visualização de Empréstimo");
            Console.WriteLine("5 - Registrar Devolução");
            Console.WriteLine("S - Voltar");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Digite um opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }

        public void CadastrarEmprestimo()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Empréstimo...");
            Console.WriteLine("--------------------------------------------");

            Emprestimo novoEmprestimo = ObterDadosEmprestimo();

            if (repositorioEmprestimo.VerificarEmprestimosAmigo(novoEmprestimo.Amigo))
            {
                Notificador.ExibirMensagem("Este membro já possui um empréstimo em aberto!", ConsoleColor.Red);
                return;
            }

            if (novoEmprestimo.Revista.StatusEmprestimo == "Emprestada")
            {
                Notificador.ExibirMensagem("Esta revista já está emprestada a outro membro!", ConsoleColor.Red);
                return;
            }

            string erros = novoEmprestimo.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                CadastrarEmprestimo();
                return;
            }

            novoEmprestimo.Revista.Emprestar();
            repositorioEmprestimo.CadastrarEmprestimo(novoEmprestimo);

            Console.WriteLine();
            Notificador.ExibirMensagem("O empréstimo foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public void EditarEmprestimo()
        {
            ExibirCabecalho();

            Console.WriteLine("Editando Empréstimo...");
            Console.WriteLine("--------------------------------------------");

            Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarEmprestimos();

            if (!emprestimos.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há empréstimos cadastrados para edição.", ConsoleColor.Yellow);
                return;
            }

            VisualizarEmprestimos(false);

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Emprestimo emprestimoEditado = ObterDadosEmprestimo();

            string erros = emprestimoEditado.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);

                CadastrarEmprestimo();
                return;
            }

            bool conseguiuEditar = repositorioEmprestimo.EditarEmprestimo(idSelecionado, emprestimoEditado);

            Console.WriteLine();
            Notificador.ExibirMensagem("O emprestimo foi editado com sucesso!", ConsoleColor.Green);
        }

        public void ExcluirEmprestimo()
        {
            ExibirCabecalho();

            Console.WriteLine("Excluindo Emprestimos...");
            Console.WriteLine("--------------------------------------------");

            Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarEmprestimos();

            if (!emprestimos.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há empréstimos cadastrados para exclusão.", ConsoleColor.Yellow);
                return;
            }

            VisualizarEmprestimos(false);

            Console.Write("Digite o ID do emprestimo que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = repositorioAmigo.SelecionarAmigoPorId(idSelecionado);

            if (repositorioAmigo.VerificarEmprestimosAmigo(amigoSelecionado))
            {
                Notificador.ExibirMensagem("O empréstimo não pode ser exclúido pois ainda está aberto.", ConsoleColor.Red);
                return;
            }

            bool conseguiuExcluir = repositorioEmprestimo.ExcluirEmprestimo(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem("O emprestimo foi excluído com sucesso!", ConsoleColor.Green);
        }

        public void VisualizarEmprestimos(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ExibirCabecalho();

                Console.WriteLine("Visualizando Empréstimos...");
                Console.WriteLine("--------------------------------------------");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -25} | {5, -20}",
                "Id", "Amigo", "Revista", "Data de Empréstimo", "Data de Devolução", "Situação"
            );

            Emprestimo[] emprestimosCadastrados = repositorioEmprestimo.SelecionarEmprestimos();

            for (int i = 0; i < emprestimosCadastrados.Length; i++)
            {
                Emprestimo e = emprestimosCadastrados[i];

                if (e == null) continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -25} | {5, -20}",
                     e.Id, e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo.ToShortDateString(), e.ObterDataDevolucao().ToShortDateString(), e.Situacao
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }


        public void RegistrarDevolucao()
        {
            ExibirCabecalho();

            Console.WriteLine("Devolução Empréstimo...");
            Console.WriteLine("--------------------------------------------");

            Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarEmprestimos();

            if (!emprestimos.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há empréstimos cadastrados para haver devoluções.", ConsoleColor.Yellow);
                return;
            }

            VisualizarEmprestimos(false);

            Console.Write("Selecione o ID de um Empréstimo: ");
            int idDevolucao = Convert.ToInt32(Console.ReadLine()!.Trim());

            Emprestimo emprestimoEscolhido = repositorioEmprestimo.SelecionarEmprestimoPorId(idDevolucao);

            emprestimoEscolhido.RegistrarDevolucao();

            Notificador.ExibirMensagem("Devolução feita com sucesso!", ConsoleColor.Green);
        }

        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Controle de Empréstimos");
            Console.WriteLine("--------------------------------------------");
        }

        public void VisualizarAmigos()
        {
            Console.WriteLine("Visualizando Membros...");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                "Id", "Nome", "Responsavel", "Telefone"
            );
            Amigo[] amigosCadastrados = repositorioAmigo.SelecionarAmigos();
            for (int i = 0; i < amigosCadastrados.Length; i++)
            {
                Amigo a = amigosCadastrados[i];
                if (a == null) continue;
                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                    a.Id, a.Nome, a.Responsavel, a.Telefone
                );
            }
            Console.WriteLine();
        }

        public bool VisualizarRevistasNaCaixa()
        {
            bool conseguiuSelecionar = true;
            Console.WriteLine();

            VisualizarCaixas();

            Console.Write("Digite o ID da caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine()!.Trim());

            Caixa caixaSelecionada = repositorioCaixa.SelecionarCaixaPorId(idCaixa);

            if (caixaSelecionada == null)
            {
                Notificador.ExibirMensagem("Id da caixa selecionada não existe", ConsoleColor.Red);
                return conseguiuSelecionar = false;
            }

            Console.WriteLine();
            Console.WriteLine("Visualizando Revistas...");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15} | {4, -25}",
                "Id", "Titulo", "Numero de edição", "Ano de publicação", "Status de empréstimo"
            );

            Revista[] revistasCadastradas = caixaSelecionada.ObterRevistas();

            for (int i = 0; i < revistasCadastradas.Length; i++)
            {
                Revista r = revistasCadastradas[i];
                if (r == null) continue;
                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -15} | {4, -25}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.StatusEmprestimo
                );
            }
            Console.WriteLine();

            return conseguiuSelecionar;
        }

        public void VisualizarCaixas()
        {
            Console.WriteLine();
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

        public Emprestimo ObterDadosEmprestimo()
        {

            VisualizarAmigos();

            Console.Write("Digite o ID do membro que realizou o empréstimo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine()!.Trim());

            bool conseguiuSelecionar = false;
            conseguiuSelecionar = VisualizarRevistasNaCaixa();

            while (!conseguiuSelecionar)
            {
                CadastrarEmprestimo();
            }

            Console.Write("Digite o ID da revista que realizou o empréstimo: ");
            int idRevista = Convert.ToInt32(Console.ReadLine()!.Trim());
            Revista revistaSelecionada = repositorioRevista.SelecionarRevistaPorId(idRevista);
            Amigo amigoSelecionado = repositorioAmigo.SelecionarAmigoPorId(idAmigo);

            Emprestimo novaEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);
            return novaEmprestimo;
        }
    }
}
