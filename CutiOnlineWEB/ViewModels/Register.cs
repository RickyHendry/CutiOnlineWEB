﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.ViewModels
{
    public class Register
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 2;
    }
}
