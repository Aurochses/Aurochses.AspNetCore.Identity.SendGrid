using Xunit;

namespace Aurochses.Identity.SendGrid.Tests
{
    public class SendGridOptionsTests
    {
        private readonly SendGridOptions _sendGridOptions;

        public SendGridOptionsTests()
        {
            _sendGridOptions = new SendGridOptions();
        }

        [Fact]
        public void FromEmailAddress_Success()
        {
            // Arrange
            const string value = "fromEmailAddress";

            // Act
            _sendGridOptions.FromEmailAddress = value;

            // Assert
            Assert.Equal(value, _sendGridOptions.FromEmailAddress);
        }

        [Fact]
        public void FromEmailName_Success()
        {
            // Arrange
            const string value = "fromEmailName";

            // Act
            _sendGridOptions.FromEmailName = value;

            // Assert
            Assert.Equal(value, _sendGridOptions.FromEmailName);
        }

        [Fact]
        public void EmailConfirmationTokenTemplateId_Success()
        {
            // Arrange
            const string value = "emailConfirmationTokenTemplateId";

            // Act
            _sendGridOptions.EmailConfirmationTokenTemplateId = value;

            // Assert
            Assert.Equal(value, _sendGridOptions.EmailConfirmationTokenTemplateId);
        }

        [Fact]
        public void PasswordResetTokenTemplateId_Success()
        {
            // Arrange
            const string value = "passwordResetTokenTemplateId";

            // Act
            _sendGridOptions.PasswordResetTokenTemplateId = value;

            // Assert
            Assert.Equal(value, _sendGridOptions.PasswordResetTokenTemplateId);
        }

        [Fact]
        public void TwoFactorTokenTemplateId_Success()
        {
            // Arrange
            const string value = "twoFactorTokenTemplateId";

            // Act
            _sendGridOptions.TwoFactorTokenTemplateId = value;

            // Assert
            Assert.Equal(value, _sendGridOptions.TwoFactorTokenTemplateId);
        }
    }
}