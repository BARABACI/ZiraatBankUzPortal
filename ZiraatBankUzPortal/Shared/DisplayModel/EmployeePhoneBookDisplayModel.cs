using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.DisplayModel
{
    public class EmployeePhoneBookDisplayModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public string IPT { get; set; }
        public string CellPhone { get; set; }
        public byte[] Picture { get; set; }
    }
}
