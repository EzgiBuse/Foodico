using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using Foodico.Web.ViewModels;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IShoppingCartService _shoppingCartService;

        public ShopController(IProductService productService, IMemoryCache memoryCache, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _memoryCache = memoryCache;
            _shoppingCartService = shoppingCartService;
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
                    productList = productList.Where(p => p.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();

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
                // Preserve the search string in ViewData
                ViewData["CurrentFilter"] = searchString;
            }
            catch (Exception ex)
            {
                
                return View("An Error Occured");
            }

            return View("Index", productList);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            ProductDto product = new ProductDto();
            List<ProductDto> productList = new List<ProductDto>();
            var viewModel = new ProductDetailsViewModel();
            try
            {
                // Try to get the product from the cache
                if (!_memoryCache.TryGetValue("Product" + id, out product))
                {
                    // If the product is not in the cache, retrieve it from the service
                    ResponseDto response = await _productService.GetProductByIdAsync(id);

                    if (response != null && response.Result != null)
                    {
                        JObject jsonObject = (JObject)response.Result;
                        string jsonString = jsonObject.ToString();
                        product = JsonConvert.DeserializeObject<ProductDto>(jsonString);

                        // Store the product in the cache for 30 minutes
                        _memoryCache.Set("Product" + id, product, TimeSpan.FromMinutes(30));
                    }
                }
                if(!_memoryCache.TryGetValue("ProductList", out productList))
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
                 viewModel = new ProductDetailsViewModel
                {
                    Product = product,
                    RelatedProducts = productList
                };

            }
            catch (Exception ex)
            {
                
                return View("An Error Occured");
            }

            return View(viewModel);
        }

        
    }
}
