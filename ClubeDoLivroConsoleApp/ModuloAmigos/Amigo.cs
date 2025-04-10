namespace ClubeDoLivroConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public int Id;
        public string Nome;
        public string Responsavel;
        public string Telefone;

        public Amigo(string nome, string responsavel, string telefone)
        {
            Nome = nome;
            Responsavel = responsavel;
            Telefone = telefone;
        }

    }
}
