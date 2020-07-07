using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class InstitutionService : ServiceBase, IInstitutionService
    {
        public InstitutionService(IUnitOfWork uow, IMapper mapper, ILogger<IInstitutionService> logger) : base(uow, mapper, logger)
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

        public async Task<InstitutionDto> UpdateInstitutionAsync(Guid id, UpdateInstitutionDto updateInstitutionDto, CancellationToken ct)
        {
            var existingEntity = await _uow.Institutions.GetAsync(id, ct);

            var updatedInstitutionEntity = _mapper.Map<Institution>(updateInstitutionDto);
            var updatedAddressEntity = updatedInstitutionEntity.Address;
            var updatedContacInfoEntity = updatedInstitutionEntity.ContactInfo;

            updatedInstitutionEntity.Id = id;
            updatedInstitutionEntity.AddressId = updatedAddressEntity.Id = existingEntity.AddressId;
            updatedInstitutionEntity.ContactInfoId = updatedContacInfoEntity.Id = existingEntity.ContactInfoId;

            await _uow.Institutions.UpdateAsync(updatedInstitutionEntity, id, ct, updatedInstitutionEntity.RowVersion);
            await _uow.Addresses.UpdateAsync(updatedAddressEntity, updatedAddressEntity.Id, ct, updatedAddressEntity.RowVersion);
            await _uow.ContactInfos.UpdateAsync(updatedContacInfoEntity, updatedContacInfoEntity.Id, ct, updatedContacInfoEntity.RowVersion);
          
            await _uow.SaveChangesAsync(ct);
            
            var institutionEntity = await _uow.Institutions.GetCompleteAsync(id, ct);
            var institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);

            return institutionDto;
        }

        public async Task<InstitutionDto> GetInstitutionByIdAsync(Guid id, CancellationToken ct)
        {
            InstitutionDto institutionDto = null;

            var institutionEntity = await _uow.Institutions.GetCompleteAsync(id, ct);
            if (institutionEntity != null)
            {
                institutionDto = _mapper.Map<InstitutionDto>(institutionEntity);
            }

            return institutionDto;
        }

        public async Task<PagedList<InstitutionDto>> GetInstitutionsAsync(InstitutionResourceParameters resourceParameters, CancellationToken ct)
        {
            var institutionEntities = _uow.Institutions
                .GetAllComplete()
                .Where(c => resourceParameters.ParentId == null || c.ParentId == resourceParameters.ParentId);

            if (institutionEntities != null && institutionEntities.Count() > 0)
            {
                var pagedListEntities = await PagedList<Institution>.CreateAsync(institutionEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

                var result = _mapper.Map<PagedList<InstitutionDto>>(pagedListEntities);
                result.TotalCount = pagedListEntities.TotalCount;
                result.TotalPages = pagedListEntities.TotalPages;
                result.CurrentPage = pagedListEntities.CurrentPage;
                result.PageSize = pagedListEntities.PageSize;

                return result;
            }

            return null;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Institutions.GetAsync(id, ct) != null)
                return true;

            return false;
        }

    }
}
