using GroepsProject1._3Backend.WebApi;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddTransient<IInfoRepository, InfoRepository>(o => new InfoRepository(sqlConnectionString));
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
