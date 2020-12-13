namespace CWBlazor.Shared
{
    public static class Constants
    {
        public static class WebFormats
        {
            /// <summary>
            /// "yyyy-MM-ddTHH:mm:ss".
            /// </summary>
            public const string Iso8601DateFormat = "s";
        }

        public static class Routes
        {
            public const string ApiController = "api/[controller]";
        }

        public static class CorsPolicyName
        {
            public const string AllowAny = nameof(AllowAny);
        }

        /// <summary>
        /// Route names (frontend rotes).
        /// </summary>
        public static class RouteNames
        {
            /// <summary>
            /// Default controllers route.
            /// </summary>
            public const string Default = nameof(Default);

            /// <summary>
            /// /reset-password.
            /// </summary>
            public const string ResetPassword = "reset-password";
        }
    }
}