using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Shared.DisplayModel
{
    public class DisplayUserModel
    {
        public string? CellPhone { get; set; }
        public string? Firstname { get; set; }
        public int Id { get; set; }
        public string? IPT { get; set; }
        public string? Lastname { get; set; }
        public string? Location { get; set; }
        public int LocationId { get; set; }
        public string? Position { get; set; }
        public int PositionId { get; set; }
        public DateTime RecordDate { get; set; }
        public string? RecordStatus { get; set; }
        public string? RecordUser { get; set; }
        public string? Title { get; set; }
        public int TitleId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public byte[]? Picture { get; set; }

    }
}
