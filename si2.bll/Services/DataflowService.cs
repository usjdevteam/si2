using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class DataflowService : ServiceBase, IDataflowService
    {
        public DataflowService(IUnitOfWork uow, IMapper mapper, ILogger<DataflowService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task CreateCourseAsync(CreatDataflowDto createDataflowDto, CancellationToken ct)
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
    }
}
