using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;
using ClubeDoLivroConsoleApp.ModuloRevistas;
using ClubeDoLivroConsoleApp.Utils;

namespace ClubeDoLivroConsoleApp.ModuloAmigos;

public class TelaAmigo : TelaBase<Amigo>, ITelaCrud
{
    public RepositorioAmigo repositorioAmigo;
    public RepositorioEmprestimo repositorioEmprestimo;
    public RepositorioRevista repositorioRevista;

    public TelaAmigo(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista) : base("Amigo", repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioRevista = repositorioRevista;
    }

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
        Console.WriteLine($"2 - Edição de {nomeEntidade}");
        Console.WriteLine($"3 - Exclusão de {nomeEntidade}");
        Console.WriteLine($"4 - Visualização de {nomeEntidade}s");
        Console.WriteLine($"5 - Visualização de Empréstimos de {nomeEntidade}");
        Console.WriteLine("S - Voltar");
        Console.WriteLine("--------------------------------------------");

        Console.Write("Digite um opção válida: ");
        char opcaoEscolhida = Convert.ToChar(Console.ReadLine()!);

        return opcaoEscolhida;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Amigo...");
        Console.WriteLine("--------------------------------------------");

        Amigo novoAmigo = (Amigo)ObterDados();

        string erros = novoAmigo.Validar();

        //if (repositorioAmigo.TelefoneRepetido(novoAmigo.Telefone))
        //{
        //    Notificador.ExibirMensagem("Este telefone já pertence a outro amigo.", ConsoleColor.Red);
        //    CadastrarRegistro();
        //    return;
        //}

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            CadastrarRegistro();
            return;
        }

        repositorioAmigo.CadastrarRegistro(novoAmigo);

        Console.WriteLine();
        Notificador.ExibirMensagem("O membro foi cadastrado com sucesso!", ConsoleColor.Green);
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Membro...");
        Console.WriteLine("--------------------------------------------");
        VisualizarRegistros(false);

        Console.Write("Digite o ID do membro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Amigo amigoEditado = (Amigo)ObterDados();

        string erros = amigoEditado.Validar();

        //if (repositorioAmigo.TelefoneRepetido(amigoEditado.Telefone, idSelecionado))
        //{
        //    Notificador.ExibirMensagem("Este telefone já pertence a outro amigo.", ConsoleColor.Red);
        //    EditarRegistro();
        //    return;
        //}

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            EditarRegistro();
            return;
        }

        bool conseguiuEditar = repositorioAmigo.EditarRegistro(idSelecionado, amigoEditado);

        Console.WriteLine();
        Notificador.ExibirMensagem("O membro foi editado com sucesso!", ConsoleColor.Green);
    }

    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Membro...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRegistros(false);

        Console.Write("Digite o ID do membro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idSelecionado);
        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idSelecionado);

        if (repositorioAmigo.VerificarEmprestimosAmigo(amigoSelecionado))
        {
            Notificador.ExibirMensagem($"\nO membro ainda possui empréstimos em aberto e não pode ser excluído.", ConsoleColor.Red);
            return;
        }

        if (repositorioRevista.VerificarRevistaReservada(revistaSelecionada))
        {
            Notificador.ExibirMensagem("O membro ainda ainda possui uma reserva e não pode ser excluído.", ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorioAmigo.ExcluirRegistro(idSelecionado);

        Console.WriteLine();
        Notificador.ExibirMensagem("O membro foi excluído com sucesso!", ConsoleColor.Green);
    }

    public override void VisualizarRegistros(bool exibirTitulo)
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

        List<Amigo> registros = repositorioAmigo.SelecionarRegistros();

        foreach (Amigo a in registros)
        {
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                a.Id, a.Nome, a.Responsavel, a.Telefone
            );
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    public void VisualizarEmprestimosAmigo()
    {
        VisualizarRegistros(false);

        Console.Write("Digite o ID do membro que realizou o empréstimo: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine()!.Trim());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);
        Emprestimo[] emprestimosDoAmigo = amigoSelecionado.ObterEmprestimos();

        Console.WriteLine();
        Console.WriteLine("Visualizando Emprestimo de \"" + amigoSelecionado.Nome + "\"");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -20} | {5, -20}",
            "Id", "Amigo", "Revista", "Data de Empréstimo", "Data de Devolução", "Situação"
        );

        foreach (Emprestimo emprestimo in emprestimosDoAmigo)
        {
            if (emprestimo == null) continue;
            Console.WriteLine(
               "{0, -10} | {1, -15} | {2, -21} | {3, -18} | {4, -20} | {5, -20}",
                emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.Revista.Titulo, emprestimo.DataEmprestimo.ToShortDateString(), emprestimo.ObterDataDevolucao().ToShortDateString(), emprestimo.Situacao
           );
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    public override Amigo ObterDados()
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
