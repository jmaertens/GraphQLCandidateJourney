using AutoMapper;
using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Contracts.Models;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Application.Services
{
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IMapper _mapper;

        public InterestService(IInterestRepository interestRepository, IMapper mapper)
        {
            _interestRepository = interestRepository;
            _mapper = mapper;
        }

        public async Task<List<string>> GetAllInterestNamesAsync()
        {
            var interests = await _interestRepository.GetAll();
            return _mapper.Map<List<InterestModel>>(interests).Select(x => x.Name).ToList();
        }

        public async Task<List<InterestModel>> GetAllInterestsAsync()
        {
            var interests = await _interestRepository.GetAll();
            return _mapper.Map<List<InterestModel>>(interests);
        }

        public async Task<InterestModel> GetInterestByIdAsync(int id)
        {
            var interest = await _interestRepository.FindById(id);
            return _mapper.Map<InterestModel>(interest);
        }

        public async Task<InterestModel> AddInterestAsync(CreateInterestCommand command)
        {
            var newInterest = new Interest(command.Name);
            await _interestRepository.Add(newInterest);
            return _mapper.Map<InterestModel>(newInterest);
        }

        public async Task<InterestModel> UpdateInterestAsync(int id, UpdateInterestCommand command)
        {
            var interest = await _interestRepository.FindById(id);
            if (interest == null) throw new Exception("Interest not found.");
            interest.Name = command.Name;

            await _interestRepository.UpdateInterest(interest);
            return _mapper.Map<InterestModel>(interest);
        }

        public async Task<InterestModel> DeleteInterestAsync(int id)
        {
            var interest = await _interestRepository.FindById(id);
            if (interest == null) throw new Exception("Interest not found.");
            await _interestRepository.Delete(interest);
            return _mapper.Map<InterestModel>(interest);
        }
    }
}
