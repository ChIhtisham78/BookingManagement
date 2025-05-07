using System.ComponentModel.DataAnnotations;

namespace Shaghaf.Core.Entities.IdentityEntities
{
    public class VerificationRequestModel
    {
        [Required(ErrorMessage = "Phone number is required.")]
      //  [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } // User's phone number where the verification code will be sent.
    }
}
