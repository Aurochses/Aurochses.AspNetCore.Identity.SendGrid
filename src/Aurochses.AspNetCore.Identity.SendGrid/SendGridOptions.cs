namespace Aurochses.AspNetCore.Identity.SendGrid
{
    /// <summary>
    /// Class SendGridOptions.
    /// </summary>
    public class SendGridOptions
    {
        /// <summary>
        /// Gets or sets from email address.
        /// </summary>
        /// <value>
        /// From email address.
        /// </value>
        public string FromEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the name of from email.
        /// </summary>
        /// <value>
        /// The name of from email.
        /// </value>
        public string FromEmailName { get; set; }

        /// <summary>
        /// Gets or sets the email confirmation token template identifier.
        /// </summary>
        /// <value>
        /// The email confirmation token template identifier.
        /// </value>
        public string EmailConfirmationTokenTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the password reset token template identifier.
        /// </summary>
        /// <value>
        /// The password reset token template identifier.
        /// </value>
        public string PasswordResetTokenTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the two factor token template identifier.
        /// </summary>
        /// <value>
        /// The two factor token template identifier.
        /// </value>
        public string TwoFactorTokenTemplateId { get; set; }
    }
}