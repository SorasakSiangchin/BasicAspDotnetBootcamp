using AutoMapper;
using CourseProject.Common.Dtos;
using CourseProject.Common.Models;

namespace CourseProject.Business;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<AddressCreate, Address>()
            //ใช้สำหรับการกำหนดว่าในการแมปข้อมูลนี้ คุณไม่ต้องการให้มันแมปค่า Id จาก AddressCreate ไปยัง Address. นั่นคือ มันจะข้ามการแมปค่า Id นี้ไป.
            .ForMember(dest => dest.Id , opt => opt.Ignore());
        CreateMap<AddressUpdate, Address>();
        CreateMap<Address, AddressGet>();
    }
}