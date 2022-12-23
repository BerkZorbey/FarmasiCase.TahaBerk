using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FarmasiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper, ICartService cartService, IUserService userService)
        {
            _productService = productService;
            _mapper = mapper;
            _cartService = cartService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<List<Product>> GetAllProducts()
        {
            var productList = await _productService.GetAllProduct();
            return productList;
        }
        [HttpGet("{id}")]
        public Task<Product> GetProductById(Guid id)
        {
            var product = _productService.GetProductById(id);
            return product;
        }
        [HttpPost]
        public IActionResult AddProduct(ProductDTO addProduct)
        {
            var productMap = _mapper.Map<Product>(addProduct);
            productMap.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _productService.AddProduct(productMap);
            return StatusCode(201);
        }
        [HttpPost("addCart")]
        public IActionResult AddToCart(Guid userId, Guid productId)
        {

            var product = _productService.GetProductById(productId);
            var user = _userService.GetUserById(userId);
            var productList = product?.Result;
            if (user.Result.Cart.Product == null)
            {
                user.Result.Cart.Product = new List<Product> { productList };
            }
            else
            {
                user.Result.Cart.Product.Add(productList);
            }
            
           

            if (!string.IsNullOrEmpty(userId.ToString()) && !string.IsNullOrEmpty(productId.ToString()))
            {
                _cartService.UpdateCart(user.Result.Cart.Id, user.Result.Cart);
                _userService.UpdateUser(userId, user.Result);
                return Ok();
            }
         
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateProduct(Guid id,ProductDTO updateProduct)
        {
            var productMap = _mapper.Map<Product>(updateProduct);
            productMap.Id = id;
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _productService.UpdateProduct(id, productMap);
                return Ok();

            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _productService.GetProductById(id);
            if(product != null)
            {
                _productService.DeleteProduct(id);
                return Ok();

            }
            return NotFound();
        }


    }
}
