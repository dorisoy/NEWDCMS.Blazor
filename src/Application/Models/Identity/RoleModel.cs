using System.ComponentModel.DataAnnotations;

namespace DCMS.Application.Models.Identity
{
    public class RoleModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}