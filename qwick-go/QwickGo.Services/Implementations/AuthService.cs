using QwickGo.Core.Dto;
using QwickGo.Core.Interfaces;
using QwickGo.Services.Implementations.Tokens;
using QwickGo.Services.Interfaces;
using FirebaseAdmin.Auth;
using QwickGo.Core.Entities;
using QwickGo.Core.Enums;
using Google.Apis.Auth.OAuth2.Requests;

namespace QwickGo.Services.Implementations;

public class AuthService : IAuthServices
{
    private readonly IUserRepository _userRepository;
    private readonly TokenServices _tokenServices;

    public AuthService(IUserRepository userRepository, TokenServices tokenServices)
    {
        _userRepository = userRepository;
        _tokenServices = tokenServices;   
    }

    public async Task<AuthResponseDto> GoogleSignup(string firebaseToken)
    {
        FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
        string email = decodedToken.Claims["email"].ToString()!;
        string uid = decodedToken.Uid;
        var user = await _userRepository.GetUserByEmail(email);

        if (user != null)
        {
            return new AuthResponseDto
            {
                UserExist = true,
                Message = "User already exists",
                Email = email
            };
        }

        var name = decodedToken.Claims["name"].ToString() ?? "";
        user = new User
        {
            Name = name,
            Email = email,
            FirebaseUid = uid,
            AuthProvider = AuthProvider.Google,
            IsEmailVerified = true,
            Role = UserRole.Owner,
            CreatedTime = DateTime.UtcNow,
        };

        await _userRepository.AddUser(user);

        var jwtToken = _tokenServices.GenerateAccessToken(user);
        var refreshToken = _tokenServices.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _userRepository.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = jwtToken,
            RefreshToken = user.RefreshToken,
            Email = email,
            Role = user.Role.ToString(),
            UserExist = false
        };
    }
}