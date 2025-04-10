using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloAmigo;


namespace ClubeDoLivroConsoleApp.ModuloAmigos
{
    public class RepositorioAmigo
    {
        public Amigo[] amigos = new Amigo[100];
        public int contadorAmigos = 0;

        public void CadastrarAmigo(Amigo novoAmigo)
        {
            novoAmigo.Id = GeradorIds.GerarIdAmigo();

            amigos[contadorAmigos++] = novoAmigo;
        }

        public Amigo[] SelecionarAmigos()
        {
            return amigos;
        }

        
    }
}
