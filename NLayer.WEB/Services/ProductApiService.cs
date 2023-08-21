using NLayer.Core.DTOs;

namespace NLayer.WEB.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            //datayı json olarak alır .net 5.0 ile geldi
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductWithCategory");

            //önceden biz response ın contexi ni okurduk. issuccesfull vs başarılı ise...contexti readasstring ileokurduk sonra stringi json a cast ederdik. burada ise direkt json veriyor.
            return response.Data;

        }

        public async Task<ProductDto> SaveAsync(ProductDto newProduct)
        {
            //önceden postAsync kullanırdık. json olarak data gönder diyor.
            var response = await _httpClient.PostAsJsonAsync("products", newProduct);
            if (!response.IsSuccessStatusCode) return null;
            //başarılıysa body i oku.
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data;
        }
           
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync < CustomResponseDto < ProductDto >>($"products/{id}");

            //bu şekilde de hataları varsa kontrol edebilirsin.
            //if (response.Errors.Any())
            //{

            //}
           return response.Data;
        }

        public async Task<bool> UpdateAsync(ProductDto newProduct)
        {
            //geriye data dönmüyor sadeece 204  gibi kod dönüyor
            var response = await _httpClient.PutAsJsonAsync("products", newProduct);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            
            return response.IsSuccessStatusCode;
        }

    }
}
