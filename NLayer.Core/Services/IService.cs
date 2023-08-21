using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);


        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);//db ye yansıması bu service i çağıran kodda tolist çağırınca gerçekleşecek. asenkron belirlemedik
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);
        Task RemoveAsyn(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
        //Service katmanında değişiklikleri db ye yansıtacağımız(savechanges) için async-Task- kullandık.
    }
}
