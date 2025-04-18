using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.ModuloReservas
{
    public class TelaReserva
    {
        public RepositorioReserva repositorioReserva;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;
        public RepositorioEmprestimo repositorioEmprestimo;
        public RepositorioCaixa repositorioCaixa;

        public TelaReserva(RepositorioReserva repositorioReserva, RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo, RepositorioCaixa repositorioCaixa)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }

        public char ApresentarMenu()
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

        public void CadastrarReserva()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Reserva...");
            Console.WriteLine("--------------------------------------------");

            Reserva novaReserva = ObterDadosReserva();

            string erros = novaReserva.Validar();


            if (novaReserva.Revista.StatusEmprestimo == "Emprestada")
            {
                Notificador.ExibirMensagem("Esta revista já está emprestada a outro membro!", ConsoleColor.Red);
                return;
            }

            if (erros.Length > 0)
            { 
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                CadastrarReserva();
                return;
            }

            repositorioReserva.CadastrarReserva(novaReserva);

            Console.WriteLine();
            Notificador.ExibirMensagem("A reserva foi cadastrada com sucesso!", ConsoleColor.Green);
        }

        public void CancelarReserva()
        {
            ExibirCabecalho();

            Console.WriteLine("Cancelando Caixa...");
            Console.WriteLine("--------------------------------------------");

            Reserva[] reservas = repositorioReserva.SelecionarReservas();

            if (!reservas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há reservas cadastradas para cancelar.", ConsoleColor.Yellow);
                return;
            }

            VisualizarReservas(false);

            Console.Write("Digite o ID da reserva que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Reserva reservaSelecionada = repositorioReserva.SelecionarReservaPorId(idSelecionado);

            bool conseguiuExcluir = repositorioReserva.CancelarReserva(reservaSelecionada);

            Console.WriteLine();
            Notificador.ExibirMensagem("A reserva foi cancelada com sucesso!", ConsoleColor.Green);
        }

        public void EmprestarRevistaReservada()
        {
            ExibirCabecalho();

            Console.WriteLine("Emprestando Revista Reservada...");
            Console.WriteLine("--------------------------------------------");

            Reserva[] reservas = repositorioReserva.SelecionarReservas();

            if (!reservas.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há reservas cadastradas para cancelar.", ConsoleColor.Yellow);
                return;
            }

            VisualizarReservas(false);

            Console.Write("Digite o ID da reserva que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Reserva reservaSelecionada = repositorioReserva.SelecionarReservaPorId(idSelecionado);

            reservaSelecionada.Concluir();
            repositorioEmprestimo.CadastrarEmprestimo(new Emprestimo(reservaSelecionada.Amigo, reservaSelecionada.Revista));

            Notificador.ExibirMensagem("\nRevista reservada emprestada com sucesso!", ConsoleColor.Green);
        }


        public void VisualizarReservas(bool exibirTitulo)
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

            Reserva[] reservasCadastradas = repositorioReserva.SelecionarReservas();

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

        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Gestão de Reservas");
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

        public Reserva ObterDadosReserva()
        {
            VisualizarAmigos();

            Console.Write("Digite o ID do membro que realizou o empréstimo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine()!.Trim());

            bool conseguiuSelecionar = false;
            conseguiuSelecionar = VisualizarRevistasNaCaixa();

            while (!conseguiuSelecionar)
            {
                CadastrarReserva();
            }

            Console.Write("Digite o ID da revista que realizou o empréstimo: ");
            int idRevista = Convert.ToInt32(Console.ReadLine()!.Trim());

            Revista revistaSelecionada = repositorioRevista.SelecionarRevistaPorId(idRevista);
            Amigo amigoSelecionado = repositorioAmigo.SelecionarAmigoPorId(idAmigo);

            Reserva novaReserva = new Reserva(amigoSelecionado, revistaSelecionada);
            return novaReserva;
        }
    }
}
