namespace NLayer.Core.DTOs
{
    public class ProductUpdateDto//apimizin alacağı model - id gerekli o yüzden yaptık-
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
