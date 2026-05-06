namespace QwickGo.Core.Dto;

public class GoogleRequest
{
    public string Token {get; set;} = string.Empty;
}

public class GoogleRequestResponseDto
{
    public bool IsCreated {get; set;} = false;
    public string Message {get; set;} = string.Empty;
    public string? Email {get; set;}
}