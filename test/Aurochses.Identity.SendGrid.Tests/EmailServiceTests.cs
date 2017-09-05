using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aurochses.Identity.SendGrid.Tests.Fakes;
using Aurochses.Testing;
using Microsoft.Extensions.Options;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using Xunit;

namespace Aurochses.Identity.SendGrid.Tests
{
    public class EmailServiceTests
    {
        private static readonly SendGridOptions SendGridOptions = new SendGridOptions
        {
            EmailConfirmationTokenTemplateId = "TestEmailConfirmationTokenTemplateId",
            PasswordResetTokenTemplateId = "TestPasswordResetTokenTemplateId",
            TwoFactorTokenTemplateId = "TestTwoFactorTokenTemplateId"
        };

        private readonly Mock<ISendGridClient> _mockSendGridClient;

        private readonly EmailService _emailService;

        public EmailServiceTests()
        {
            _mockSendGridClient = new Mock<ISendGridClient>(MockBehavior.Strict);

            var mockSendGridOptions = new Mock<IOptions<SendGridOptions>>(MockBehavior.Strict);
            mockSendGridOptions
                .SetupGet(x => x.Value)
                .Returns(SendGridOptions);

            _emailService = new EmailService(_mockSendGridClient.Object, mockSendGridOptions.Object);
        }

        private static IApplicationUser GetApplicationUser(string methodName)
        {
            var email = typeof(EmailServiceTests).GenerateEmail(methodName);

            return new FakeApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };
        }

        private void SetupSendGridClientSendEmailAsync(SendGridOptions sendGridOptions, IApplicationUser user, string substitutionKey, string substitutionValue, string templateId)
        {
            _mockSendGridClient
                .Setup(
                    x => x.SendEmailAsync(
                        It.Is<SendGridMessage>(
                            message =>
                                message.From.Email == sendGridOptions.FromEmailAddress
                                && message.From.Name == sendGridOptions.FromEmailName
                                && message.Personalizations.Any(
                                    p => p.Tos.Any(
                                        t => t.Email == user.Email
                                             && t.Name == $"{user.FirstName} {user.LastName}"
                                    )
                                )
                                && message.Personalizations.Any(
                                    p => p.Substitutions.Any(
                                             s => s.Key == "%UserName%"
                                                  && s.Value == user.UserName
                                         )
                                         && p.Substitutions.Any(
                                             s => s.Key == "%Email%"
                                                  && s.Value == user.Email
                                         )
                                         && p.Substitutions.Any(
                                             s => s.Key == "%FirstName%"
                                                  && s.Value == user.FirstName
                                         )
                                         && p.Substitutions.Any(
                                             s => s.Key == "%LastName%"
                                                  && s.Value == user.LastName
                                         )
                                         && p.Substitutions.Any(
                                             s => s.Key == substitutionKey
                                                  && s.Value == substitutionValue
                                         )
                                )
                                && message.TemplateId == templateId
                        ),
                        default(CancellationToken)
                    )
                )
                .ReturnsAsync(() => new Response(HttpStatusCode.OK, null, null))
                .Verifiable();
        }

        [Fact]
        public async Task SendEmailConfirmationTokenAsync_Success()
        {
            // Arrange
            var user = GetApplicationUser(nameof(SendEmailConfirmationTokenAsync_Success));
            const string callbackUrl = "TestCallbackUrl";

            SetupSendGridClientSendEmailAsync(SendGridOptions, user, "%callbackUrl%", callbackUrl, SendGridOptions.EmailConfirmationTokenTemplateId);

            // Act
            await _emailService.SendEmailConfirmationTokenAsync(user, callbackUrl);

            // Assert
            _mockSendGridClient.Verify();
        }

        [Fact]
        public async Task SendPasswordResetTokenAsync_Success()
        {
            // Arrange
            var user = GetApplicationUser(nameof(SendPasswordResetTokenAsync_Success));
            const string callbackUrl = "TestCallbackUrl";

            SetupSendGridClientSendEmailAsync(SendGridOptions, user, "%callbackUrl%", callbackUrl, SendGridOptions.PasswordResetTokenTemplateId);

            // Act
            await _emailService.SendPasswordResetTokenAsync(user, callbackUrl);

            // Assert
            _mockSendGridClient.Verify();
        }

        [Fact]
        public async Task SendTwoFactorTokenAsync_Success()
        {
            // Arrange
            var user = GetApplicationUser(nameof(SendTwoFactorTokenAsync_Success));
            const string token = "TestToken";

            SetupSendGridClientSendEmailAsync(SendGridOptions, user, "%token%", token, SendGridOptions.TwoFactorTokenTemplateId);

            // Act
            await _emailService.SendTwoFactorTokenAsync(user, token);

            // Assert
            _mockSendGridClient.Verify();
        }
    }
}