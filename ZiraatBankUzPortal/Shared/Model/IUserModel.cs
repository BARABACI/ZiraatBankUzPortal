namespace ZiraatBankUzPortal.Shared.Model
{
    public interface IUserModel
    {
        int Id { get; set; }
        string Firstname { get; set; }
        string CellPhone { get; set; }   
        string IPT { get; set; }
        string Lastname { get; set; }
        int LocationId { get; set; }
        int PositionId { get; set; }
        DateTime RecordDate { get; set; }
        string RecordStatus { get; set; }
        string RecordUser { get; set; }
        int TitleId { get; set; }
        DateTime? DateofBirth { get; set; }
        byte[] Picture { get; set; }
    }
}