﻿using Blog.Application.Dto;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUser(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] LoginDTO loginData)
        {
            if (string.IsNullOrEmpty(loginData.Login) || string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("login and password are required.");
            }

            var user = await _userService.CreateUser(loginData.Login, loginData.Password);

            return Created("User successfully created", user);
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO loginData)
        //{
        //    if (string.IsNullOrEmpty(loginData.Name) || string.IsNullOrEmpty(loginData.Password))
        //    {
        //        return BadRequest("login and password are required.");
        //    }

        //    var result = await _userService.Login(loginData.Name, loginData.Password);

        //    if (result)
        //    {
        //        return Ok("Logged successfully");
        //    }
        //    else
        //    {
        //        return Unauthorized("Inwalid login or password");
        //    }
        //}
    }
}