using AppCore.Models;
using Infastructure.Services;
using Infastructure.Services.Interfaces;
using Infrastracture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ProjectExam
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("localDb");
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




            builder.Services.AddControllers();
            
            builder.Services.AddScoped<ICountryService,CountryService>();
            builder.Services.AddScoped<UniversityService>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
   //public partial class Program
   // {

   // }
}
