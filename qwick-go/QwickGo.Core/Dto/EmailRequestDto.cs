public class EmailRequestDto
{
    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string? Phone {get; set;}
    public string Token {get; set;} = null!;

}