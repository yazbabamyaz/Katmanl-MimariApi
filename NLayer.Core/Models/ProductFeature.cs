namespace NLayer.Core.Models
{
    //Product entity sinin ek özellikleri. Product tablosunu şişirmemek için ayrı yaptık ve 1 e 1 ilişki olacak. 
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
