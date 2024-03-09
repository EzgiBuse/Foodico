using AutoMapper;
using Foodico.Services.ProductAPI.Data;
using Foodico.Services.ProductAPI.Models;
using Foodico.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Foodico.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _responseDto= new ResponseDto();
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetProductsForPage")]
        public ResponseDto GetProductsForPage(ShopIndexRequestDto shopIndexRequestDto)
        {
            try
            {
                int pageNumber = shopIndexRequestDto.PageNumber;
                int pageSize = shopIndexRequestDto.PageSize;
                // Calculate the number of items to skip based on the current page number
                int itemsToSkip = (pageNumber - 1) * pageSize;

                // Query the database for products, ordering by whatever criteria you prefer
                var products = _db.Products
                    .OrderBy(p => p.ProductId) // Ordering example
                    .Skip(itemsToSkip) // Skip the appropriate number of items based on the current page
                    .Take(pageSize) // Take the number of items for this page
                    .ToList();
                var productDtos = _mapper.Map<List<ProductDto>>(products);
                ShopIndexResponseDto res = new ShopIndexResponseDto
                {
                    Products = productDtos,
                    TotalProducts = _db.Products.Count()
                };
                _responseDto.Result = res;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpGet]
        [Route("GetTotalProductsCount")]
        public int GetTotalProductsCount()
        {
            // Retrieve the total number of products in the database
            int totalProducts = _db.Products.Count();
            return totalProducts;
        }

        [HttpGet]
       
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
                return _responseDto;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductId == id);
                _mapper.Map<ProductDto>(obj);
                _responseDto.Result = obj;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpPost]
       [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _db.Products.Add(obj);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {

                Product obj = _mapper.Map<Product>(productDto);
                _db.Products.Update(obj);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpDelete]
       [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _db.Products.First(x => x.ProductId == id);
                _db.Products.Remove(obj);
                _db.SaveChanges();


            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }
    }
}
