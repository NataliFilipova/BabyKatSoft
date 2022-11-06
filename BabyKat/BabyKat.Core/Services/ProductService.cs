﻿using BabyKat.Core.Contracts;
using BabyKat.Core.Models.Categoryy;
using BabyKat.Core.Models.Productt;
using BabyKat.Infrastructure.Data;
using BabyKat.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyKat.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repo;
        public ProductService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
           return await repo.AllReadonly<Category>().ToListAsync();
              

                
        }
        public async Task<IEnumerable<ProductModel>> GetProductsForCategoryAsync(int categoryId)
        {
            var category = await repo.GetByIdAsync<Category>(categoryId);

            return await repo.AllReadonly<Product>()
                .OrderByDescending(p => p.Id)
                .Where(p => p.CategoryId == category.Id)
                .Select(p => new ProductModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description

                })
                 
                .ToListAsync();

        }

        public async Task AddProductAsync(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
               
                CategoryId = model.CategoryId
            };

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductHomeModel>> LastThreeProducts()
        {
            return await repo.AllReadonly<Product>()
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductHomeModel() 
                { 
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name
                })
                 .Take(2)
                .ToListAsync();
        }

          public async Task<IEnumerable<ProductModel>> GetAllAsync()
            {
            return await repo.AllReadonly<Product>()
             .Select(p => new ProductModel()
             {
                 Id = p.Id,
                 Name = p.Name,
                 Description = p.Description,
                 ImageUrl= p.ImageUrl, 
                 Price = p.Price,
                 CategoryId = p.CategoryId
             }).ToListAsync();


        }

        public async Task RemoveProductFromCategory(int productId)
        {
            await repo.DeleteAsync<Product>(productId);
            await repo.SaveChangesAsync();
        }
    }
}
