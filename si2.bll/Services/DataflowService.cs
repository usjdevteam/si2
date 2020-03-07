using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.common;
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
            DataflowDto dataflowDto = null;
            try
            {
                var dataflowEntity = _mapper.Map<Dataflow>(createDataflowDto);
                await _uow.Dataflows.AddAsync(dataflowEntity, ct);
                await _uow.SaveChangesAsync(ct);
                dataflowDto = _mapper.Map<DataflowDto>(dataflowEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return dataflowDto;
        }

        public async Task<DataflowDto> UpdateDataflowAsync(Guid id, UpdateDataflowDto updateDataflowDto, CancellationToken ct)
        {
            DataflowDto dataflowDto = null;
            try
            {
                var updatedEntity = _mapper.Map<Dataflow>(updateDataflowDto);
                updatedEntity.Id = id;
                await _uow.Dataflows.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
                await _uow.SaveChangesAsync(ct);
                var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
                dataflowDto = _mapper.Map<DataflowDto>(dataflowEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return dataflowDto;
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
            try
            {
                var dataflowEntity = await _uow.Dataflows.FirstAsync(c => c.Id == id, ct);
                await _uow.Dataflows.DeleteAsync(dataflowEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, string.Empty);
            }
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
