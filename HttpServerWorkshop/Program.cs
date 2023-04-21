namespace HttpServerWorkshop
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

            var app = builder.Build();
            app.UseMiddleware<LoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/users") && new[] { HttpMethod.Post.Method, HttpMethod.Delete.Method, HttpMethod.Put.Method }.Contains(context.Request.Method), builder =>
            {
                builder.UseMiddleware<SimpleAuthMiddleware>();
            });


            app.MapControllers();

            app.Run();
        }
    }
}