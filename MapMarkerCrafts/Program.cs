
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MapMarkerCrafts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var app = builder.Build();
            app.Run();
        }
    }
}
