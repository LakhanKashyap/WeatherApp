using WeatherApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Configure Swagger to include XML documentation from our code comments
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(
        Path.Combine(
            AppContext.BaseDirectory,
            $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

//Dependency Injection for HTTP Client
builder.Services.AddHttpClient();

//Dependency Injection for WeatherService
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();




