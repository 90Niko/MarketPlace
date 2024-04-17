using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Areas.Admin.Components
{
    public class AdminMenuCompnent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
