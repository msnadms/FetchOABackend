using AutoMapper;
using FetchOA.Dtos;
using FetchOA.Models;

namespace FetchOA.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReceiptDto, Receipt>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());

            CreateMap<Receipt, ReceiptDto>();
        }


    }
}
