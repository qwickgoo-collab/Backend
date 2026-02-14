using System.ComponentModel.DataAnnotations;
using QwickGo.Core.Enums;

namespace QwickGo.Core.Entities;
public class User
{
    [Key]
    public int UserID {get; set;}

    [Required]
    [MaxLength(100)]
    public string Name {get; set;} = string.Empty;

    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    public string PhoneNumber {get; set;} = string.Empty;

    [Required]
    public string PasswordHash {get; set;} = string.Empty;

    public UserRole Role {get; set;} = UserRole.Customer;

    public string? FirebaseUid {get; set;}
    public string AuthProvider {get; set;} = "Email";

    public bool IsEmailVerified {get; set;} = false;

    public bool IsPhoneVerified {get; set;} = false;

    public DateTime CreatedTime {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedTime {get; set;} = DateTime.UtcNow;

}