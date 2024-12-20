using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public SkinTypeDto SkinType { get; set; } 

        public SkinIssueDto SkinIssue { get; set; } 

        public List<SkincareRoutineDto> Routines { get; set; } = new List<SkincareRoutineDto>();

        public List<BanDto> Bans { get; set; } = new List<BanDto>();
    }
}
