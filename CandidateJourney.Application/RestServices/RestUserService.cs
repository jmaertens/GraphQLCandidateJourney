using AutoMapper;
using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Application.Contracts.Queries;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Repositories;

namespace CandidateJourney.Application.Services;

public class RestUserService : IRestUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public RestUserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<UserModel>> GetAll()
    {
        var users = await _userRepository.GetAll();
        return _mapper.Map<List<UserModel>>(users);
    }

    public async Task<UserModel> Login(LoginQuery query)
    {
        const string errorMessage = "No match was found for the username and password you entered. Please double-check and try again.";

        var user = await _userRepository.FindByEmailAddress(query.EmailAddress!);

        if (user == null) throw new Exception(errorMessage);
        if (user.IsRegistered == false) throw new Exception(errorMessage);

        var passwordsMatch = BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash);
        if (!passwordsMatch) throw new Exception(errorMessage);

        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> GetUserByEmailAddress(string emailAddress)
    {
        var user = await _userRepository.FindByEmailAddress(emailAddress);
        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> RegisterUser(RegisterUserCommand command)
    {
        var user = await _userRepository.FindById(command.RegistrationId!);
        if (user == null || user.IsRegistered) throw new Exception("No pending registration was found.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);
        user.UpdatePasswordHash(hashedPassword);

        await _userRepository.Update(user);
        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> GetUnregisteredUser(Guid id)
    {
        var user = await _userRepository.FindById(id);
        if (user == null || user.IsRegistered) throw new Exception("No pending registration was found.");

        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> Invite(InviteUserCommand command)
    {
        var existingUser = await _userRepository.FindByEmailAddress(command.EmailAddress!);
        if (existingUser != null) throw new Exception($"A user with email address \"{command.EmailAddress}\" already exists.");

        var user = new User(command.FirstName!, command.LastName!, command.EmailAddress!);
        await _userRepository.Add(user);

        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> ArchiveUserAsync(Guid userId)
    {
        var user = await _userRepository.FindById(userId);
        await _userRepository.ArchiveUser(user);
        return _mapper.Map<UserModel>(user);
    }
}