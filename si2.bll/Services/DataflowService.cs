using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using Si2.common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;

namespace si2.bll.Services
{
    public class DataflowService : ServiceBase, IDataflowService
    {
        public DataflowService(IUnitOfWork uow, IMapper mapper, ILogger<IDataflowService> logger) : base(uow, mapper, logger)
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
            
            var updatedEntity = _mapper.Map<Dataflow>(updateDataflowDto);
            updatedEntity.Id = id;
            await _uow.Dataflows.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);
            var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
            dataflowDto = _mapper.Map<DataflowDto>(dataflowEntity);
            
            return dataflowDto;
        }

        public async Task<DataflowDto> PartialUpdateDataflowAsync(Guid id, UpdateDataflowDto updateDataflowDto, CancellationToken ct)
        {
            var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);

            _mapper.Map(updateDataflowDto, dataflowEntity);

            await _uow.Dataflows.UpdateAsync(dataflowEntity, id, ct, dataflowEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);

            dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
            var dataflowDto = _mapper.Map<DataflowDto>(dataflowEntity);

            return dataflowDto;
        }

        public async Task<UpdateDataflowDto> GetUpdateDataFlowDto(Guid id, CancellationToken ct)
        {
            var dataflowEntity = await _uow.Dataflows.GetAsync(id, ct);
            var updateDataflowDto = _mapper.Map<UpdateDataflowDto>(dataflowEntity);
            return updateDataflowDto;
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
                _uow.Dataflows.Delete(dataflowEntity);
                await _uow.SaveChangesAsync(ct);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, string.Empty);
            }
        }

        public async Task<PagedList<DataflowDto>> GetDataflowsAsync(DataflowResourceParameters resourceParameters, CancellationToken ct)
        {
            var dataflowEntities = _uow.Dataflows.GetAll();

            if (!string.IsNullOrEmpty(resourceParameters.Status))
            {
                if (Enum.TryParse(resourceParameters.Status, true, out DataflowStatus status))
                {
                    dataflowEntities = dataflowEntities.Where(c => 1==1);
                }
            }

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQueryForWhereClause = resourceParameters.SearchQuery.Trim().ToLower();
                dataflowEntities = dataflowEntities
                    .Where(a => a.Title.ToLower().Contains(searchQueryForWhereClause))
                       ;
            }

            var pagedListEntities = await PagedList<Dataflow>.CreateAsync(dataflowEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

            var result = _mapper.Map<PagedList<DataflowDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Dataflows.GetAsync(id, ct) != null)
                return true;
            
            return false;
        }


        public async Task ManageDataflowVehicles(Guid dataflowId, List<Guid> AddedVehicleIds, List<Guid> DeletedVehicleIds, CancellationToken ct)
        {
            if (DeletedVehicleIds != null)
            {
                foreach (var vId in DeletedVehicleIds)
                {
                    DataflowVehicle dataflowVehicle = await _uow.DataflowsVehicles.FirstOrDefaultAsync(c => c.DataflowId == dataflowId && c.VehicleId == vId, ct);
                    if (dataflowVehicle  != null)
                        _uow.DataflowsVehicles.Delete(dataflowVehicle);
                }
            }

            if (AddedVehicleIds != null)
            {
                foreach (var vId in AddedVehicleIds)
                {
                    if (await _uow.DataflowsVehicles.FirstOrDefaultAsync(c => c.DataflowId == dataflowId && c.VehicleId == vId, ct) == null)
                        await _uow.DataflowsVehicles.AddAsync(new DataflowVehicle() { DataflowId = dataflowId, VehicleId = vId }, ct);
                }
            }

            await _uow.SaveChangesAsync(ct);
        }
    }
}
