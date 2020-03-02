using AutoMapper;
using si2.bll.Dtos;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatDataflowDto, Dataflow>();
        }
    }
}
