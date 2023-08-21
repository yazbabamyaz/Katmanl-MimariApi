namespace NLayer.Core.Models
{
    //Category ile aralarında 1 e çok ilişki olacak.
    //ProductFeature ile 1-1 ilişki var
    public class Product : BaseEntity
    {
        //nullable için ya ? koy ya constructorda geç ya da bu özelliği kapat
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        //[ForeignKey] efcore bunun foreignKey olduğunu bilir isimlendirmeden dolayı
        public int CategoryId { get; set; }//bu Product entity si için foreinkey dir
        public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; }
    }
}
