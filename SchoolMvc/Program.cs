using Microsoft.EntityFrameworkCore;
using SchoolMvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDb")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// /Student/ShowAll and /Student/ShowDetails?id=3 both resolve through this route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=ShowAll}/{id?}");

app.Run();
