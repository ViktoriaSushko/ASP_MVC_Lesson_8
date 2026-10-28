using ASP_MVC_Lesson_8.AutoMapperProfiles;
using ASP_MVC_Lesson_8.Data;
using ASP_MVC_Lesson_8.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connStr = builder.Configuration.GetConnectionString("MSSQLCatsDB");
builder.Services.AddDbContext<CatsContext>(options => options.UseSqlite(connStr));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BreedProfile>();
    cfg.AddProfile<CatProfile>();
});
var app = builder.Build();
using(IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
    await CatsSeed.SeedData(serviceProvider, app.Environment);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
