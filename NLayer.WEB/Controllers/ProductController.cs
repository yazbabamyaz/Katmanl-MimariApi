using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.WEB.Filters;
using NLayer.WEB.Services;

namespace NLayer.WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        
        public async Task<IActionResult> Index()
        {

            return View(await _productApiService.GetProductWithCategoryAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Save()
        {           

            var categoryDto = await _categoryApiService.GetAllAsync();           
            //drop doldurmak için
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {

                await _productApiService.SaveAsync(productDto);

               
                return RedirectToAction(nameof(Index));//tip güvenli-"index" te diyebilirdik.
            }
            //başarısız ise tekrar drop doldur.
            var categories = await _categoryApiService.GetAllAsync();
         
            //drop doldurmak için
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();



        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product=await _productApiService.GetByIdAsync(id);
            var categoryDto = await _categoryApiService.GetAllAsync();
           
            //drop doldurmak için
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name",product.CategoryId);
            //4.parametre "seçili gelecek olan categoridir".
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)//client tarafta doğrulama yapıldı ama burada da yapıyoruz çift taraflı.
            {
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            //doğrulama başarısız ise drop doldur:
            var categories = await _categoryApiService.GetAllAsync();
           
            //drop doldurmak için
            ViewBag.Categories = new SelectList(categories, "Id", "Name",productDto.CategoryId);
            return View(productDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
           
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
