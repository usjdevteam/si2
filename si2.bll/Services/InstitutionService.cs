using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;
using si2.dal.Entities;
using si2.dal.UnitOfWork;

namespace si2.bll.Services
{
    public class InstitutionService : ServiceBase, IInstitutionService
    {

        public InstitutionService(IUnitOfWork uow, IMapper mapper, ILogger<InstitutionService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<InstitutionDto> CreateInstitutionAsync(CreateInstitutionDto createInstitutionDto, CancellationToken ct)
        {
            InstitutionDto institutionDto = null;
            try
            {
                var institutionEntity = _mapper.Map<Institution>(createInstitutionDto);
                await _uow.Institutions.AddAsync(institutionEntity, ct);
                await _uow.SaveChangesAsync(ct);
                institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return institutionDto;
        }

        public async Task<InstitutionDto> GetInstitutionByIdAsync(Guid id, CancellationToken ct)
        {
            InstitutionDto institutionDto = null;

            var institutionEntity = await _uow.Institutions.GetAsync(id, ct);
            if (institutionEntity != null)
            {
                institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);
            }

            return institutionDto;
        }
    }
    
}
