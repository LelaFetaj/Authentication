using Authentication.Models.Entities.Users;

namespace Authentication.Models.DTOs.Users
{
    public class ModifyUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Other;
        public DateTimeOffset BirthDate { get; set; }
    }
}
