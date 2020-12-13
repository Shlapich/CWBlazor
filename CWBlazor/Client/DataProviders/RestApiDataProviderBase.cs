using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CWBlazor.Client.Exceptions;
using CWBlazor.Client.Extencions;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace CWBlazor.Client.DataProviders
{
    /// <summary>
    /// Provides base methods to access server.
    /// </summary>
    public abstract class RestApiDataProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiDataProviderBase"/> class.
        /// </summary>
        /// <param name="hostInfo">Host info.</param>
        /// <param name="jsRuntime"></param>
        /// <param name="navigationManager">Navigation manager.</param>
        /// <param name="http">Http client.</param>
        protected RestApiDataProviderBase(HostInfo hostInfo, IJSRuntime jsRuntime, NavigationManager navigationManager,
            HttpClient http)
        {
            HostInfo = hostInfo;
            JsRuntime = jsRuntime;
            this.NavigationManager = navigationManager;
            Http = http;
        }

        /// <summary>
        /// Data provider base uri.
        /// </summary>
        protected HostInfo HostInfo { get; }

        /// <summary>
        /// JsRuntime
        /// </summary>
        protected IJSRuntime JsRuntime { get; }

        protected NavigationManager NavigationManager { get; }
        protected HttpClient Http { get; }

        private async Task<HttpRequestMessage> GetRequestMessage(string endpoint, HttpMethod method,
            IEnumerable<KeyValuePair<string, object>> parameters, object body)
        {
            var result = new HttpRequestMessage() { RequestUri = BuildUri(endpoint) };
            var token = await GetToken();
            if (token != null)
            {
                result.Headers.Add("Authorization", $"Bearer {token}");
            }

            switch (method.Method)
            {
                case "GET":
                    result.Method = HttpMethod.Get;
                    result.RequestUri = BuildUri(endpoint, parameters);
                    break;
                case "POST":
                    result.Method = HttpMethod.Post;
                    result.RequestUri = BuildUri(endpoint, parameters);
                    var content = WrapBody(body);
                    result.Content = content;
                    break;
                case "PUT":
                case "DELETE":
                case "HEAD":
                case "OPTIONS":
                case "PATCH":
                case "MERGE":
                case "COPY":
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }

            return result;
        }

        private Uri BuildUri(string endpoint, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var uri = BuildUri(endpoint);
            var builder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var (key, value) in parameters)
            {
                query[key] = value.ToString();
            }

            builder.Query = query.ToString() ?? string.Empty;
            return new Uri(builder.ToString());
        }

        private Uri BuildUri(string endpoint)
        {
            var clearEndpoint = endpoint.TrimStart('/').TrimEnd('/');
            return new Uri($"{HostInfo.BaseAddress}{clearEndpoint}/");
        }

        /// <summary>
        /// Get API call response.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/>.</param>
        /// <returns>API Call response.</returns>
        protected virtual async Task<HttpResponseMessage> GetResponseAsync(HttpRequestMessage request)
        {
            var response = await Http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        await JsRuntime.RemoveUserTokenAsync();
                        var token = await GetToken();
                        if (token == null)
                        {
                            NavigationManager.NavigateTo("login");
                            return response;
                        }

                        return await GetResponseAsync(request);
                    case HttpStatusCode.BadRequest:
                        break;
                    default:
                        throw new RestApiDataSourceException(GetExceptionMessage(request.RequestUri, request,
                            response));
                }
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            if (responseContent == "[]")
            {
                response.Content = WrapBody(string.Empty);
            }

            return response;
        }

        /// <summary>
        /// Get API Call response.
        /// </summary>
        /// <param name="endpoint">server endpoint.</param>
        /// <param name="method">Http method.</param>
        /// <param name="paramsDto">Parameters to use in request.</param>
        /// <param name="body">Body.</param>
        /// <returns>API call response.</returns>
        protected virtual async Task<HttpResponseMessage> GetResponseAsync(string endpoint, HttpMethod method,
            object paramsDto,
            object body)
        {
            var requestParams = paramsDto.ConvertDtoToDictionaryOfParameters();
            var request = await GetRequestMessage(endpoint, method, requestParams, body);

            return await GetResponseAsync(request);
        }

        /// <summary>
        /// Get API Call response.
        /// </summary>
        /// <param name="endpoint">server endpoint.</param>
        /// <param name="method">Http method.</param>
        /// /// <param name="body">Body.</param>
        /// <returns>API call response.</returns>
        protected virtual async Task<HttpResponseMessage> GetResponseAsync(string endpoint, HttpMethod method, object body)
        {
            var request = await GetRequestMessage(endpoint, method, new List<KeyValuePair<string, object>>(), body);
            return await GetResponseAsync(request);
        }

        public async Task<string> GetToken()
        {
            var userInfo = await JsRuntime.GetUserInfoAsync();
            if (!string.IsNullOrWhiteSpace(userInfo?.Token)) return userInfo.Token;

            if (string.IsNullOrWhiteSpace(userInfo?.RefreshToken))
            {
                return null;
            }

            if (await UpdateToken())
            {
                userInfo = await JsRuntime.GetUserInfoAsync();
            }

            return userInfo.Token;
        }

        public async Task<bool> UpdateToken()
        {
            var userInfo = await JsRuntime.GetUserInfoAsync();
            var response = await RefreshToken(userInfo.RefreshToken);
            if (!response.IsSuccessStatusCode)
            {
                await JsRuntime.RemoveUserInfoAsync();
                return false;
            }

            var result =
                JsonConvert.DeserializeObject<AuthSuccessResponse>(await response.Content.ReadAsStringAsync());
            await JsRuntime.SetUserInfoAsync(new UserInfo
                { Token = result.Token, RefreshToken = result.RefreshToken, Email = userInfo.Email });

            return true;
        }

        private async Task<HttpResponseMessage> RefreshToken(string refreshToken)
        {
            var result = new HttpRequestMessage();
            result.Method = HttpMethod.Post;
            result.RequestUri = BuildUri($"/api/Account/TokenRefresh/");
            var content = WrapBody(new { refreshToken = refreshToken });
            result.Content = content;

            return await Http.SendAsync(result);
        }

        private StringContent WrapBody(object body) =>
            new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8,
                "application/json");

        private string GetExceptionMessage(Uri uri, HttpRequestMessage request, HttpResponseMessage response)
        {
            var parameters = "";
            if (request.RequestUri is not null)
            {
                var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
                parameters = string.Join('&',
                    query.AllKeys.Select(t => $"{t}={query[t]}"));
            }

            return @$"
Data Provider `{uri}` responded with error code: {(int) response.StatusCode}.
Parameters: {parameters}";
        }
    }
}
