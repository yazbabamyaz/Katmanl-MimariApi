namespace NLayer.Core.Models
{
    //ortak property lerin olduğu class.
    public abstract class BaseEntity//newlemeyaceğimiz için abstract yaptım
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
