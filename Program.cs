
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace RateLimitting.Net8._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = 2; 
                    options.Window = TimeSpan.FromSeconds(10);
                    //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    //options.QueueLimit = 5;
                });

                options.AddConcurrencyLimiter("concurrency", options =>
                {
                    options.PermitLimit = 2;
                    //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    //options.QueueLimit = 5;

                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
            app.UseRateLimiter();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
