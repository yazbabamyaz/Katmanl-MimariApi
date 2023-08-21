using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore] //jsona dönüştürürken ignore et.
        public int StatusCode { get; set; }//dış dünyaya açmak istemiyorum. client zaten istek yaptığında bu status code u  görüyor postman vs ile
        public List<string> Errors { get; set; }

        //static metotlar oluşturalım başarılı olunca dönsün ya da başarısız olunca...new ile nesne oluşturmaya gerek yok. static metotları kullancaz
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        //bazen geriye data dönmeyebiliriz.Mesela update işlemi olan endpoint düşünelim geriye bişey dönmeyebiliriz.
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        //hatalar
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        //tek hata
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
