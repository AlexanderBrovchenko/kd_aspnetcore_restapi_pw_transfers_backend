using AutoMapper;
using kd_pw_transfers_backend.Models;
using kd_pw_transfers_backend.Resources;
using kd_pw_transfers_backend.Services;
using kd_pw_transfers_backend.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace kd_pw_transfers_backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private IPublicService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IPublicService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("HelloWorld")]
        public string Index()
        {
            return "This is my default action...";
        }

        [AllowAnonymous]
        [HttpPost("sessions/create")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            User user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            string tokenString = TokenCreator.CreateToken(user);

            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            User user = _mapper.Map<User>(model);

            try
            {
                user = _userService.CreateUser(user, model.Password);
                string tokenString = TokenCreator.CreateToken(user);

                return Ok(new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Token = tokenString
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("briefinfo")]
        public IActionResult UserInfo()
        {
            try
            {
                int userid = Int32.Parse(User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value);
                User user = _userService.GetById(userid);
                int balance = _userService.UserBalance(user);
                return Ok(new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Balance = balance
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Authorize]
        [HttpGet("users/list")]
        public IActionResult GetOthers()
        {
            try
            {
                int userid = Int32.Parse(User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value);
                IEnumerable<User> others = _userService.GetOthers(userid);
                var model = _mapper.Map<IEnumerable<User>, IList<UserForOthers>>(others);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("users/{id}")]
        public IActionResult GetById(int id)
        {
            User user = _userService.GetById(id);
            return Ok(user);
        }
    }
}
