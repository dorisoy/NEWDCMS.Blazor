using System.ComponentModel.DataAnnotations;

namespace DCMS.Application.Requests.Identity
{
    public class TokenRequest
    {
        //[Required]
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }

        public string LoginType { get; set; }
        public string Captcha { get; set; }
        public bool AutoLogin { get; set; }
    }
}