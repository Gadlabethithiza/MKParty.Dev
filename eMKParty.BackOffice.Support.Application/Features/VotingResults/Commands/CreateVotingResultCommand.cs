using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Application.Features.VotingResults.Commands
{
	public class CreateVotingResultCommand : IRequest<Result<VotingResultDto>>, IMapFrom<VotingResult>
    {
        public string VDPartCode { get; set; } = "MK";
        public string VDUniqueCode { get; set; }
        public int VDResults { get; set; }
        public string VDAgentCode { get; set; }
    }

    internal class CreateVotingResultCommandHandler : IRequestHandler<CreateVotingResultCommand, Result<VotingResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVotingResultCommand> _logger;
        private readonly IAesOperation _securityService;
        private readonly IConfiguration _config;
        private readonly IVotingResultRepository _votingresultRepository;

        public CreateVotingResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IVotingResultRepository votingresultRepository,IAesOperation securityService, ILogger<CreateVotingResultCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _securityService = securityService;
            _votingresultRepository = votingresultRepository;
        }

        public async Task<Result<VotingResultDto>> Handle(CreateVotingResultCommand command, CancellationToken cancellationToken)
        {
            //Validate VD Unique Code
            if (!string.IsNullOrWhiteSpace(command.VDUniqueCode))
                if (!await VotingStationExist(command.VDUniqueCode)) return await Result<VotingResultDto>.FailureAsync(null, "Voting Stationg VD Code does not exist.");

            //Validate if The passed ID Number is for the valid MK Agent
            MemberDto agentInfo = new MemberDto();
            if (!string.IsNullOrWhiteSpace(command.VDAgentCode))
            {
                agentInfo = await UserExistAndIsAnAgent(command.VDAgentCode);

                if(agentInfo == null)
                    return await Result<VotingResultDto>.FailureAsync(null, "The Person sending the results is not Marked as a Party Agent.");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(command.VDPartCode))
                    command.VDPartCode = "MK";

                //Validate if Results Exist            
                if (!string.IsNullOrWhiteSpace(command.VDPartCode) && !string.IsNullOrWhiteSpace(command.VDUniqueCode))
                {
                    //var votingResult = await VotingResultsExist(command.VDUniqueCode, command.VDPartCode.Trim());

                    //Populate the object to Post
                    //if (votingResult == null) //Insert New
                    //{
                    var item = new VotingResult()
                    {
                        VDPartCode = command.VDPartCode.Trim(),
                        VDUniqueCode = command.VDUniqueCode,
                        VDAgentCode = agentInfo.id_no,
                        VDResults = command.VDResults,
                        VDYear = DateTime.Now.Year,
                        creationdate = DateTime.Now,
                        updateddate = DateTime.Now,
                        creationby = agentInfo.name + " " + agentInfo.surname,
                        updatedby = agentInfo.name + " " + agentInfo.surname,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        Guid = Guid.NewGuid()
                    };

                    await _unitOfWork.Repository<VotingResult>().AddAsync(item);
                    item.AddDomainEvent(new VotingResultCreatedEvent(item));
                    await _unitOfWork.Save(cancellationToken);
                    //}
                    //else //Update Existing
                    //{
                    //    //Update only when the new resutls are greater than the old one
                    //    if (command.VDResults > votingResult.VDResults)
                    //    {
                    //        votingResult.VDResults = command.VDResults;
                    //        votingResult.updatedby = agentInfo.name + " " + agentInfo.surname;
                    //        await _votingresultRepository.UpadeVotingResultAsync(votingResult);
                    //    }
                    //}
                }

                return await Result<VotingResultDto>.SuccessAsync(null, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred at CreateVotingResultCommandHandler() - {ex} ", DateTime.UtcNow.ToLongTimeString());
                return await Result<VotingResultDto>.FailureAsync(null, "Error Occured:" + ex);
            }
        }

        private async Task<VotingResultDto> VotingResultsExist(string _VDUniqueCode, string _VDPartCode)
        {
            var returnValu = await _unitOfWork.Repository<VotingResult>().Entities.Where(x => x.VDUniqueCode == _VDUniqueCode
                                    && x.VDPartCode == _VDPartCode
                                    && x.VDYear == DateTime.Now.Year)
                                   .ProjectTo<VotingResultDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                

            return returnValu;
        }

        private async Task<bool> VotingStationExist(string _VDUniqueCode)
        {
            bool returnValu = false;

            if (await _unitOfWork.Repository<VotingStation>().Entities.Where(x => x.VotingDistrict == _VDUniqueCode || x.VDUniqueCode == _VDUniqueCode)
                .ProjectTo<VotingStationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync() != null)
                returnValu = true;

            return returnValu;
        }

        private async Task<MemberDto> UserExistAndIsAnAgent(string id_no)
        {
            var returnValu = await _unitOfWork.Repository<MemberRegister>().Entities.Where(x => x.elections_agent == false && x.id_no == _securityService.EncryptString(_config["SecurityKey"], id_no))
                                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            return returnValu;
        }
    }
}