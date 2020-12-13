using System.Threading.Tasks;
using CWBlazor.Shared.Models;

namespace CWBlazor.Client.DataProviders
{
    public interface IAccountDataProvider
    {
        public Task<UniversalResponse<AuthSuccessResponse>> Register(UserRegistrationRequest request);

        public Task<UniversalResponse<AuthSuccessResponse>> Login(UserLoginRequest request);

        public Task<UniversalResponse<bool>> RecoverPassword(RecoverPasswordRequest request);

        public Task<UniversalResponse<AuthSuccessResponse>> ResetPassword(ResetPasswordRequest request);

        public Task<UniversalResponse<AuthSuccessResponse>> ChangePassword(ChangePasswordRequest request);

        public Task<string> GetToken();

        public Task<bool> UpdateToken();
    }
}