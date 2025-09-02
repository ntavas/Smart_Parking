using FluentValidation;
using Microsoft.Extensions.Logging;
using SmartParking.Application.Dtos;
using SmartParking.Application.Mappings;
using SmartParking.Application.Services.Interfaces;

namespace SmartParking.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IValidator<RegisterRequestDto> _registerValidator;
    private readonly IValidator<LoginRequestDto> _loginValidator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtGenerator,
        IValidator<RegisterRequestDto> registerValidator,
        IValidator<LoginRequestDto> loginValidator,
        IPasswordHasher passwordHasher,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<UserDto> RegisterAsync(RegisterRequestDto request)
    {
        var validationResult = await _registerValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.ToString());

        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var user = request.ToEntity();
        user.PasswordHash = _passwordHasher.Hash(request.Password);

        var createdUser = await _userRepository.AddUserAsync(user);
        return createdUser.ToDto();
    }

    public async Task<string?> LoginAsync(LoginRequestDto request)
    {
        var validationResult = await _loginValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.ToString());
        
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        
        var passVerified = _passwordHasher.Verify(request.Password, user.PasswordHash);
        
        _logger.LogInformation("Password verification result: {Result}", passVerified);
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return null;

        _logger.LogInformation("User {UserId} logged in successfully", user.Id);
        
        return _jwtGenerator.GenerateToken(user);
    }
}