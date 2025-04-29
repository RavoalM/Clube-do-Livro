using ClubeDoLivroConsoleApp.Gerais;
using ClubeDoLivroConsoleApp.ModuloEmprestimo;

namespace ClubeDoLivroConsoleApp.ModuloAmigos
{
    public class RepositorioAmigo : RepositorioBase<Amigo>
    {
        //public bool TelefoneRepetido(string telefone, int idIgnorar = -1)
        //{
        //    for (int i = 0; i < registros.Length; i++)
        //    {
        //        if (registros[i] == null)
        //            continue;
                
        //        Amigo amigo = (Amigo)registros[i];

        //        if (amigo.Telefone == telefone && registros[i].Id != idIgnorar)
        //            return true;
        //    }

        //    return false;
        //}

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
        
    }
}
