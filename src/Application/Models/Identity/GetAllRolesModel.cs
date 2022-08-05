using System.Collections.Generic;

namespace DCMS.Application.Models.Identity
{
    public class GetAllRolesModel
    {
        public IEnumerable<RoleModel> Roles { get; set; }
    }
}