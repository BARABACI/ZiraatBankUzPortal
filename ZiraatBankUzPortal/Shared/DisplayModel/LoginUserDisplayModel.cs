﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.DisplayModel
{
    public class LoginUserDisplayModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? EmployeeId  { get; set; }
        public string? RoleName { get; set; }
        public string? AccessToken { get; set; }
    }
}
