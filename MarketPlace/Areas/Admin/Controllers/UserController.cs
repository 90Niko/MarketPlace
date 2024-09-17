using MarketPlace.Areas.Admin.Models.Product;
using MarketPlace.Areas.Admin.Models.User;
using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Core.Services;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
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

        [HttpGet]
        public async Task<IActionResult> AllCategory()
        {
            var categories = await data.Categories
                 .Where(c => c.IsDeleted == false)
                 .Select(c => new CategoryServiceModel
                 {
                     Id = c.Id,
                     Name = c.Name
                 })
                 .ToListAsync();

            if (categories == null)
            {
                return BadRequest();
            }

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(CategoryServiceModel model)
        {

            var category = await data.Categories
                .Where(c => c.Id == model.Id)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return BadRequest();
            }

            category.IsDeleted = true;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(AllCategory));
        }
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<Category> categories = await data.Categories.ToListAsync();

            if (categories.Any(c => c.Name == model.Name))
            {

                if (categories.Any(c => c.IsDeleted == true && c.Name == model.Name))
                {
                    var check = categories.FirstOrDefault(c => c.Name == model.Name);
                    check.IsDeleted = false;
                    await data.SaveChangesAsync();
                    return RedirectToAction(nameof(AllCategory));
                }
                ModelState.AddModelError(nameof(model.Name), "Category already exists.");
                return View(model);
            }

            var category = new Category
            {
                Name = model.Name,
                IsDeleted = false
            };

            await data.Categories.AddAsync(category);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(AllCategory));
        }
    }
}
