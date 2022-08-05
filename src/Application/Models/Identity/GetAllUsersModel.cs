using System.Collections.Generic;

namespace DCMS.Application.Models.Identity
{
    public class GetAllUsersModel
    {
        public IEnumerable<UserModel> Users { get; set; }
    }
}