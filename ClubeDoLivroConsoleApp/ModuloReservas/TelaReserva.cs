using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;
using ClubeDoLivroConsoleApp.ModuloRevistas;
using ClubeDoLivroConsoleApp.Utils;

namespace ClubeDoLivroConsoleApp.ModuloReservas
{
    public class TelaReserva : TelaBase
    {
        public RepositorioReserva repositorioReserva;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;
        public RepositorioEmprestimo repositorioEmprestimo;
        public RepositorioCaixa repositorioCaixa;

        public TelaReserva(RepositorioReserva repositorioReserva, RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo, RepositorioCaixa repositorioCaixa) : base("Reserva", repositorioReserva)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }

        public override char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine("1 - Cadastro de Reservas");
            Console.WriteLine("2 - Cancelamento de Reservas");
            Console.WriteLine("3 - Emprestar revista Reservada");
            Console.WriteLine("4 - Visualização de Reservas");
            Console.WriteLine("S - Voltar");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Digite um opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Reserva...");
            Console.WriteLine("--------------------------------------------");

            Reserva novaReserva = ObterDados();

            string erros = novaReserva.Validar();


            if (novaReserva.Revista.StatusEmprestimo == "Emprestada")
            {
                Notificador.ExibirMensagem("Esta revista já está emprestada a outro membro!", ConsoleColor.Red);
                return;
            }

            if (erros.Length > 0)
            { 
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                CadastrarRegistro();
                return;
            }

            repositorioReserva.CadastrarRegistro(novaReserva);

            Console.WriteLine();
            Notificador.ExibirMensagem("A reserva foi cadastrada com sucesso!", ConsoleColor.Green);
        }

        public override void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Cancelando Caixa...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Reserva[] reservasCadastradas = new Reserva[registros.Length];

            if (!reservasCadastradas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há reservas cadastradas para cancelar.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRegistros(false);

            Console.Write("Digite o ID da reserva que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Reserva reservaSelecionada = (Reserva)repositorioReserva.SelecionarRegistroPorId(idSelecionado);

            bool conseguiuExcluir = repositorioReserva.ExcluirRegistro(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem("A reserva foi cancelada com sucesso!", ConsoleColor.Green);
        }

        public void EmprestarRevistaReservada()
        {
            ExibirCabecalho();

            Console.WriteLine("Emprestando Revista Reservada...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Reserva[] reservasCadastradas = new Reserva[registros.Length];

            if (!reservasCadastradas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há reservas cadastradas para cancelar.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRegistros(false);

            Console.Write("Digite o ID da reserva que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Reserva reservaSelecionada = (Reserva)repositorioReserva.SelecionarRegistroPorId(idSelecionado);

            reservaSelecionada.Concluir();
            repositorioEmprestimo.CadastrarRegistro(new Emprestimo(reservaSelecionada.Amigo, reservaSelecionada.Revista));

            Notificador.ExibirMensagem("\nRevista reservada emprestada com sucesso!", ConsoleColor.Green);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ExibirCabecalho();

                Console.WriteLine("Visualizando Reservas...");
                Console.WriteLine("--------------------------------------------");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -25} | {5, -20}",
                "Id", "Amigo", "Revista", "Dias De Reserva", "Validade da reserva","Status de Reserva"
            );

            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Reserva[] reservasCadastradas = new Reserva[registros.Length];

            for (int i = 0; i < reservasCadastradas.Length; i++)
            {
                Reserva r = reservasCadastradas[i];  

                if (r == null) continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -25} | {5, -20}", 
                    r.Id, r.Amigo.Nome, r.Revista.Titulo, r.DataReserva.ToShortDateString(), r.ObterDataValidade().ToShortDateString(), r.Status
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
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
            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Amigo[] amigosCadastrados = new Amigo[registros.Length];
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

            Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

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
            EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
            Caixa[] caixasCadastradas = new Caixa[registros.Length];
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

        public override Reserva ObterDados()
        {
            VisualizarAmigos();

            Console.Write("Digite o ID do membro que realizou o empréstimo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine()!.Trim());

            bool conseguiuSelecionar = false;
            conseguiuSelecionar = VisualizarRevistasNaCaixa();

            while (!conseguiuSelecionar)
            {
                CadastrarRegistro();
            }

            Console.Write("Digite o ID da revista que realizou o empréstimo: ");
            int idRevista = Convert.ToInt32(Console.ReadLine()!.Trim());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);
            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

            Reserva novaReserva = new Reserva(amigoSelecionado, revistaSelecionada);
            return novaReserva;
        }
    }
}
