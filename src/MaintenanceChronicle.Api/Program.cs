using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Api.Utils;
using MaintenanceChronicle.Application;
using MaintenanceChronicle.Application.Validators;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions => {
//    serverOptions.ListenAnyIP(7290); // Change the port number here
//});

// Add services to the container.
//These services are needed fot the ICurrentTenantProvider
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();

//Method for global filter into db
builder.Services.AddScoped<ICurrentTenantProvider, CurrentTenantProvider>();

builder.Services.AddAuthentication();

//DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionStrings:DbConnection"), optionsBuilder =>
    {
        optionsBuilder.UseNodaTime();
        optionsBuilder.MapEnum<RecordType>("recordType");
    });
    
});

//Use PATCH endpoints
builder.Services.AddControllers()
    .AddNewtonsoftJson();

//Identity
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

//Clock
builder.Services.AddSingleton<IClock>(SystemClock.Instance);

//Registering middleware to validate if user has access to tenant
builder.Services.AddScoped<UserTenantValidationMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MaintenanceChronicleApi", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserTenantValidationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();

app.MapControllers();

//Testing purposes
app.MapGet("/", () => "This page wooooooorks");

app.Run();
