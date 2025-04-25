using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloRevistas;
using ClubeDoLivroConsoleApp.Utils;

namespace ClubeDoLivroConsoleApp.Gerais
{
    public abstract class TelaBase
    {
        protected string nomeEntidade;
        private RepositorioBase repositorio;

        protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
        {
            this.nomeEntidade = nomeEntidade;
            this.repositorio = repositorio;
        }

        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Gestão de {nomeEntidade}s");
            Console.WriteLine("--------------------------------------------");
        }

        public virtual char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine($"1 - Cadastro {nomeEntidade}");
            Console.WriteLine($"2 - Edição {nomeEntidade}");
            Console.WriteLine($"3 - Exclusão {nomeEntidade}");
            Console.WriteLine($"4 - Visualização {nomeEntidade}s");

            Console.WriteLine("S - Voltar");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Digite um opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }

        public virtual void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Cadastrando {nomeEntidade}...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase novoRegistro = ObterDados();

            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);

                CadastrarRegistro();
                return;
            }

            repositorio.CadastrarRegistro(novoRegistro);

            Console.WriteLine();
            Notificador.ExibirMensagem($"O {nomeEntidade} foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public virtual void EditarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Editando {nomeEntidade}...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase[] registros = repositorio.SelecionarRegistros();
            EntidadeBase[] registrosCadastrados = new EntidadeBase[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                registrosCadastrados[i] = registros[i];
            }

            if (!registros.Any(a => a != null))
            {
                Notificador.ExibirMensagem($"Não há {nomeEntidade}s cadastrados para edição.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRegistros(false);

            Console.Write($"Digite o ID do {nomeEntidade} que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            EntidadeBase registroEditado = ObterDados();
            string erros = registroEditado.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                EditarRegistro();
                return;
            }

            bool conseguiuEditar = repositorio.EditarRegistro(idSelecionado, registroEditado);

            Console.WriteLine();
            Notificador.ExibirMensagem($"O {nomeEntidade} foi editado com sucesso!", ConsoleColor.Green);
        }

        public virtual void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Excluindo {nomeEntidade}...");
            Console.WriteLine("--------------------------------------------");

            EntidadeBase[] registros = repositorio.SelecionarRegistros();
            EntidadeBase[] registrosCadastrados = new EntidadeBase[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                registrosCadastrados[i] = (Amigo)registros[i];
            }

            if (!registros.Any(a => a != null))
            {
                Notificador.ExibirMensagem("Não há membros cadastrados para exclusão.", ConsoleColor.Yellow);
                return;
            }

            VisualizarRegistros(false);

            Console.Write("Digite o ID do membro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = repositorio.ExcluirRegistro(idSelecionado);

            Console.WriteLine();
            Notificador.ExibirMensagem($"O {nomeEntidade} foi excluído com sucesso!", ConsoleColor.Green);
        }

        public abstract void VisualizarRegistros(bool v);

        public abstract EntidadeBase ObterDados();
    }
}
