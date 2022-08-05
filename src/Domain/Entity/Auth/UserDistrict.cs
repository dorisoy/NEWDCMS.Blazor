namespace DCMS.Domain.Auth
{
    /// <summary>
    /// 用于表示用于片区映射
    /// </summary>
    public partial class UserDistricts : AuditableEntity<int>
    {
        public int UserId { get; set; }
        public int DistrictId { get; set; }
    }
}
