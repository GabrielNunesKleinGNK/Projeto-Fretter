namespace Fretter.Domain.Interfaces.Repository
{
    public interface IUnitOfWork<TContext>
    {
        int Commit();
    }
}
