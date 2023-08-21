using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //**readonly ise değer atama ya default olarak burada yapılacak ya da ctor da değer atayacaksın.
        // başka yerde yanlışlıkla set etmeyi önler.
        protected readonly AppDbContext _context;//belki miras alacağımız yerlerde de işimize yarar  diye protected dedim.ProductRepository de _context i kullanabilmek için
        //dbset için
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();//dataları memory e alma -izleme takibi kırdık

        }

        public async Task<T> GetByIdAsync(int id)
        //primarykey bekler ama params bekler çünkü 1 tabloda birden fazla primarykey alanı belirleyebiliriz.
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            //remove dediğimzde entity nin state i deleted olarak işaretlenir.Savechanges bekler.
            //_context.Entry(entity).State = EntityState.Deleted;2 si aynı işlem basit işlem o yüzden async metodu yok.
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
