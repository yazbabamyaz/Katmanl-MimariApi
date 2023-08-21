using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);//gerek yoktu ama örnek
            builder.Property(x => x.Id).UseIdentityColumn();//otomatik artan özelliği 1 er 1 er
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.ToTable("Products");//tablo ismi için

            //ilişkiler için örnek olsun diye:1 product ın 1 catergorisi olur hasone
            //bir categorinin birden çok product ı olur:WithMany
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            //. ile özellik belirtme fluent api özelliğidir.
        }
    }
}
