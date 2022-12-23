using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FarmasiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CartController(ICartService cartService, IMapper mapper, IUserService userService)
        {
            _cartService = cartService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public Task<List<Cart>> GetAllCart(){
            var cartList = _cartService.GetAllCart();
            
            return cartList;
        }
        [HttpGet("{id}")]
        public Task<Cart> GetCartById(Guid id)
        {
            var cart = _cartService.GetCartById(id);
            return cart;
        }
        [HttpPost]
        public IActionResult AddCart(CartDTO addCart)
        {
            var cartMap = _mapper.Map<Cart>(addCart);
            cartMap.Id = Guid.NewGuid();
            
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cartService.AddCart(cartMap);
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(Guid userId,Guid id, CartDTO updateCart)
        {
            var cartMap = _mapper.Map<Cart>(updateCart);
            cartMap.Id = id;
            var user = _userService.GetUserById(userId).Result;
            user.Cart = cartMap;
            if (user.Cart.Id != null)
            {
                
                _cartService.UpdateCart(id, cartMap);
                _userService.UpdateUser(userId, user);

                return Ok();

            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteCart(Guid userId,Guid id)
        {
            var product = _cartService.GetCartById(id);
            var user = _userService.GetUserById(userId).Result;
            if (product != null)
            {
                _cartService.DeleteCart(id);
                user.Cart = new Cart(){
                    Id = Guid.NewGuid()
                };
                _userService.UpdateUser(userId, user);
                return Ok();

            }
            return NotFound();
        }

    }
}
