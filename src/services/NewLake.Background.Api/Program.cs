var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://new-lake-seq", apiKey: "HEvI4EuLkzAjdVM8ZIXZ")
    .CreateLogger();

Log.Information("Starting web host");
var builder = WebApplication.CreateBuilder(args);

// remove default logging providers
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
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