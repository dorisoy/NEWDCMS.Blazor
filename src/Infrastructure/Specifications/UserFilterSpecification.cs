using DCMS.Infrastructure.Models.Identity;
using DCMS.Application.Specifications.Base;

namespace DCMS.Infrastructure.Specifications
{
    public class UserFilterSpecification : SpecificationBase<AppUser>
    {
        public UserFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.UserRealName.Contains(searchString)  || p.Email.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.UserName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}