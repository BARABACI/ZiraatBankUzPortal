using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.Model
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public int ParentMenuId { get; set; }
        public string PageLink { get; set; }
        public string MenuName { get; set; }
        public string IconName { get; set; }
        public string PageRoles { get; set; }
        public string RecordStatus { get; set; }
    }
}
