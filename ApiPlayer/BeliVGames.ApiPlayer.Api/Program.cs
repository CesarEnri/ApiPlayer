using BeliVGames.ApiPlayer.Application;
using BeliVGames.ApiPlayer.Infrastructure;
using BeliVGames.ApiPlayer.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; 


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddPersistenceServices(configuration);


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeliVGames API");
    });
    app.UseDeveloperExceptionPage();

}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket Ticket Management API");
});

app.UseCors("Open");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();