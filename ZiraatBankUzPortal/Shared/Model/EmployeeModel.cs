namespace ZiraatBankUzPortal.Shared.Model
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int TitleId { get; set; }
        public int PositionId { get; set; }
        public int LocationId { get; set; }
        public string IPT { get; set; }
        public string CellPhone { get; set; }
        public string RecordUser { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;
        public string RecordStatus { get; set; }
        public DateTime? DateofBirth { get; set; }
        public byte[] Picture { get; set; }
    }
}
