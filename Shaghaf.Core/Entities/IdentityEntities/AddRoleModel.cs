
using System.ComponentModel.DataAnnotations;


namespace Shaghaf.Core.Entities.IdentityEntities
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
