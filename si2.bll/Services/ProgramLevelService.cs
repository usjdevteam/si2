using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.ProgramLevel;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.ProgramLevel;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using Si2.common.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;
namespace si2.bll.Services
{
    public class ProgramLevelService : ServiceBase, IProgramLevelService
    {

        public ProgramLevelService(IUnitOfWork uow, IMapper mapper, ILogger<IProgramLevelService> logger) : base(uow, mapper, logger)
        {
        }


        public async Task<ProgramLevelDto> CreateProgramLevelAsync(CreateProgramLevelDto createProgramLevelDto, CancellationToken ct)
        {
            ProgramLevelDto programLevelDto = null;
            try
            {
                var programLevelEntity = _mapper.Map<ProgramLevel>(createProgramLevelDto);
                await _uow.ProgramLevels.AddAsync(programLevelEntity, ct);
                await _uow.SaveChangesAsync(ct);
                programLevelDto = _mapper.Map<ProgramLevelDto>(programLevelEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return programLevelDto;
        }

        public async Task<ProgramLevelDto> GetProgramLevelByIdAsync(Guid id, CancellationToken ct)
        {
            ProgramLevelDto programLevelDto = null;

            var programLevelEntity = await _uow.ProgramLevels.GetAsync(id, ct);

            if (programLevelEntity != null)
            {
                programLevelDto = _mapper.Map<ProgramLevelDto>(programLevelEntity);
            }

            return programLevelDto;
        }

        public async Task<PagedList<ProgramLevelDto>> GetProgramLevelsAsync(CancellationToken ct)
        {
            var programLevelEntities = _uow.ProgramLevels.GetAll();

            var pagedListEntities = await PagedList<ProgramLevel>.CreateAsync(programLevelEntities,
                  1, programLevelEntities.Count(), ct);

            var result = _mapper.Map<PagedList<ProgramLevelDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }


        public async Task<ProgramLevelDto> UpdateProgramLevelAsync(Guid id, UpdateProgramLevelDto updateProgramLevelDto, CancellationToken ct)
        {
            ProgramLevelDto programLevelDto = null;

            var updatedEntity = _mapper.Map<ProgramLevel>(updateProgramLevelDto);
            updatedEntity.Id = id;
            await _uow.ProgramLevels.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);
            var studentEntity = await _uow.ProgramLevels.GetAsync(id, ct);
            programLevelDto = _mapper.Map<ProgramLevelDto>(studentEntity);

            return programLevelDto;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.ProgramLevels.GetAsync(id, ct) != null)
                return true;

            return false;
        }
    }
}
