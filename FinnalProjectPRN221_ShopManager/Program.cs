
using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("MyComDB");
builder.Services.AddDbContext<ShopManagementContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();


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
//app.MapHub<ProductHub>("/productHub");
app.Run();