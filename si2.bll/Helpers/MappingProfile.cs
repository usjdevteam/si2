using AutoMapper;
using si2.bll.Dtos.Requests;
using si2.bll.Dtos.Results;
using si2.dal.Entities;

namespace si2.bll.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDataflowDto, Dataflow>();
            CreateMap<Dataflow, DataFlowDto>();
        }
    }
}
