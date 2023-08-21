namespace NLayer.Core.UnitOfWorks
{
    //savechanges ve savechangesAsync işlemleri için-db ye yansıtma-
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
