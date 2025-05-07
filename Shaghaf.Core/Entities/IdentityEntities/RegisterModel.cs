using System.ComponentModel.DataAnnotations;


namespace Shaghaf.Core.Entities.IdentityEntities
{
    public class RegisterModel
    {
        [Required, StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        [Required, StringLength(256)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
