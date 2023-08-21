using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    //:IGenericRepository<Product> yazmasaydık olmaz mıydı
    public interface IProductRepository : IGenericRepository<Product>
    {
        //product a özel metotlar + diğer metotlara zaten :IGenericRepository<Product> hamlesi ile ulaşırım.
        Task<List<Product>> GetProductWithCategory();

    }
}
