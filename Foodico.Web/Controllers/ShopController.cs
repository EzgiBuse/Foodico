using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using Foodico.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodico.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;

        public ShopController(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> productList = new List<ProductDto>();

            try
            {
                // Try to get the product list from the cache
                if (!_memoryCache.TryGetValue("ProductList", out productList))
                {
                    // If the product list is not in the cache, retrieve it from the service
                    ResponseDto response = await _productService.GetAllProductsAsync();

                    if (response != null && response.Result != null)
                    {
                        JArray jsonArray = (JArray)response.Result;
                        string jsonString = jsonArray.ToString();
                        productList = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);

                        // Store the product list in the cache for 30 minutes
                        _memoryCache.Set("ProductList", productList, TimeSpan.FromMinutes(30));
                    }
                }
            }
            catch (Exception ex)
            {
                
                 return View("No Items Found");
            }

            return View(productList);
        }

        public async Task<IActionResult> FilterAndSort(string searchString, string sortOrder)
        {
            List<ProductDto> productList = new List<ProductDto>();

            try
            {
                // Try to get the product list from the cache
                if (!_memoryCache.TryGetValue("ProductList", out productList))
                {
                    // If the product list is not in the cache, retrieve it from the service
                    ResponseDto response = await _productService.GetAllProductsAsync();

                    if (response != null && response.Result != null)
                    {
                        JArray jsonArray = (JArray)response.Result;
                        string jsonString = jsonArray.ToString();
                        productList = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);
                    }
                }

                // Filter the products based on the search string
                if (!string.IsNullOrEmpty(searchString))
                {
                    productList = productList.Where(p => p.Name.Contains(searchString)).ToList();
                }

                // Sort the products based on the sort order
                switch (sortOrder)
                {
                    case "name_asc":
                        productList = productList.OrderBy(p => p.Name).ToList();
                        break;
                    case "name_desc":
                        productList = productList.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "price_asc":
                        productList = productList.OrderBy(p => p.Price).ToList();
                        break;
                    case "price_desc":
                        productList = productList.OrderByDescending(p => p.Price).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                
                return View("An Error Occured");
            }

            return View("Index", productList);
        }

    }
}
