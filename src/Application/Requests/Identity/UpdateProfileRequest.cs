using System.ComponentModel.DataAnnotations;

namespace DCMS.Application.Requests.Identity
{
    public class UpdateProfileRequest
    {
        [Required]
        public string UserRealName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}