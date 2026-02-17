using QwickGo.Core.Dto;

namespace QwickGo.Services.Interfaces;
public interface IAuthServices
{
    Task<AuthResponseDto> GoogleSignup(string FirebaseToken);
}