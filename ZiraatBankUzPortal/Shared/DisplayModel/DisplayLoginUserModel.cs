using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.DisplayModel
{
    public class DisplayLoginUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserId  { get; set; }
        public string RoleName { get; set; }
        public string AccessToken { get; set; }
    }
}
