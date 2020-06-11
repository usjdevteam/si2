using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;

namespace si2.bll.Services
{
    public interface IInstitutionService : IServiceBase
    {
        Task<InstitutionDto> CreateInstitutionAsync(CreateInstitutionDto createInstitutionDto, CancellationToken ct);
        Task<InstitutionDto> GetInstitutionByIdAsync(Guid id, CancellationToken ct);
    }
}
