using AutoMapper;
using si2.bll.Dtos.Requests.ContactInfo;
using si2.bll.Dtos.Results.ContactInfo;
using si2.bll.Dtos.Requests.Address;
using si2.bll.Dtos.Results.Address;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.Program;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.Program;
using si2.dal.Entities;
using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;

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

            CreateMap<CreateDocumentDto, Document>();
            CreateMap<UpdateDocumentDto, Document>();
            CreateMap<SoftDeleteDocumentDto, Document>();
            CreateMap<Document, DocumentDto>();
        }
    }
}
