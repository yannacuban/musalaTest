using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using GateWays.Common.Comunication;
using GateWays.Common.Pagination;
using GateWays.Common.QueryResults;
using GateWays.Common.Resources;
using GateWays.EntityFrameworkCore.Models;

namespace GateWaysAPI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Gateway, GatewayDTO>();

            CreateMap<Peripheral, PeripheralDTO>()
                .ForMember(x => x.Created, req => req.MapFrom(src => src.Created.ToString(CultureInfo.InvariantCulture)));

            CreateMap<PagedResponse<Gateway>, PagedDataContract<GatewayDTO>>()
                .ForMember(x => x.Items, req => req.MapFrom(src => src.Resource));

            CreateMap<AddGatewayResource, Gateway>();

            CreateMap<UpdateGatewayResource, Gateway>();

            CreateMap<AddPeripheralResource, Peripheral>();

            CreateMap<UpdatePeripheralResource, Peripheral>();

            // Resource to Domain
            CreateMap<GatewayDTO, Gateway>();

            CreateMap<PeripheralDTO, Peripheral>();
        }
    }
}
