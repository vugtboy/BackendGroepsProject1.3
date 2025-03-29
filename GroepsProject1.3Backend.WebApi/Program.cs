using GroepsProject1._3Backend.WebApi;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GroepsProject1._3Backend.WebApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddTransient<IInfoRepository, InfoRepository>(o => new InfoRepository(sqlConnectionString));
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>(o => new AppointmentRepository(sqlConnectionString));

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();


builder.Services.AddAuthorization();
builder.Services
.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
})
.AddDapperStores(options =>
{
    options.ConnectionString = sqlConnectionString;
});

builder.Services
    .AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme)
    .Configure(options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromMinutes(value: 60);
    });

var app = builder.Build();
app.UseAuthorization();

app.MapGroup(prefix: "/account")
    .MapIdentityApi<IdentityUser>();

app.MapPost(pattern: "account/logout",
    async (SignInManager<IdentityUser> singInManager,
    [FromBody] object empty) =>
    {
        if (empty != null)
        {
            await singInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .RequireAuthorization();

app.MapControllers().RequireAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello world, the API is up ");


app.UseHttpsRedirection();

app.MapControllers();

app.Run();