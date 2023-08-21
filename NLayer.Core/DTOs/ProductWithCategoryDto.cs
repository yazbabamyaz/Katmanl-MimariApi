using NLayer.Core.Models;

namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto : Product
    {
        public CategoryDto Category { get; set; }
    }
}
