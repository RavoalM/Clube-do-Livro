using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigos;
using ClubeDoLivroConsoleApp.ModuloCaixas;
using ClubeDoLivroConsoleApp.ModuloRevistas;
using ClubeDoLivroConsoleApp.Utils;

namespace ClubeDoLivroConsoleApp.ModuloEmprestimo;

public class TelaEmprestimo : TelaBase<Emprestimo>, ITelaCrud
{
    public RepositorioAmigo repositorioAmigo;
    public RepositorioRevista repositorioRevista;
    public RepositorioEmprestimo repositorioEmprestimo;
    public RepositorioCaixa repositorioCaixa;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo, RepositorioCaixa repositorioCaixa) : base("Empréstimo", repositorioEmprestimo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    public override char ApresentarMenu()
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
        char opcaoEscolhida = Convert.ToChar(Console.ReadLine()!);

        return opcaoEscolhida;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        Emprestimo novoEmprestimo = (Emprestimo)ObterDados();

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

        if (novoEmprestimo.Revista.StatusEmprestimo == "Reservada")
        {
            Notificador.ExibirMensagem("Esta revista já está reservada a outro membro!", ConsoleColor.Red);
            return;
        }

        string erros = novoEmprestimo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            CadastrarRegistro();
            return;
        }

        novoEmprestimo.Revista.Emprestar();
        novoEmprestimo.Amigo.AdicionarEmpréstimo(novoEmprestimo);
        repositorioEmprestimo.CadastrarRegistro(novoEmprestimo);

        Console.WriteLine();
        Notificador.ExibirMensagem("O empréstimo foi cadastrado com sucesso!", ConsoleColor.Green);
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRegistros(false);

        Console.Write("Digite o ID do empréstimo que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoEditado = (Emprestimo)ObterDados();

        string erros = emprestimoEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();
            return;
        }

        bool conseguiuEditar = repositorioEmprestimo.EditarRegistro(idSelecionado, emprestimoEditado);
        emprestimoEditado.Amigo.AdicionarEmpréstimo(emprestimoEditado);
        Console.WriteLine();
        Notificador.ExibirMensagem("O emprestimo foi editado com sucesso!", ConsoleColor.Green);
    }

    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Emprestimos...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRegistros(false);

        Console.Write("Digite o ID do emprestimo que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoSelecionado = (Emprestimo)repositorioEmprestimo.SelecionarRegistroPorId(idSelecionado);

        if (emprestimoSelecionado.Situacao != "Concluído")
        {
            Notificador.ExibirMensagem("O empréstimo não pode ser exclúido pois ainda está aberto.", ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorioEmprestimo.ExcluirRegistro(idSelecionado);

        Console.WriteLine();
        Notificador.ExibirMensagem("O emprestimo foi excluído com sucesso!", ConsoleColor.Green);
    }

    public override void VisualizarRegistros(bool exibirTitulo)
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
        
        List<Emprestimo> registros = repositorioEmprestimo.SelecionarRegistros();

        foreach (Emprestimo e in registros)
        {
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

        VisualizarRegistros(false);

        Console.Write("Selecione o ID de um Empréstimo: ");
        int idDevolucao = Convert.ToInt32(Console.ReadLine()!.Trim());

        Emprestimo emprestimoEscolhido = (Emprestimo)repositorioEmprestimo.SelecionarRegistroPorId(idDevolucao);

        emprestimoEscolhido.RegistrarDevolucao();

        Notificador.ExibirMensagem("Devolução feita com sucesso!", ConsoleColor.Green);
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
        List<Amigo> registros = repositorioAmigo.SelecionarRegistros();
        foreach (Amigo a in registros)
        {
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

        Caixa caixaSelecionada = repositorioCaixa.SelecionarRegistroPorId(idCaixa);
        Revista[] revistasNaCaixa = caixaSelecionada.ObterRevistas();

        if (caixaSelecionada == null)
        {
            Notificador.ExibirMensagem("Id da caixa selecionada não existe", ConsoleColor.Red);
            return conseguiuSelecionar = false;
        }

        Console.WriteLine();
        Console.WriteLine("Visualizando Revistas da Caixa \"" + caixaSelecionada.Etiqueta + "\"");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -21} | {3, -15} | {4, -25}",
            "Id", "Titulo", "Numero de edição", "Ano de publicação", "Status de empréstimo"
        );

        foreach (Revista revista in revistasNaCaixa)
        {
            if (revista == null) continue;

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -20} | {4, -25}",
                revista.Id, revista.Titulo, revista.NumeroEdicao, revista.AnoPublicacao, revista.StatusEmprestimo
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
        List<Caixa> registros = repositorioCaixa.SelecionarRegistros();
        foreach (Caixa c in registros)
        {
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -21} | {3, -15}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
            );
        }
        Console.WriteLine();
    }

    public override Emprestimo ObterDados()
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

        Emprestimo novaEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);
        return novaEmprestimo;
    }
}