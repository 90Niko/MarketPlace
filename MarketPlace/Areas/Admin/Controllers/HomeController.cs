﻿using MarketPlace.Areas.Admin.Models.Product;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly ApplicationDbContext data;

        public HomeController(ApplicationDbContext _data)
        {
            data = _data;
        }


        public IActionResult Dashboard()
        {
            var products = data.Products
                .Where(p => p.IsApproved == false)
                .Select(p => new ProductListingServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.Image,
                    Description = p.Description,
                    Price = p.Price,
                    Seller = p.Seller.UserName,
                    Category = p.Category.Name
                })
                .ToList();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ForReview(ProductListingServiceModel model)
        {
            var product = await data.Products.Where(p => p.Id == model.Id && p.IsApproved == false)
                  .FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest();
            }

            product.IsApproved = true;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Dashboard));
        }  
    }
}
