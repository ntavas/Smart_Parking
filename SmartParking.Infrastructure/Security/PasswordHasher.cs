using Microsoft.Extensions.Logging;
using SmartParking.Application.Services.Interfaces;

namespace SmartParking.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher {
    private readonly ILogger<PasswordHasher> _logger;

    public PasswordHasher(ILogger<PasswordHasher> logger)
    {
        _logger = logger;
    }

    public string Hash(string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        _logger.LogInformation("Hashed password: {Hash}", hashedPassword);
        var test = BCrypt.Net.BCrypt.Verify("pass1234", hashedPassword);
        _logger.LogInformation("Password verification is: {Result}", test);
        return hashedPassword;
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}