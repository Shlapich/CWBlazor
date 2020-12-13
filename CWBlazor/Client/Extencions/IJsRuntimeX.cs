using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace CWBlazor.Client.Extencions
{
    public static class IJsRuntimeX
    {
        private const string GetLocalStorageItem = "localStorage.getItem";
        private const string SetLocalStorageItem = "localStorage.setItem";
        private const string RemoveLocalStorageItem = "localStorage.removeItem";
        private const string UserItem = "userItem";

        public static async Task<UserInfo> GetUserInfoAsync(this IJSRuntime jsRuntime)
        {
            var json = await jsRuntime.InvokeAsync<string>(GetLocalStorageItem, UserItem).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            UserInfo result;
            try
            {
                return JsonConvert.DeserializeObject<UserInfo>(json);
            }
            catch (Exception e)
            {
                await RemoveUserInfoAsync(jsRuntime);
            }
            return null;
        }

        public static async Task SetUserInfoAsync(this IJSRuntime jsRuntime, UserInfo userInfo)
        {
            var json = JsonConvert.SerializeObject(userInfo);
            await jsRuntime.InvokeVoidAsync(SetLocalStorageItem, UserItem, json).ConfigureAwait(false);
        }

        public static async Task RemoveUserInfoAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(RemoveLocalStorageItem, UserItem).ConfigureAwait(false);
        }

        public static async Task RemoveUserTokenAsync(this IJSRuntime jsRuntime)
        {
            var result = await GetUserInfoAsync(jsRuntime);
            if (result == null)
            {
                return;
            }

            result.Token = null;

            await SetUserInfoAsync(jsRuntime, result);
        }
    }
}