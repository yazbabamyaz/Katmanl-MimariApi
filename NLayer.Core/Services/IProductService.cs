using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        //burada product ile category döncem. özel bir sınıf entity DEĞİL.
        //repositoriler geriye entity dönerken service katmanı ise api lerin istediği dto yu dönüyor
        // Task<List<ProductWithCategoryDto>> GetProductWithCategory();
        //bir tık ilerletelim geriye customresponse dönsün ProductWithCategoryDto yerine
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory();
    }
}
