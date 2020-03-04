using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests;
using si2.bll.Dtos.Results;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class DataflowService : ServiceBase, IDataflowService
    {
        public DataflowService(IUnitOfWork uow, IMapper mapper, ILogger<DataflowService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task CreateDataflowAsync(CreateDataflowDto createDataflowDto, CancellationToken ct)
        {
            Dataflow dataflowEntity = null;

            try
            {
                dataflowEntity = _mapper.Map<Dataflow>(createDataflowDto);
                await _uow.Dataflows.AddAsync(dataflowEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<DataFlowDto> GetDataflowByIdAsync(Guid id, CancellationToken ct)
        {
            DataFlowDto dataflowDto = null;

            var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
            if (dataflowEntity != null)
            {
                dataflowDto = _mapper.Map<DataFlowDto>(dataflowEntity);
            }

            return dataflowDto;
        }
    }
}
