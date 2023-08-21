using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.WEB.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue=context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null) 
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity=await _service.GetByIdAsync(id);
            if (anyEntity!=null) 
            {
                await next.Invoke();
                return;
            }
            //apiden farkı burada error sayfaya yönlendircez.
            var errorViewModel = new ErrorViewModel();

            errorViewModel.Errors.Add($"{typeof(T).Name}({id}) not found.");

            context.Result = new RedirectToActionResult("Error","Home",errorViewModel);
        }
    }
}