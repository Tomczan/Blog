﻿using Blog.Domain.Models;

namespace Blog.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();

        Task<User?> GetById(string id);

        Task<User> Create(User user);

        //Task<bool> Login(string name, string password);
    }
}