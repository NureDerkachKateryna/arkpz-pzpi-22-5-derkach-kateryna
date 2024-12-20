using AutoMapper;
using Microsoft.Extensions.Logging;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Services
{
    public class BanService : IBanService
    {
        private readonly IBanRepository _banRepository;

        private readonly IUserRepository _userRepository;

        private readonly ILogger<BanService> _logger;

        private readonly IMapper _mapper;

        public BanService(IBanRepository banRepository, IUserRepository userRepository, ILogger<BanService> logger, IMapper mapper)
        {
            _banRepository = banRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddBanAsync(BanDto banDto)
        {
            try
            {
                Ban ban = new Ban();

                this._mapper.Map(banDto, ban);

                await this._banRepository.AddBanAsync(ban);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task DeleteBanAsync(int banId)
        {           
            try
            {
                if (banId <= 0)
                {
                    throw new ArgumentNullException("Ban id must be greater than 0");
                }

                await this._banRepository.DeleteBanAsync(banId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<BanDto>> GetAllBansAsync()
        {
            try
            {
                List<Ban> bans = new List<Ban>();

                bans = await this._banRepository.GetAllBansAsync(x => x.BanId > 0);

                List<BanDto> bansDto = new List<BanDto>();

                this._mapper.Map(bans, bansDto);

                return bansDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }        
    }
}
