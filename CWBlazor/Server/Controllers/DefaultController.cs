using System.Collections.Generic;
using System.Security.Claims;
using CWBlazor.Shared.Contracts.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace CWBlazor.Server.Controllers
{
    /// <inheritdoc />
    public abstract class DefaultController : ControllerBase
    {
        private const string AccessDeniedMessage = "Access Denied.";
        private const string NotFoundMessage = "Not Found.";

        /// <summary>
        /// Provides current user id.
        /// </summary>
        protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <inheritdoc />
        public override OkObjectResult Ok(object value)
        {
            return base.Ok(value);
        }

        /// <inheritdoc />
        public override BadRequestObjectResult BadRequest(object error)
        {
            return base.BadRequest(ErrorResponseWrapper(error) ?? error);
        }

        /// <summary>
        /// Get Access Denied response.
        /// </summary>
        /// <returns>The access denied <see cref="ErrorResponse"/>.</returns>
        protected virtual ErrorResponse AccessDeniedResponse()
        {
            return new ErrorResponse(AccessDeniedMessage);
        }

        /// <summary>
        /// Get Not Found response.
        /// </summary>
        /// <returns>The not found <see cref="ErrorResponse"/>.</returns>
        protected virtual ErrorResponse NotFoundResponse()
        {
            return new ErrorResponse(NotFoundMessage);
        }

        /// <inheritdoc />
        public override ObjectResult StatusCode(int statusCode, object value)
        {
            return IsSuccessHttpStatusCode(statusCode)
                ? base.StatusCode(statusCode, new ApiResponse(value))
                : base.StatusCode(statusCode, ErrorResponseWrapper(value) ?? value);
        }

        private bool IsSuccessHttpStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }

        private ErrorResponse ErrorResponseWrapper(object obj)
        {
            switch (obj)
            {
                case ErrorResponse errorResponse:
                    return errorResponse;
                case ErrorsModel errorModel:
                    return new ErrorResponse(errorModel);
                case IEnumerable<string> errors:
                    return new ErrorResponse(errors);
                case string error:
                    return new ErrorResponse(error);
                default:
                    return null;
            }
        }
    }
}