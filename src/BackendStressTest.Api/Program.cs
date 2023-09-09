
using BackendStressTest.Api.Configurations;
using Dapper;

namespace BackendStressTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependencyInjections(builder.Configuration);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new DateOnlyConfiguration());
            });

            SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(o =>
            {
                o.SupportNonNullableReferenceTypes();
                o.MapType<DateOnly>(() => new()
                {
                    Type = "string",
                    Example = new Microsoft.OpenApi.Any.OpenApiString("yyyy-mm-dd")
                });
            });

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapHealthChecks("/healthz");

            app.MapControllers();

            app.Run();
        }
    }
}
