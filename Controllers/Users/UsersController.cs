// controlador para los usuarios

using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hecotoBackend.Services;
using hecotoBackend.DTOs;
using System.Threading.Tasks;
using hecotoBackend.Models;

namespace hecotoBackend.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuthServices _authServices;

        public UsersController(AuthServices authServices)
        {
            _authServices = authServices ?? throw new ArgumentNullException(nameof(authServices));
        }

    }
}

