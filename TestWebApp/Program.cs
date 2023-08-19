using System.Diagnostics;
Environment.SetEnvironmentVariable("GEOSERVER_HOME", @"C:\GeoServer");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=map}/{action=Index}/{id?}");

var geoserverProcess = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "cmd",
        Arguments = "/C start \"GeoServer\" \"C:\\geoserver\\bin\\startup.bat\"",
    }
};
geoserverProcess.Start();
app.Run();
