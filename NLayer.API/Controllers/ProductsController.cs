using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    //[ValidateFilterAttribute] bestPractise değil
    public class ProductsController : CustomBaseController
    {
        //şimdilik maplemeyi burda yapalım sonra service katmanında yapcaz.
        private readonly IMapper _mapper;
        //controller sadece service bilir repoyu referans almayacaklar
       
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IService<Product> service, IProductService productService)
        {
            _mapper = mapper;
           
            _service = productService;
        }

        //[HttpGet("GetProductWithCategory")]//get çakışması olmasın diye
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _service.GetProductWithCategory());
            //return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200,productsDtos));
            //bunun için basecontroller oluşturalım. herseferinde ok-badrequest-NotFound vs yazmayalım
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product= await _service.GetByIdAsync(id);
            //if (product==null)
            //{
            //    return CreateActionResult(CustomResponseDto<ProductDto>.Fail(400,"hata"));
            //}
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }
        [HttpPost]
        public  async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
           var product= await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201,productsDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
             await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));// geriye bişey dönmediği için NoContentdto dedim zorunlu değildi...
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) 
        { 
            var product=await _service.GetByIdAsync(id);
            //İleride bu if kalkacak
            if (product==null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404,"bu id ye sahip ürün yok"));
            }
            await _service.RemoveAsyn(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


    }
}
