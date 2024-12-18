using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.ViewModels.Products;

namespace SkinCareHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            try
            {
                await this._userService.DeleteUserAsync(userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<UserDto> usersDtos = await this._userService.GetAllUsersAsync();

                List<UserViewModel> users = new List<UserViewModel>();

                this._mapper.Map(usersDtos, users);

                return Ok(users);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            try
            {
                UserDto userDto = await this._userService.GetUserByIdAsync(userId);

                UserViewModel user = new UserViewModel();

                this._mapper.Map(userDto, user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] UpdateUserViewModel updateUserViewModel)
        {
            try
            {
                UserDto user = new UserDto();

                this._mapper.Map(updateUserViewModel, user);

                user.Id = updateUserViewModel.Id;

                await this._userService.UpdateUserAsync(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
