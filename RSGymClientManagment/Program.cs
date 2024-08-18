using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// TODO MRS: ler o nome da connection string do appsettings.json
var connectionString =
builder.Configuration.GetConnectionString("RSGymClientManagment_ConnectionString");
// TODO MRS: registar o serviço da EF
builder.Services.AddDbContext<ClientManagmentContext>(options =>
options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
