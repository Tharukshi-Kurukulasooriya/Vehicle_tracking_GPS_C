using CEBVehicleTracker.Services;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpClient<IWialonService, WialonService>(client =>
{
    client.BaseAddress = new Uri("https://hst-api.wialon.com/");
});
builder.Services.AddSingleton<List<string>>(_ => new List<string>
{
    // Add your vehicle numbers here
 
});
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<VehicleHub>("/vehicleHub");

app.Run();