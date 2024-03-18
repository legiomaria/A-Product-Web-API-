﻿using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Size> Sizes { get; set; }
        
    }
}