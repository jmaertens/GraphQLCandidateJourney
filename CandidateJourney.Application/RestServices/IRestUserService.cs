using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Application.Contracts.Queries;

namespace CandidateJourney.Application.Services;

public interface IRestUserService
{
    Task<List<UserModel>> GetAll();

    Task<UserModel> Login(LoginQuery query);

    Task<UserModel> GetUserByEmailAddress(string emailAddress);

    Task<UserModel> RegisterUser(RegisterUserCommand command);

    Task<UserModel> GetUnregisteredUser(Guid id);

    Task<UserModel> Invite(InviteUserCommand command);
    Task<UserModel> ArchiveUserAsync(Guid userId);
}