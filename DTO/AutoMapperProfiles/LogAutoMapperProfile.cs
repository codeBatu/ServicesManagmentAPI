using AutoMapper;
using Model;
using Model.EntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{ 
 public   class LogAutoMapperProfile : Profile
    {
        public LogAutoMapperProfile()
        {
            CreateMap<LogTableDtoEntity, LogTable>().ForMember(dest => dest.Contents, opt => opt.MapFrom(src => $"{src.Contents}"))
                .ForMember(
                    dest => dest.TraceId,
                    opt => opt.MapFrom(src => $"{src.TraceId}")
                )
                .ForMember(
                    dest => dest.CreateDateTime,
                    opt => opt.MapFrom(src => $"{src.CreateDateTime}")
                ).ForMember(
                    dest => dest.ServiceId,
                    opt => opt.MapFrom(src => $"{src.ServiceId}")
                );
           
        }
    }
}
