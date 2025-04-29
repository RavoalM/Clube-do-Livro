namespace ClubeDoLivroConsoleApp.Gerais
{
    public abstract class RepositorioBase<T> where T : EntidadeBase<T>
    {
        private List<T> registros = new List<T>();
        private int contadorIds = 0;

        public void CadastrarRegistro(T novoRegistro)
        {
            novoRegistro.Id = ++contadorIds;
            registros.Add(novoRegistro);
        }

        public bool EditarRegistro(int idRegistro, T registroEditado)
        {
            foreach (T item in registros)
            {
                if (item.Id == idRegistro)
                {
                    item.AtualizarRegistro(registroEditado);
                    return true;
                }
            }

            return false;
        }

        public bool ExcluirRegistro(int idRegistro)
        {
            T registroSelecionado = SelecionarRegistroPorId(idRegistro);

            if (registroSelecionado != null)
            {
                registros.Remove(registroSelecionado);

                return true;
            }

            return false;
        }

        public T SelecionarRegistroPorId(int idRegistro)
        {
            foreach (T item in registros)
            {
                if (item.Id == idRegistro)  
                    return item;
            }
            return null;
        }

        public List<T> SelecionarRegistros()
        {
            return registros;
        }
    }
}
