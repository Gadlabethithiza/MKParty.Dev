using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember;
using eMKParty.BackOffice.Support.Application.Features.VotingResults.Commands;
using eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Incidents.Commands
{
	public class IncidentResultCommand : IRequest<Result<IncidentDto>>, IMapFrom<Incident>
    {
        [MaxLength(20)]
        public string? VDPartCode { get; set; } = "MK";

        [Required]
        [MaxLength(20)]
        public string VDUniqueCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string VDAgentCode { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "{0} Incident Description length must be between {2} and {1}.", MinimumLength = 20)]
        public string Incident_Description { get; set; }

        public string? Category { get; set; } = "Other";
        public string? Severity { get; set; } = "Minor";
        public string? IncStatus { get; set; } = "Open";

        [StringLength(1000, ErrorMessage = "The Resolution Description value cannot exceed 1000 characters. ")]
        public string? Resolution_Description { get; set; }

        public Boolean? IsIECRelated { get; set; } = false;
        public string? AssignedTo { get; set; }
        public DateTime? AssignedDate { get; set; } = Convert.ToDateTime("1900-01-01 00:00:00");
        public string? EscalatedTo { get; set; }
        public DateTime? EscalatedDate { get; set; } = Convert.ToDateTime("1900-01-01 00:00:00");
        public DateTime? CompletedDate { get; set; } = Convert.ToDateTime("1900-01-01 00:00:00");
        public int? VDYear { get; set; } = DateTime.Now.Year;
        public string? creationby { get; set; }
        public DateTime? creationdate { get; set; } = DateTime.Now;
        public string? updatedby { get; set; }
        public DateTime? updateddate { get; set; } = DateTime.Now;
        public Guid Guid { get; set; } = System.Guid.NewGuid();
    }

    internal class IncidentResultCommandHandler : IRequestHandler<IncidentResultCommand, Result<IncidentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<IncidentResultCommandHandler> _logger;
        private readonly IAesOperation _securityService;
        private readonly IConfiguration _config;
        private readonly IDatabaseUnitOfWork _dbUnitWork;


        public IncidentResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IDatabaseUnitOfWork dbUnitWork, IAesOperation securityService, ILogger<IncidentResultCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _securityService = securityService;
            _dbUnitWork = dbUnitWork;
        }

        public async Task<Result<IncidentDto>> Handle(IncidentResultCommand command, CancellationToken cancellationToken)
        {
            //Validate VD Unique Code
            if (!string.IsNullOrWhiteSpace(command.VDUniqueCode))
                if (!await VotingStationExist(command.VDUniqueCode)) return await Result<IncidentDto>.FailureAsync(null, "Voting Stationg VD Code does not exist.");

            //Validate if The passed ID Number is for the valid MK Agent
            MemberDto agentInfo = new MemberDto();
            if (!string.IsNullOrWhiteSpace(command.VDAgentCode))
            {
                agentInfo = await UserExistAndIsAnAgent(command.VDAgentCode);

                if (agentInfo == null)
                    return await Result<IncidentDto>.FailureAsync(null, "The Person sending the results is not Marked as a Party Agent.");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(command.VDPartCode))
                    command.VDPartCode = "MK";

                var item = new Incident()
                {
                    VDPartCode = command.VDPartCode.Trim(),
                    VDUniqueCode = command.VDUniqueCode,
                    VDAgentCode = agentInfo.id_no ?? agentInfo.membership_no,
                    Incident_Description = command.Incident_Description,
                    Category = command.Category,
                    Severity = command.Severity,
                    IncStatus = command.IncStatus,
                    Resolution_Description = command.Resolution_Description,
                    IsIECRelated = command.IsIECRelated,
                    AssignedTo = command.AssignedTo,
                    AssignedDate = command.AssignedDate,
                    EscalatedTo = command.EscalatedTo,
                    EscalatedDate = command.EscalatedDate,
                    CompletedDate = command.CompletedDate,
                    VDYear = command.VDYear,
                    creationdate = command.creationdate,
                    updateddate = command.updateddate,
                    creationby = agentInfo.name + " " + agentInfo.surname,
                    updatedby = agentInfo.name + " " + agentInfo.surname,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = 0,
                    ModifiedBy = 0,

                    Guid = command.Guid
                };

                await _unitOfWork.Repository<Incident>().AddAsync(item);
                item.AddDomainEvent(new IncidentCreatedEvent(item));
                await _unitOfWork.Save(cancellationToken);
                return await Result<IncidentDto>.SuccessAsync(null, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred at CreateVotingResultCommandHandler() - {ex} ", DateTime.UtcNow.ToLongTimeString());
                return await Result<IncidentDto>.FailureAsync(null, "Error Occured:" + ex);
            }
        }

        private async Task<bool> VotingStationExist(string _VDUniqueCode)
        {
            bool returnValu = false;

            if (await _unitOfWork.Repository<VotingStation>().Entities.Where(x => x.VotingDistrict == _VDUniqueCode || x.VDUniqueCode == _VDUniqueCode)
                .ProjectTo<VotingStationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync() != null)
                returnValu = true;

            return returnValu;
        }

        private async Task<MemberDto> UserExistAndIsAnAgent(string parameter)
        {
            var returnValu = await _unitOfWork.Repository<MemberRegister>().Entities.Where(x => x.elections_agent == false && ((x.id_no == _securityService.EncryptString(_config["SecurityKey"], parameter) || (x.membership_no == parameter))))
                                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            return returnValu;
        }
    }
}