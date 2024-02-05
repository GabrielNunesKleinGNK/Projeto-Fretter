namespace Fretter.Api.Helpers
{
    public interface IViewModel<out TEntity>
         //where TEntity : class
    {
        TEntity Model();
    }
}
