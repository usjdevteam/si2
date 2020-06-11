using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using si2.bll.Dtos.Requests.Program;
using si2.bll.Dtos.Results.Program;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;

namespace si2.bll.Services
{
    public interface IProgramService : IServiceBase
    {
        Task<ProgramDto> CreateProgramAsync(CreateProgramDto createProgramDto, CancellationToken ct);

        Task<PagedList<ProgramDto>> GetProgramAsync(CancellationToken ct);
    }
}
