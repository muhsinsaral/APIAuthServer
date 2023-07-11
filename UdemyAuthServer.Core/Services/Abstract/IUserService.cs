using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using SharedLibrary.Dtos;
using UdemyAuthServer.Core.DTOs;

namespace UdemyAuthServer.Core.Services.Abstract
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<NoContent>> CreateUserRolesAsync(string userName);
    }
}