using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created (only if not exists)
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }
            else if (!await context.Database.CanConnectAsync())
            {
                await context.Database.EnsureCreatedAsync();
            }

            // Seed Roles
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Vet"));
                await roleManager.CreateAsync(new IdentityRole("Staff"));
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            // Seed Users
            if (!await userManager.Users.AnyAsync())
            {
                var adminUser = new AppUser
                {
                    UserName = "admin@petcare.com",
                    Email = "admin@petcare.com",
                    FullName = "Admin User",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(adminUser, "Admin");

                var vetUser = new AppUser
                {
                    UserName = "vet@petcare.com",
                    Email = "vet@petcare.com",
                    FullName = "Dr. Smith",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(vetUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(vetUser, "Vet");

                var customerUser = new AppUser
                {
                    UserName = "customer@petcare.com",
                    Email = "customer@petcare.com",
                    FullName = "John Doe",
                    EmailConfirmed = true,
                    Address = "123 Main St"
                };
                await userManager.CreateAsync(customerUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(customerUser, "Customer");
            }

            // Seed Services
            if (!await context.Services.AnyAsync())
            {
                var services = new List<Service>
                {
                    new Service
                    {
                        Name = "General Consultation",
                        Description = "Comprehensive health checkup for your pet.",
                        Price = 50.00m,
                        DurationMinutes = 30,
                        ImageUrl = "https://images.unsplash.com/photo-1628009368231-7603352984c3?w=500&auto=format&fit=crop&q=60"
                    },
                    new Service
                    {
                        Name = "Vaccination",
                        Description = "Essential vaccines to keep your pet safe.",
                        Price = 30.00m,
                        DurationMinutes = 15,
                        ImageUrl = "https://images.unsplash.com/photo-1576201836163-4975841e71cf?w=500&auto=format&fit=crop&q=60"
                    },
                    new Service
                    {
                        Name = "Grooming Spa",
                        Description = "Bathing, hair cutting, and styling.",
                        Price = 45.00m,
                        DurationMinutes = 60,
                        ImageUrl = "https://images.unsplash.com/photo-1516734212186-a967f81ad0d7?w=500&auto=format&fit=crop&q=60"
                    },
                    new Service
                    {
                        Name = "Pet Hotel",
                        Description = "Overnight boarding with care and play.",
                        Price = 80.00m,
                        DurationMinutes = 1440, // 24 hours
                        ImageUrl = "https://images.unsplash.com/photo-1541781777631-fa182f3a4b30?w=500&auto=format&fit=crop&q=60"
                    }
                };
                await context.Services.AddRangeAsync(services);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Premium Dog Food",
                        Description = "High-protein dry food for adult dogs.",
                        Price = 25.99m,
                        Category = "Food",
                        StockQuantity = 100,
                        ImageUrl = "https://images.unsplash.com/photo-1589924691195-41432c84c161?w=500&auto=format&fit=crop&q=60"
                    },
                    new Product
                    {
                        Name = "Cat Toy Mouse",
                        Description = "Interactive toy for cats.",
                        Price = 5.99m,
                        Category = "Toys",
                        StockQuantity = 50,
                        ImageUrl = "https://images.unsplash.com/photo-1545249390-6bdfa286032f?w=500&auto=format&fit=crop&q=60"
                    },
                    new Product
                    {
                        Name = "Pet Shampoo",
                        Description = "Gentle formula for sensitive skin.",
                        Price = 12.50m,
                        Category = "Care",
                        StockQuantity = 30,
                        ImageUrl = "https://images.unsplash.com/photo-1585714660475-1f436154a9a4?w=500&auto=format&fit=crop&q=60"
                    }
                };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
