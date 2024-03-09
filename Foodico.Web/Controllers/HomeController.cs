using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace Foodico.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IMemoryCache memoryCache)
        {   _productService = productService;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<ProductDto> productList = new List<ProductDto>();
               
                ResponseDto response = await _productService.GetAllProductsAsync();

                    if (response != null && response.Result != null)
                    {
                        JArray jsonArray = (JArray)response.Result;
                        string jsonString = jsonArray.ToString();
                        productList = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);

                    }
               
                return View(productList);
            }
            catch (Exception ex)
            {

                return View();
            }

            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}