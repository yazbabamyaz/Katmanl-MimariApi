using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        //DbContextOptions alır neden:çünkü bu optionsla beraber db yolunu program.cs de belirteceğiz.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }//bu product a bağlı olduğu için yani product eklemeden productfeature eklenmesin diyorsan bunu kapatabilirsin farklı şekilde yaparsın.

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //efcore biz savechanges i  çağırana kadar tüm entityleri memory de track ediyordu.Db ye yansıtmadan hemn önce entity nin update mi added mı olduğunu bulalım ona göre createddate yada updated date i değiştircez.

            //track etmiş entity lerde dönelim
            //db ye yansıtmadan önce db ye yansıtılacak olan entitylerin CreatedDate ve UpdatedDate ini değiştirdik.
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)//tüm entityler baseentity idi
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate= DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                //CreatedDate alanına dokunma dedik
                                Entry(entityReference).Property(x=>x.CreatedDate).IsModified= false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }

                    }
                }
            }




            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Id yi primaryKey olarak belirtmiş olduk.Buna gerek yoktu.
            //modelBuilder.Entity<Category>().HasKey(c => c.Id);
            //BU AYARLARI AYRI BİR Configuration CLASSTA YAPALIM BURA TEMİZ KALSIN

            //Üzerinde çalıştığım Assembly içindeki tüm konfigürasyon dosyalarını oku:reflection yaparak.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //2 class ın seed işlemlerini konfigürasyon dosyasından yaptık. örnek olsun diye birini de burdan yaptım.
            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature
                {
                    Id = 1,
                    Color = "Kırmızı",
                    Height = 30,
                    Width = 10,
                    ProductId = 1

                },
                new ProductFeature
                {
                    Id = 2,
                    Color = "Mavi",
                    Height = 50,
                    Width = 20,
                    ProductId = 2

                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
