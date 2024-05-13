using CourseProject.Common.Interfaces;
using CourseProject.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        //typeof(DtoEntityMapperProfile) คือการระบุว่าเราจะใช้ DtoEntityMapperProfile เป็น Configuration Profile สำหรับ AutoMapper ในโปรเจคนี้ ซึ่ง DtoEntityMapperProfile คือ class ที่เราสร้างขึ้นมาเพื่อกำหนดวิธีการแมปข้อมูลระหว่าง object ต่างๆ ในโปรเจค
        // services.AddAutoMapper(typeof(DtoEntityMapperProfile)); จะเพิ่ม AutoMapper ใน Dependency Injection Container และกำหนดให้ใช้ DtoEntityMapperProfile เป็น Configuration Profile สำหรับการแมปข้อมูลระหว่าง object ในโปรเจคนี้
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IAddressService , AddressService>();
    }
}