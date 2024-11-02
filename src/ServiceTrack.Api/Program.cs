using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionStrings:DbConnection"));
});

builder.Services.AddControllers();

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

//MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RequestHandlerRegistrationHelper).Assembly);
});

//builder.Services.AddScoped<AuthService>();

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
