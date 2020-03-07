using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class DataflowService : ServiceBase, IDataflowService
    {
        public DataflowService(IUnitOfWork uow, IMapper mapper, ILogger<DataflowService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<DataflowDto> CreateDataflowAsync(CreateDataflowDto createDataflowDto, CancellationToken ct)
        {
            Dataflow dataflowEntity = null;

            try
            {
                dataflowEntity = _mapper.Map<Dataflow>(createDataflowDto);
                await _uow.Dataflows.AddAsync(dataflowEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
           

            return _mapper.Map<DataflowDto>(dataflowEntity);
        }

        public async Task<DataflowDto> GetDataflowByIdAsync(Guid id, CancellationToken ct)
        {
            DataflowDto dataflowDto = null;

            var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
            if (dataflowEntity != null)
            {
                dataflowDto = _mapper.Map<DataflowDto>(dataflowEntity);
            }

            return dataflowDto;
        }

        public async Task DeleteDataflowByIdAsync(Guid id, CancellationToken ct)
        {

            var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
            if (dataflowEntity == null)
                throw new Exception();

            await _uow.Dataflows.DeleteAsync(dataflowEntity, ct);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<DataflowDto>> GetDataflowsAsync(CancellationToken ct)
        {
            var dataflowEntities = await _uow.Dataflows.GetAllAsync(ct);
            if (dataflowEntities != null)
            {
                var dataflowDtos = _mapper.Map<IEnumerable<DataflowDto>>(dataflowEntities);
                return dataflowDtos;
            }
            return null; 
        }
    }
}
