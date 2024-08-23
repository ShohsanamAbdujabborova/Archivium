using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Collections;
using Archivium.Domain.Entities.Commets;
using Archivium.Domain.Entities.Enums;
using Archivium.Domain.Entities.Likes;

namespace Archivium.Domain.Entities.Users;
public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Phone { get; set; }
    public UserRole UserRole { get; set; }
    public bool IsBlocked { get; set; } = false;
    public IEnumerable<Collection> Collections { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}