namespace ClubeDoLivroConsoleApp.Gerais
{
    public abstract class EntidadeBase
    {
        public int Id { get; set; }

        public abstract void AtualizarRegistro(EntidadeBase registroEditado);
        public abstract string Validar();
    }
}
