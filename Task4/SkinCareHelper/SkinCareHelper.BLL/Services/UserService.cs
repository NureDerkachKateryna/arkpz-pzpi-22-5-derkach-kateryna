using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.BLL.Validators;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IBanRepository _banRepository;

        private readonly ILogger<UserService> _logger;

        private readonly IMapper _mapper;

        private readonly IValidator<UserDto> _validator;

        public UserService(IUserRepository userRepository, IBanRepository banRepository, ILogger<UserService> logger, IMapper mapper)
        {            
            _userRepository = userRepository;
            _banRepository = banRepository;
            _logger = logger;
            _mapper = mapper;
            _validator = new UserValidator();
        }
        public async Task AddUserAsync(UserDto userDto)
        {
            try
            {
                if(!this._validator.Validate(userDto).IsValid)
                {
                    throw new ArgumentException("User is not valid");
                }

                User user = new User();

                this._mapper.Map(userDto, user);

                await this._userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("User id must not be null or empty");
            }

            try
            {
                await this._userRepository.DeleteUserAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            try
            {
                List<User> user = new List<User>();

                user = await this._userRepository.GetAllUsersAsync(x => !string.IsNullOrEmpty(x.Id));

                List<UserDto> userDtos = new List<UserDto>();

                this._mapper.Map(user, userDtos);

                return userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException("Email must not be null");
                }

                User user = new User();

                user = (await this._userRepository.GetAllUsersAsync(x => email.Equals(x.Email))).First();

                UserDto userDto = new UserDto();

                this._mapper.Map(user, userDto);

                return userDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }            
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("User id must not be null or empty");
                }

                User user = new User();

                user = await this._userRepository.GetUserAsync(x => x.Id.Equals(userId));

                UserDto userDto = new UserDto();

                this._mapper.Map(user, userDto);

                return userDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            try
            {
                if (!this._validator.Validate(userDto).IsValid || string.IsNullOrEmpty(userDto.Id))
                {
                    throw new ArgumentNullException("User is not valid");
                }

                var existingUser = await this._userRepository.GetUserAsync(u => u.Id.Equals(userDto.Id));

                existingUser.UserName = userDto.DisplayName;
                existingUser.Email = userDto.Email;
                existingUser.UserName = userDto.UserName;
                existingUser.SkinType = (SkinType)userDto.SkinType;
                existingUser.SkinIssue = (SkinIssue)userDto.SkinIssue;

                await this._userRepository.UpdateUserAsync(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }
    }
}
