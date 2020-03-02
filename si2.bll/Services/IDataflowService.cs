using si2.bll.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IDataflowService : IServiceBase
    {
        Task CreateCourseAsync(CreatDataflowDto createDataflowDto, CancellationToken ct);
    }
}
