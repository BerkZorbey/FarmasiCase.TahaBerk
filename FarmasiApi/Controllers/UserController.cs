using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace FarmasiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper, ICartService cartService)
        {
            _userService = userService;
            _mapper = mapper;
            _cartService = cartService;
        }
        [HttpGet]
        public Task<User> GetUserById(Guid id)
        {
            var user =_userService.GetUserById(id);
            return user;
        }

        [HttpPost]
        public IActionResult AddUser(UserDTO user)
        {
            var userModel = _mapper.Map<User>(user);
            userModel.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                Cart userCart = new Cart
                {
                    Id = Guid.NewGuid()
                };
                userModel.Cart = userCart;
                _userService.AddUser(userModel);
                _cartService.AddCart(userCart);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateUser(Guid id,User user)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                _userService.UpdateUser(id, user);
                return Ok();
            }
            return BadRequest();
        }
    }
}
