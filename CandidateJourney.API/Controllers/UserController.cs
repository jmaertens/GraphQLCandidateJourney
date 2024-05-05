using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CandidateJourney.Application.Contracts.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace CandidateJourney.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{/*
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
    
    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var validator = new LoginQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        var userModel = await _userService.Login(query);

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, userModel.Id.ToString()),
            new (ClaimTypes.Email, userModel.EmailAddress),
            new (ClaimTypes.GivenName, userModel.FirstName),
            new (ClaimTypes.Surname, userModel.LastName)
        };

        var serializedToken = CreateToken(claims);
        return Ok(new { token = serializedToken });
    }
    
    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var emailClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email);
        if (emailClaim == null) return Unauthorized();

        var userModel = await _userService.GetUserByEmailAddress(emailClaim.Value);
        return Ok(userModel);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("registration/{registrationId}")]
    public async Task<IActionResult> GetRegistration(Guid? registrationId)
    {
        var user = await _userService.GetUnregisteredUser(registrationId!.Value);

        return Ok(new RegistrationModel
        {
            Id = user.Id,
            EmailAddress = user.EmailAddress
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var validator = new RegisterUserCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await _userService.RegisterUser(command);
            
        return await Login(new LoginQuery
        {
            EmailAddress = user.EmailAddress,
            Password = command.Password
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Invite([FromBody] InviteUserCommand command)
    {
        var validator = new InviteUserCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await _userService.Invite(command);
        return Ok(user.Id);
    }
    
    private string CreateToken(IEnumerable<Claim> claims)
    {
        var key = _configuration.GetValue<string>("Authentication:SecurityKey");
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var token = new JwtSecurityToken(
            issuer: "CandidateJourney",
            audience: "CandidateJourney",
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    [Route("archive/{userId}")]
    [HttpDelete]
    [Authorize]
    [SwaggerOperation(Summary = "Archive a user", Description = "Archive a user")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> ArchiveUser(Guid userId)
    {
        try
        {
            var updatedUser = await _userService.ArchiveUserAsync(userId);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }*/
}