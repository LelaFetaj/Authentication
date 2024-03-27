using Microsoft.AspNetCore.Identity;

namespace Authentication.Models.Entities.Users
{
    public class User : IdentityUser<Guid>
    {
        public required string FirstName {  get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Other;
        public DateTimeOffset BirthDate { get; set; }
        public Guid CreatedBy { get; set; }
        public required DateTimeOffset CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public required DateTimeOffset UpdatedDate { get; set; }
    }
}
