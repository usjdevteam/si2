using AutoMapper;
using si2.bll.Dtos.Requests.ContactInfo;
using si2.bll.Dtos.Results.ContactInfo;
using si2.bll.Dtos.Requests.Address;
using si2.bll.Dtos.Results.Address;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.Program;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Dtos.Results.Program;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Helpers.PagedList;

using si2.dal.Entities;
using si2.bll.Dtos.Requests.Course;
using si2.bll.Dtos.Results.Course;
using si2.bll.Dtos.Results.CourseCohortDto;
using si2.bll.Dtos.Results.UserCourse;
using si2.bll.Dtos.Results.Administration;

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

            CreateMap<CreateProgramDto, Program>();
            CreateMap<Program, ProgramDto>();

            CreateMap<CreateContactInfoDto, ContactInfo>();
            CreateMap<UpdateContactInfoDto, ContactInfo>();
            CreateMap<ContactInfo, ContactInfoDto>();

            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>(); 
            CreateMap<Address, AddressDto>();

            CreateMap<CreateInstitutionDto, Institution>();
            CreateMap<UpdateInstitutionDto, Institution>();
            CreateMap<Institution, InstitutionDto>();
            CreateMap<Institution, UpdateInstitutionDto>();
            
            CreateMap<UserCohort, UserCohortDto>();

            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<Course, UpdateCourseDto>();

            CreateMap<CourseCohort, CourseCohortDto>();
            CreateMap<UserCourse, UserCourseDto>();

            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
