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
 public   class ServiceAutoMapper : Profile
    {
        public ServiceAutoMapper()
        {
            CreateMap<ServiceTableDtoEntity, ServiceTable>().ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => $"{src.ServiceName}")).ForMember(
                    dest => dest.CreateDateTime,
                    opt => opt.MapFrom(src => $"{src.CreateDateTime}")
                )
                .ForMember(
                    dest => dest.ServiceStatus,
                    opt => opt.MapFrom(src => $"{src.ServiceStatus}")
                ).ForMember(
                    dest => dest.ActiveLife,
                    opt => opt.MapFrom(src => $"{src.ActiveLife}")
                ).ForMember(
                    dest => dest.RestDateTime,
                    opt => opt.MapFrom(src => $"{src.RestDateTime}")
                ).ForMember(
                    dest => dest.RestartCount,
                    opt => opt.MapFrom(src => $"{src.RestartCount}")
                ).ForMember(
                    dest => dest.Version,
                    opt => opt.MapFrom(src => $"{src.Version}")
                );
           
        }
    }
}
