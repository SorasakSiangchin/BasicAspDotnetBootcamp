using CourseProject.Business;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Models;
using CourseProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//คำสั่ง using ใน C# ใช้สำหรับการกำหนด scope ที่จะทำการ dispose ออบเจ็กต์ที่เป็น IDisposable(แบบใช้แล้วทิ้ง) ทันทีเมื่อออกจาก scope นั้น
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    services.Database.EnsureCreated(); //  บรรทัดนี้จะตรวจสอบว่าฐานข้อมูลสำหรับแอปพลิเคชันนี้มีอยู่แล้วหรือไม่ ถ้ายังไม่มี Entity Framework Core จะสร้างฐานข้อมูลให้ การทำงานนี้เป็นสิ่งที่มีประโยชน์ในสภาพแวดล้อมการพัฒนาที่คุณอาจต้องการสร้างฐานข้อมูลใหม่ทุกครั้งที่คุณเรียกใช้แอปพลิเคชัน แต่ในสภาพแวดล้อมการผลิต คุณมักจะใช้การย้ายฐานข้อมูล (migrations) 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();