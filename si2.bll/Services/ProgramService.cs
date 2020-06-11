using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Program;
using si2.bll.Dtos.Results.Program;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public async Task<PagedList<ProgramDto>> GetProgramAsync(CancellationToken ct)
        {

            PagedList<ProgramDto> result = new PagedList<ProgramDto>();

            try
            {
                var programEntities = _uow.Programs.GetAll();

                var pagedListEntities = await PagedList<Program>.CreateAsync(programEntities,
                    1, programEntities.Count(), ct);

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
