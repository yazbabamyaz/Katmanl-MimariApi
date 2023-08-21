namespace NLayer.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        //biz bunlara navigation property diyoruz.cat dan product a gidebilirsin
        public ICollection<Product> Products { get; set; }//bir category nin birden fazla product ı olabilir.
    }
}
