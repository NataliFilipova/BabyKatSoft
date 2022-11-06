﻿using BabyKat.Core.Contracts;
using BabyKat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyKat.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService productService;
        
        public HomeController(IProductService _productService, ICategoryService _categoryService)
        {
            productService = _productService;
           
        }
        public async Task<IActionResult> Index()
        {
            var model = await productService.LastThreeProducts();
            return View(model);
        }

 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}