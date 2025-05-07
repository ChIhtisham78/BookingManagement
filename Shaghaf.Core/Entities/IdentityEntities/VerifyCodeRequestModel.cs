using System.ComponentModel.DataAnnotations;

namespace Shaghaf.Core.Entities.IdentityEntities
{
    public class VerifyCodeRequestModel
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } // User's phone number for which the code needs to be verified.

        [Required(ErrorMessage = "Verification code is required.")]
        [StringLength(6, ErrorMessage = "The verification code must be 6 digits long.", MinimumLength = 6)]
        public string Code { get; set; } // The verification code that was sent to the user.
    }
}
