using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    //GenericRepository<Product>,yazmasaydık 10 metot gelirdi  kod tekrarı olurdu
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Product>> GetProductWithCategory()
        {
            //Eager Loading yaptık. Daha datayı çekerken Kategorilerinde alınmasını istedik.
            //eğer ihtiyaç olduğunda daha sonra çekersek o da lazy loading olurmuş.
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
