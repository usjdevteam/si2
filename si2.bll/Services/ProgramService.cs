using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Program;
using si2.bll.Dtos.Results.Program;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;

namespace si2.bll.Services
{
    public class ProgramService : ServiceBase, IProgramService
    {
        public ProgramService(IUnitOfWork uow, IMapper mapper, ILogger<ProgramService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<ProgramDto> CreateProgramAsync(CreateProgramDto createProgramDto, CancellationToken ct)
        {
            ProgramDto programDto = null;

            try
            {
                var programEntity = _mapper.Map<Program>(createProgramDto);
                await _uow.Programs.AddAsync(programEntity, ct);
                await _uow.SaveChangesAsync(ct);
                programDto = _mapper.Map<ProgramDto>(programEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return programDto;
        }

        public async Task<ProgramDto> GetProgramByIdAsync(Guid id, CancellationToken ct) 
        {
            ProgramDto programDto = null;

            try
            {
                var programEntity = await _uow.Programs.GetAsync(id, ct);
                if (programEntity != null)
                {
                    programDto = _mapper.Map<ProgramDto>(programEntity);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return programDto;
        }

        public async Task<PagedList<ProgramDto>> GetProgramAsync(ProgramResourceParameters resourceParameters, CancellationToken ct)
        {
            PagedList<ProgramDto> result = new PagedList<ProgramDto>();

            try
            {
                var programEntities = _uow.Programs.GetAll();
                var pagedListEntities = await PagedList<Program>.CreateAsync(programEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

                result = _mapper.Map<PagedList<ProgramDto>>(pagedListEntities);
                result.TotalCount = pagedListEntities.TotalCount;
                result.TotalPages = pagedListEntities.TotalPages;
                result.CurrentPage = pagedListEntities.CurrentPage;
                result.PageSize = pagedListEntities.PageSize;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return result;
        }
    }
}
