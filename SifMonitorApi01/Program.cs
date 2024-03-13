namespace SifMonitorApi01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors((c)=>c.AddPolicy("AllowOrigin", options=>
            options.AllowAnyOrigin().
            AllowAnyHeader().AllowAnyMethod()));
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.UseCors(c=>
            c.AllowAnyOrigin().
            AllowAnyHeader().
            AllowAnyMethod()
            );

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}