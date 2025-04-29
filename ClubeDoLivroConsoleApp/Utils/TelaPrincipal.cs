using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;
using ClubeDoLivroConsoleApp.ModuloReservas;
using ClubeDoLivroConsoleApp.ModuloRevistas;

namespace ClubeDoLivroConsoleApp.Utils
{
    public class TelaPrincipal
    {
        private char opcaoPrincipal;

        private RepositorioAmigo repositorioAmigo;
        private RepositorioCaixa repositorioCaixa;
        private RepositorioRevista repositorioRevista;
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioReserva repositorioReserva;

        public TelaPrincipal()
        {
            this.repositorioAmigo = new RepositorioAmigo();
            this.repositorioCaixa = new RepositorioCaixa();
            this.repositorioRevista = new RepositorioRevista();
            this.repositorioEmprestimo = new RepositorioEmprestimo();
            this.repositorioReserva = new RepositorioReserva();
        }

        public static void Intruducao()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   ____   ___  _  __ ___     ____   ___  _  __ ___ ");
            Console.WriteLine("  |  _ \\ / _ \\| |/ /|_ _|   |  _ \\ / _ \\| |/ /|_ _|");
            Console.WriteLine("  | | | | | | | ' /  | |    | | | | | | | ' /  | | ");
            Console.WriteLine("  | |_| | |_| | . \\  | |    | |_| | |_| | . \\  | | ");
            Console.WriteLine("  |____/ \\___/|_|\\_\\|___|   |____/ \\___/|_|\\_\\|___|");


            Console.WriteLine("    --------------------     -------------------   ");
            Console.WriteLine("  /                       \\/                     \\ ");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |       SEJA BEM VINDO AO CLUBE DO LIVRO!!!      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine("  \\                       /\\                     / ");
            Console.WriteLine("    --------------------     -------------------   ");

            Thread.Sleep(7000);
            Console.ResetColor();
        }

        public void ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|           Clube do Livro             |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Controle de Membros");
            Console.WriteLine("2 - Controle de Caixas");
            Console.WriteLine("3 - Controle de Revistas");
            Console.WriteLine("4 - Controle de Empréstimos");
            Console.WriteLine("5 - Controle de Reservas");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            opcaoPrincipal = Console.ReadLine()[0];
        }

        public ITelaCrud ObterTela()
        {
            if (opcaoPrincipal == '1')
            {
                return new TelaAmigo(repositorioAmigo, repositorioEmprestimo, repositorioRevista);
            }

            else if (opcaoPrincipal == '2')
            {
                return new TelaCaixa(repositorioCaixa, repositorioRevista);
            }

            else if (opcaoPrincipal == '3')
            {
                return new TelaRevista(repositorioRevista, repositorioCaixa, repositorioAmigo);
            }

            else if (opcaoPrincipal == '4')
            {
                return new TelaEmprestimo(repositorioEmprestimo, repositorioRevista, repositorioAmigo, repositorioCaixa);
            }

            else if (opcaoPrincipal == '5')
            {
                return new TelaReserva(repositorioReserva, repositorioEmprestimo, repositorioRevista, repositorioAmigo, repositorioCaixa);
            }
            else
            {
                Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red);
                return null;
            }
        }

    }
}
