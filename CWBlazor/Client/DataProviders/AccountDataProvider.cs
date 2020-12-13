using System.Net.Http;
using System.Threading.Tasks;
using CWBlazor.Shared.Contracts.Wrappers;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace CWBlazor.Client.DataProviders
{
    public class AccountDataProvider : RestApiDataProviderBase, IAccountDataProvider
    {
        public AccountDataProvider(
            HostInfo hostInfo,
            IJSRuntime jsRuntime,
            NavigationManager navigationManager,
            HttpClient http)
            : base(hostInfo, jsRuntime, navigationManager, http)
        {
        }

        public async Task<UniversalResponse<AuthSuccessResponse>> Register(UserRegistrationRequest request)
        {
            var response = await GetResponseAsync("api/Account/Register", HttpMethod.Post, request);
            return await WrapResult(response);
        }

        public async Task<UniversalResponse<AuthSuccessResponse>> Login(UserLoginRequest request)
        {
            var response = await GetResponseAsync("api/Account/Login", HttpMethod.Post, request);
            return await WrapResult(response);
        }

        public async Task<UniversalResponse<bool>> RecoverPassword(RecoverPasswordRequest request)
        {
            var response = await GetResponseAsync("api/Account/RecoverPassword", HttpMethod.Post, request);
            return await WrapRecoverPasswordResponse(response);
        }

        public async Task<UniversalResponse<AuthSuccessResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var response = await GetResponseAsync("api/Account/ResetPassword", HttpMethod.Post, request);
            return await WrapResult(response);
        }

        public async Task<UniversalResponse<AuthSuccessResponse>> ChangePassword(ChangePasswordRequest request)
        {
            var response = await GetResponseAsync("api/Account/ChangePassword", HttpMethod.Post, request);
            return await WrapResult(response);
        }

        private async Task<UniversalResponse<bool>> WrapRecoverPasswordResponse(HttpResponseMessage response)
        {
            var result = new UniversalResponse<bool>();
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                result.Value = true;
            }
            else
            {
                result.IsSuccess = false;
                result.Error = JsonConvert.DeserializeObject<ErrorResponse>(content);
            }

            return result;
        }

        private async Task<UniversalResponse<AuthSuccessResponse>> WrapResult(HttpResponseMessage response)
        {
            var result = new UniversalResponse<AuthSuccessResponse>();
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                result.Value = JsonConvert.DeserializeObject<AuthSuccessResponse>(content);
            }
            else
            {
                result.IsSuccess = false;
                result.Error = JsonConvert.DeserializeObject<ErrorResponse>(content);
            }

            return result;
        }
    }
}