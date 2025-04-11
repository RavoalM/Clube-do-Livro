using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigo;

namespace ClubeDoLivroConsoleApp.ModuloAmigos
{
    public class TelaAmigo
    {
        public RepositorioAmigo repositorioAmigo;

        public TelaAmigo(RepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = new RepositorioAmigo();
        }

        public char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine("1 - Cadastro de Amigo");
            Console.WriteLine("2 - Edição de Amigo");
            Console.WriteLine("3 - Exclusão de Amigo");
            Console.WriteLine("4 - Visualização de Amigo");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Digite um opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }

        public void CadastrarAmigo()
        {
            ExibirCabecalho();

            Console.WriteLine("Cadastrando Amigo...");
            Console.WriteLine("--------------------------------------------");

            Amigo novoAmigo = ObterDadosAmigo();

            string erros = novoAmigo.Validar();

            if (repositorioAmigo.TelefoneRepetido(novoAmigo.Telefone))
            {
                Notificador.ExibirMensagem("Este telefone já pertence a outro amigo.", ConsoleColor.Red);
                CadastrarAmigo();
                return;
            }

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);

                CadastrarAmigo();
                return;
            }

            repositorioAmigo.CadastrarAmigo(novoAmigo);

            Console.WriteLine();
            Notificador.ExibirMensagem("O membro foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public void EditarAmigo()
        {
            ExibirCabecalho();

            Console.WriteLine("Editando Membro...");
            Console.WriteLine("--------------------------------------------");

            VisualizarAmigos(false);

            Console.Write("Digite o ID do membro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo amigoEditado = ObterDadosAmigo();

            string erros = amigoEditado.Validar();

            if (repositorioAmigo.TelefoneRepetido(amigoEditado.Telefone, idSelecionado))
            {
                Notificador.ExibirMensagem("Este telefone já pertence a outro amigo.", ConsoleColor.Red);
                EditarAmigo();
                return;
            }

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);

                EditarAmigo();
                return;
            }

            bool conseguiuEditar = repositorioAmigo.EditarAmigo(idSelecionado, amigoEditado);

            Console.WriteLine();
            Notificador.ExibirMensagem("O membro foi editado com sucesso!", ConsoleColor.Green);
        }

        public void ExcluirAmigo()
        {
            ExibirCabecalho();

            Console.WriteLine("Excluindo Membro...");
            Console.WriteLine("--------------------------------------------");

            VisualizarAmigos(false);

            Console.Write("Digite o ID do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = repositorioAmigo.ExcluirAmigo(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem("O membro foi excluído com sucesso!", ConsoleColor.Green);
        }


        public void VisualizarAmigos(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ExibirCabecalho();

                Console.WriteLine("Visualizando Membros...");
                Console.WriteLine("--------------------------------------------");
            }

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

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }


        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Gestão de Amigos");
            Console.WriteLine("--------------------------------------------");
        }

        public Amigo ObterDadosAmigo()
        {
            Console.Write("Digite o nome do membro: ");
            string nome = Console.ReadLine()!.Trim();

            Console.Write("Digite o nome do responsavel: ");
            string responsavel = Console.ReadLine()!.Trim();

            Console.Write("Digite o telefone do amigo ou responsavel ");
            string telefone = Console.ReadLine()!.Trim();

            Amigo novoAmigo = new Amigo(nome, responsavel, telefone);

            return novoAmigo;
        }
    }
}
