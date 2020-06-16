using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface ICohortService : IServiceBase
    {
        Task<CohortDto> CreateCohortAsync(CreateCohortDto createCohortDto, CancellationToken ct);
        Task<CohortDto> GetCohortByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<CohortDto>> GetCohortsAsync(CancellationToken ct);
        

        Task<bool> ExistsAsync(Guid id, CancellationToken ct);



    }
}
