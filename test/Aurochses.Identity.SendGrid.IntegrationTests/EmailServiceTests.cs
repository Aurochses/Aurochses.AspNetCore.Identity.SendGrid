using System.Threading.Tasks;
using Aurochses.Identity.SendGrid.IntegrationTests.Fakes;
using Aurochses.Testing;
using Xunit;

namespace Aurochses.Identity.SendGrid.IntegrationTests
{
    public class EmailServiceTests : IClassFixture<EmailServiceFixture>
    {
        private readonly EmailServiceFixture _fixture;

        public EmailServiceTests(EmailServiceFixture fixture)
        {
            _fixture = fixture;
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

        [Fact]
        public async Task SendEmailConfirmationTokenAsync_Success()
        {
            // Arrange
            var user = GetApplicationUser(nameof(SendEmailConfirmationTokenAsync_Success));

            // Act
            var sendResult = await _fixture.EmailService.SendEmailConfirmationTokenAsync(user, "http://www.example.com");

            // Assert
            Assert.True(sendResult.Succeeded);
        }

        [Fact]
        public async Task SendPasswordResetTokenAsync_Success()
        {
            // Arrange
            var user = GetApplicationUser(nameof(SendPasswordResetTokenAsync_Success));

            // Act
            var sendResult = await _fixture.EmailService.SendPasswordResetTokenAsync(user, "http://www.example.com");

            // Assert
            Assert.True(sendResult.Succeeded);
        }

        [Fact]
        public async Task SendTwoFactorTokenAsync_Success()
        {
            // Arrange
            var user = GetApplicationUser(nameof(SendTwoFactorTokenAsync_Success));

            // Act
            var sendResult = await _fixture.EmailService.SendTwoFactorTokenAsync(user, "TestToken");

            // Assert
            Assert.True(sendResult.Succeeded);
        }
    }
}