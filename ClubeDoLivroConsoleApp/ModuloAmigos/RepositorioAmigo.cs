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

        public bool EditarAmigo(int idAmigo, Amigo amigoEditado)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null) continue;

                else if (amigos[i].Id == idAmigo)
                {
                    amigos[i].Nome = amigoEditado.Nome;
                    amigos[i].Responsavel = amigoEditado.Responsavel;
                    amigos[i].Telefone = amigoEditado.Telefone;

                    return true;
                }
            }

            return false;
        }

        public bool ExcluirAmigo(int idAmigo)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null) continue;

                else if (amigos[i].Id == idAmigo)
                {
                    amigos[i] = null;

                    return true;
                }
            }

            return false;
        }

        public Amigo[] SelecionarAmigos()
        {
            return amigos;
        }

        
    }
}
