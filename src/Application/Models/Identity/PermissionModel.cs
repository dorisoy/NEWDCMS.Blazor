using System.Collections.Generic;

namespace DCMS.Application.Models.Identity
{
    public class PermissionModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaimModel> RoleClaims { get; set; }
    }
}