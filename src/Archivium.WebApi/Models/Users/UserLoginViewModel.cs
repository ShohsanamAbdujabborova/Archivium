using Archivium.Domain.Entities.Enums;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Archivium.WebApi.Models.Users;

public class UserLoginViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Token { get; set; }
    [DefaultValue(false)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public bool IsBlocked { get; set; }
    public UserRole UserRole { get; set; }
    public DateTime DateOfBirth { get; set; }
}
