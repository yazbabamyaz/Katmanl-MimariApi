using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app )
        {
            //exc. fırlayınca çalışır.run: sonlandırıcı mid..
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";//response ın contenttype belirttik.
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();//fırlatılan hatayı aldık.
                    //exception sınfıı yapalım service katmanına yani hata tipi ne olacak ...

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,    //hata ClientSideException ise 400 gönder. değilse 500
                        NotFoundException=> 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;



                    var errorViewModel = new ErrorViewModel();

                  


                   


                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                    //response ı json a serilize et
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));//önceden genelde NewtonSoft kullanılırdı.Şimdi ise:JsonSerializer

                });
            });
        }
    }
}
