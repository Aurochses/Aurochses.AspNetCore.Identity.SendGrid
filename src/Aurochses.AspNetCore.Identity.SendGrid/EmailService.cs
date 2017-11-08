using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aurochses.AspNetCore.Identity.SendGrid
{
    /// <summary>
    /// Class EmailService.
    /// </summary>
    /// <seealso cref="Aurochses.AspNetCore.Identity.IEmailService" />
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;

        private readonly SendGridOptions _sendGridOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService" /> class.
        /// </summary>
        /// <param name="sendGridClient">The send grid client.</param>
        /// <param name="sendGridOptions">The send grid options.</param>
        public EmailService(ISendGridClient sendGridClient, IOptions<SendGridOptions> sendGridOptions)
        {
            _sendGridClient = sendGridClient;

            _sendGridOptions = sendGridOptions.Value;
        }

        /// <summary>
        /// Send Email Confirmation Token
        /// </summary>
        /// <param name="user">The User.</param>
        /// <param name="callbackUrl">The callback url.</param>
        /// <returns>SendResult</returns>
        public async Task<SendResult> SendEmailConfirmationTokenAsync(IApplicationUser user, string callbackUrl)
        {
            return await Send(_sendGridOptions.EmailConfirmationTokenTemplateId, user, "%callbackUrl%", callbackUrl);
        }

        /// <summary>
        /// Send Password Reset Token
        /// </summary>
        /// <param name="user">The User.</param>
        /// <param name="callbackUrl">The callback url.</param>
        /// <returns>SendResult</returns>
        public async Task<SendResult> SendPasswordResetTokenAsync(IApplicationUser user, string callbackUrl)
        {
            return await Send(_sendGridOptions.PasswordResetTokenTemplateId, user, "%callbackUrl%", callbackUrl);
        }

        /// <summary>
        /// Send Two Factor Token
        /// </summary>
        /// <param name="user">The User.</param>
        /// <param name="token">The token.</param>
        /// <returns>SendResult</returns>
        public async Task<SendResult> SendTwoFactorTokenAsync(IApplicationUser user, string token)
        {
            return await Send(_sendGridOptions.TwoFactorTokenTemplateId, user, "%token%", token);
        }

        private async Task<SendResult> Send(string templateId, IApplicationUser user, string substitutionKey, string substitutionValue)
        {
            var message = new SendGridMessage();

            message.SetFrom(new EmailAddress(_sendGridOptions.FromEmailAddress, _sendGridOptions.FromEmailName));

            message.AddTo(new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}"));

            message.AddSubstitution("%UserName%", user.UserName);
            message.AddSubstitution("%Email%", user.Email);
            message.AddSubstitution("%FirstName%", user.FirstName);
            message.AddSubstitution("%LastName%", user.LastName);

            message.AddSubstitution(substitutionKey, substitutionValue);

            message.SetTemplateId(templateId);

            var response = await _sendGridClient.SendEmailAsync(message);

            return response.StatusCode == HttpStatusCode.Accepted
                ? SendResult.Success
                : SendResult.Failed(response);
        }
    }
}