using AutoMapper;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;
using si2.dal.Entities;

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

            CreateMap<CreateInstitutionDto, Institution>();
            CreateMap<Institution, InstitutionDto>();
        }
    }
}
