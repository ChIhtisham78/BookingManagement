using Shaghaf.Core.Entities.IdentityEntities;
using System.Threading.Tasks;

namespace Shaghaf.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model); // Method for user registration.
        Task<AuthModel> LoginAsync(LoginModel model); // Method for user login.
        Task<string> AddRoleAsync(AddRoleModel model); // Method to add a role to a user.
        Task<bool> SendVerificationCodeAsync(string phoneNumber); // Method to send verification code.
        Task<bool> VerifyCodeAsync(string phoneNumber, string code); // Method to verify the code.
        Task<bool> ResetPasswordAsync(string phoneNumber, string newPassword, string confirmPassword); // Method to reset password with confirmation.
    }
}
