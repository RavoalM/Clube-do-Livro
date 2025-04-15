using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;

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

        public bool TelefoneRepetido(string telefone, int idIgnorar = -1)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null)
                    continue;

                if (amigos[i].Telefone == telefone && amigos[i].Id != idIgnorar)
                    return true;
            }

            return false;
        }


        public Amigo SelecionarAmigoPorId(int idAmigo)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = amigos[i];

                if (a == null)
                    continue;

                else if (a.Id == idAmigo)
                    return a;
            }

            return null;
        }

        public bool VerificarEmprestimosAmigo(Amigo amigoEscolhido)
        {
            int emprestimos = 0;

            if (amigoEscolhido.Emprestimos == null)
                return false;

            foreach (Emprestimo e in amigoEscolhido.Emprestimos)
            {
                if (e != null && e.Situacao != "Concluído")
                    return true;
            }

            if (emprestimos > 0)
                return true;
            else
                return false;
        }

        public Amigo[] SelecionarAmigos()
        {
            return amigos;
        }

        
    }
}
