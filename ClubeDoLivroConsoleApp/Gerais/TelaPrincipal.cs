using System;

namespace ClubeDoLivroConsoleApp.Gerais
{
    public class TelaPrincipal
    {
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
            Console.WriteLine(" |        SEJA BEM VINDO AO CLUBE DO LIVRO!!!     |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine(" |                        ||                      |");
            Console.WriteLine("  \\                       /\\                     / ");
            Console.WriteLine("    --------------------     -------------------   ");

            Thread.Sleep(7000);
            Console.ResetColor();
        }

        public char ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|           Clube do Livro             |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Controle de Membros");
            Console.WriteLine("S - Sair");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }
    }
}
