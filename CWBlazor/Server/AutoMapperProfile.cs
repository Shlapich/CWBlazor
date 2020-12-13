using CWBlazor.Server.DTOs.Account;
using CWBlazor.Shared.Models;

namespace CWBlazor.Server
{
    /// <inheritdoc />
    public class AutoMapperProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// Set auto mapping maps.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<AuthenticationResultDto, AuthSuccessResponse>();
        }
    }
}