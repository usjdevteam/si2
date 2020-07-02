using si2.bll.Dtos.Requests.ProgramLevel;
using si2.bll.Dtos.Results.ProgramLevel;
using si2.bll.Helpers.PagedList;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IProgramLevelService : IServiceBase
    {
        Task<ProgramLevelDto> GetProgramLevelByIdAsync(Guid id, CancellationToken ct);
        Task<ProgramLevelDto> CreateProgramLevelAsync(CreateProgramLevelDto createProgramLevelDto, CancellationToken ct);
        Task<ProgramLevelDto> UpdateProgramLevelAsync(Guid id, UpdateProgramLevelDto updateProgramLevelDto, CancellationToken ct);
        Task<PagedList<ProgramLevelDto>> GetProgramLevelsAsync(CancellationToken ct);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    }
}
