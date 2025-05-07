using System.ComponentModel.DataAnnotations;


namespace Shaghaf.Core.Entities.IdentityEntities
{
    public class LoginModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
