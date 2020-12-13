namespace CWBlazor.Domain.Enums
{
    /// <summary>
    /// Describes type of security audit.
    /// </summary>
    public enum SecurityAuditType
    {
        /// <summary>
        /// Log In Failure.
        /// </summary>
        LoginFailure = 1,

        /// <summary>
        /// Forgotten password reset.
        /// </summary>
        ResetPassword = 2,

        /// <summary>
        /// Password change.
        /// </summary>
        ChangePassword = 3,

        /// <summary>
        /// Password change.
        /// </summary>
        ForgotPassword = 4,

        /// <summary>
        /// Register failure.
        /// </summary>
        RegisterFailure = 5,

        /// <summary>
        /// Login from another device.
        /// </summary>
        AnotherDeviceLogin = 6,

        /// <summary>
        /// Uncategorized reason.
        /// </summary>
        Other = 99,
    }
}