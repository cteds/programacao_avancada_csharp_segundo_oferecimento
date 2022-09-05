namespace deploy_sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Deploy Sample ({app.Environment.EnvironmentName})");
                });
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}