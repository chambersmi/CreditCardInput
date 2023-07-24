
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;

namespace PaymentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //FOR ANGULAR
            builder.Services.AddCors();
            var configuration = builder.Configuration;
            builder.Services.AddDbContext<PaymentDetailContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DevConnection")));

            var app = builder.Build();

            //Must be called before the pipeline begins
            app.UseCors(options => options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod().AllowAnyHeader());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}