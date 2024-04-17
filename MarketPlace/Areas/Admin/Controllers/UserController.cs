using MarketPlace.Areas.Admin.Models.User;
using MarketPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        ApplicationDbContext data;

        public UserController(ApplicationDbContext _data)
        {
            data = _data;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await data.Users
                .Where(u => u.UserName != User.Identity.Name)
                .Select(u => new UserServiceModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.NormalizedUserName.ToLower()
                })
                .ToListAsync();

            if (users == null)
            {
                return BadRequest();
            }

            return View(users);
        }
    }
}
