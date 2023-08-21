using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        //generic metot yazdık. ok-badrequest gibi farklı farklı dönmek yerine objectresult dönelim
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)//noContent bişey dönme
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                }; 
            
            return new ObjectResult(response)
            {
                StatusCode= response.StatusCode,
            };
        }
        
    }
}
