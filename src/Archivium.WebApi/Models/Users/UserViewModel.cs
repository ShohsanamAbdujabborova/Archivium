using Archivium.Domain.Entities.Enums;

namespace Archivium.WebApi.Models.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public UserRole UserRole { get; set; }
}
