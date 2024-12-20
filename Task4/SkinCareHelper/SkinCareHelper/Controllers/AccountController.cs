using AutoMapper;
using BLL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.Services.Abstractions;
using SkinCareHelper.ViewModels.Products;

namespace SkinCareHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly IUserService _userService;

        private readonly IUserAccessor _userAccessor;

        private readonly IMapper _mapper;

        public AccountController(
            IAccountService accountService,
            IUserService userService,
            IUserAccessor userAccessor,
            IMapper mapper)
        {
            _accountService = accountService;
            _userService = userService;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            try
            {
                var token = await _accountService.LoginAsync(user.Email, user.Password);

                UserDto userInfo = await this._userService.GetUserByEmailAsync(user.Email);

                AuthResponseViewModel authResponse = new AuthResponseViewModel();
                authResponse.Token = token.Replace("\"", string.Empty);
                authResponse.User = new UserViewModel();
                this._mapper.Map(userInfo, authResponse.User);

                return Ok(authResponse);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                User user = new User();

                this._mapper.Map(model, user);

                if (await _accountService.RegistrateAsync(user, model.Password))
                {
                    return Ok();
                }

                return BadRequest("User with such username already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                string userId = this._userAccessor.GetUserId();

                UserDto user = await this._userService.GetUserByIdAsync(userId);

                UserViewModel userViewModel = new UserViewModel();

                this._mapper.Map(user, userViewModel);

                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot get current user");
            }
        }

        [Authorize]
        [HttpPut("{email}/{oldPassword}/{newPassword}")]
        public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                bool result = await _accountService.ChangePasswordAsync(email, oldPassword, newPassword);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot change password");
            }
        }
    }
}