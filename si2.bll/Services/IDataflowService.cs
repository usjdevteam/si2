using si2.bll.Dtos.Requests;
using si2.bll.Dtos.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IDataflowService : IServiceBase
    {
        Task CreateDataflowAsync(CreateDataflowDto createDataflowDto, CancellationToken ct);
        Task<DataFlowDto> GetDataflowByIdAsync(Guid id, CancellationToken ct);

        Task DeleteDataflowByIdAsync(Guid id, CancellationToken ct);
    }
}
