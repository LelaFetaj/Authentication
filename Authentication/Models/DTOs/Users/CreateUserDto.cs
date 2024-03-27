using Authentication.Models.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.DTOs.Users
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Other;
        public DateTimeOffset? BirthDate { get; set; }
    }
}
