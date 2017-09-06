using Aurochses.Testing;
using Microsoft.Extensions.Options;
using SendGrid;

namespace Aurochses.Identity.SendGrid.IntegrationTests
{
    public class EmailServiceFixture : ConfigurationFixture
    {
        public EmailServiceFixture()
        {
            var sendGridClient = new SendGridClient(Configuration["SendGrid:ApiKey"]);

            var sendGridOptions = new SendGridOptions
            {
                FromEmailAddress = Configuration["Identity:SendGridOptions:FromEmailAddress"],
                FromEmailName = Configuration["Identity:SendGridOptions:FromEmailName"],
                EmailConfirmationTokenTemplateId = Configuration["Identity:SendGridOptions:EmailConfirmationTokenTemplateId"],
                PasswordResetTokenTemplateId = Configuration["Identity:SendGridOptions:PasswordResetTokenTemplateId"],
                TwoFactorTokenTemplateId = Configuration["Identity:SendGridOptions:TwoFactorTokenTemplateId"]
            };

            EmailService = new EmailService(sendGridClient, new OptionsWrapper<SendGridOptions>(sendGridOptions));
        }

        public EmailService EmailService { get; }
    }
}