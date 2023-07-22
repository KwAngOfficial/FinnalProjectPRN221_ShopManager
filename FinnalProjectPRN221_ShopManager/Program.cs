
using FinnalProjectPRN221_ShopManager.Hubs;
using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session sau 30 phút không hoạt động
});
//builder.Services.AddSignalR();
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("MyComDB");
builder.Services.AddDbContext<ShopManagementContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSession();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
//app.MapGet("/", () => "Hello World!");

app.MapRazorPages();

//app.MapHub<ProductHubs>("/productHub");
app.Run();