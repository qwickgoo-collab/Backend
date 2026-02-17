namespace QwickGo.Core.Dto;
public class AuthResponseDto
{
    public string Token {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Role {get; set;} = string.Empty;
    public string? RefreshToken {get; set;} 
    public bool UserExist {get; set;} = true;
    public string? Message {get; set;} 
}