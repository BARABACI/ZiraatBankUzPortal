using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.Dto
{
    public class EmployeeCreateDto
    {
        public string RegistrationNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int TitleId { get; set; }
        public int PositionId { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public string IPT { get; set; }
        public string CellPhone { get; set; }
        public string RecordUser { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;
        public DateTime? DateofBirth { get; set; }
        public byte[] Picture { get; set; }
    }
}
