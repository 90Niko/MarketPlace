using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Data.SeedDb
{
    internal class SeedData
    {
        public ApplicationUser AdminUser { get; private set; }

        public Category ElectronicsCategory { get; set; }

        public Category ClothingCategory { get; set; }

        public Category HomeAndGarten { get; set; }
        public SeedData()
        {
            SeedUsers();
            SeedCategories();
        }

        private void SeedUsers()
        {
            var hashPassword = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser()
            {
                Id = "f5563c5e-d780-4bce-812d-408f2c079ae2",
                UserName = "admin@mail.com",
                NormalizedUserName = "admin@mail.com",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
                FirstName = "Great",
                LastName = "Admin"
            };

            AdminUser.PasswordHash = hashPassword.HashPassword(AdminUser, "admin123");

        }
        private void SeedCategories()
        {
            ElectronicsCategory = new Category { Id = 1, Name = "Electronics" };

            ClothingCategory = new Category { Id = 2, Name = "Clothing" };

            HomeAndGarten = new Category { Id = 3, Name = "Home and Garten" };
        }
    }
}
