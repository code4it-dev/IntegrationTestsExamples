using IntegrationTestsExamples.SocialHandlers;

namespace IntegrationTestsExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ISocialLinkParser, SocialLinkParser>();
            builder.Services.AddScoped<SocialLinkParser>();
            builder.Services.AddScoped<InvalidPostHandler>();
            builder.Services.AddScoped<TwitterHandler>();
            builder.Services.AddScoped<LinkedInHandler>();

            builder.Services.AddScoped<ISocialLinksFactory, SocialLinksFactory>();
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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}