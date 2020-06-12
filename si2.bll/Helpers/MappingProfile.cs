using AutoMapper;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Helpers.PagedList;
using si2.dal.Entities;
using System.Linq;

namespace si2.bll.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDataflowDto, Dataflow>();
            CreateMap<UpdateDataflowDto, Dataflow>();
            CreateMap<Dataflow, DataflowDto>();
            CreateMap<Dataflow, UpdateDataflowDto>();


            CreateMap<CreateCohortDto, Cohort>();
            CreateMap<UpdateCohortDto, Cohort>();
            CreateMap<Cohort, CohortDto>();
            CreateMap<Cohort, UpdateCohortDto>();

            CreateMap<UserCohort, UserCohortDto>();


        }
    }
}
