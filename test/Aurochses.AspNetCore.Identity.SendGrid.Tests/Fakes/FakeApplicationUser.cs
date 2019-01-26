using System;
using System.Diagnostics.CodeAnalysis;

namespace Aurochses.AspNetCore.Identity.SendGrid.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeApplicationUser : IApplicationUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}