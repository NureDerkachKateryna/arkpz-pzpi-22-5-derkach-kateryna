﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Photos
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = null!;

        public string ApiKey { get; set; } = null!;

        public string ApiSecret { get; set; } = null!;
    }
}
