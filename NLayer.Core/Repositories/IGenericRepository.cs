using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);


        IQueryable<T> GetAll();//Where metodu da aynısını yapıyor
        //where sorgumuz olabilir IQueryable döncez list değil neden çünkü sorgular direkt db ye gitmez ne zamn ki tolist dersek  o zaman gider performans için
        //productrepository.where(x=>x.id>5) buraya kadar IQueryable dönüyor daha db ye sorgu yapmadı veri çekmedi sadece sorguyu hazırladık. ama list deseydik productrepository.where(x=>x.id>5) buradan sonra hemen db de işlem yapıp verileri çekip memorye alır.Whereden sonra orderby yada başka komutlar çağırabilirsin... productrepository.where(x=>x.id>5).ToListAsync(); dediğimizde db ye sorgu yapacak
        IQueryable<T> Where(Expression<Func<T, bool>> expression);//x=>x.id>5 ilk kısım YANİ ENTİTY :x T DİR 2.kısım x.id>5 bool dür
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);//birden fazla ürün kaydı için. List almadık interface aldık soyut nesnelerle çalışmak önemli

        //update-delete async değil
        //efcore memory e alıp takip ettiği product ın sadece state i ni değiştiriyor.Modified yapıyor uzun süren işlem yok
        void Update(T entity);
        void Remove(T entity);
        //asenkron metotları neden kullanırız thread leri bloklamamak için daha efektif kullanmak için
        void RemoveRange(IEnumerable<T> entities);

        //dbcontext efcore un update-delete için async metotları yok.
    }
}
