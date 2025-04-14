using BLL.Interfaces;
using BLL.Repos;
using DAL.Contexts;
using DAL.Models;
using Doctor_sAppointment.MappingProfiles;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Doctor_sAppointment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            //--------------------------Services-------------------------------
            //-----------------------------------------------------------------

            var connectionString =  builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string'DefaultConnection' not found.");
            builder.Services.AddDbContext<DoctorDbContext>(options => options.UseSqlServer(connectionString));


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            //--------------------------Profiles-------------------------------

            builder.Services.AddAutoMapper(P => { 
                                                P.AddProfile<DoctorPtofile>();
                                                P.AddProfile<DoctorScheduleProfile>();
                                                P.AddProfile<UserProfile>();
                                                P.AddProfile<PatientProfile>();
                                                });

            //--------------------------ForIdentity-------------------------------

            builder.Services.AddIdentity<AccountUser, IdentityRole>()
                            .AddEntityFrameworkStores<DoctorDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/AccountUser/Login";
                options.AccessDeniedPath = "/Home/Error";
            });

            //-----------------------------------------------------------------
            //--------------------------Services-------------------------------



            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();


            app.UseAuthentication();  
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
