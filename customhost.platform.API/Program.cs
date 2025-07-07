using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using customhost_backend.GuestExperience.Application.Internal.CommandServices;
using customhost_backend.GuestExperience.Application.Internal.QueryServices;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.Shared.Infrastructure.Interfaces.ASP.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.Shared.Domain.Repositories;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Application.Internal.CommandServices;
using customhost_backend.crm.Application.Internal.QueryServices;
using customhost_backend.crm.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.crm.Infrastructure.Repositories;
using customhost_backend.crm.Infrastructure.Persistence.Repositories;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;
using customhost_backend.billings.Application.Internal.CommandServices;
using customhost_backend.billings.Application.Internal.QueryServices;
using customhost_backend.billings.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.profiles.Application.Internal.CommandServices;
using customhost_backend.profiles.Application.Internal.QueryServices;
using customhost_backend.profiles.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.analytics.Domain.Repositories;
using customhost_backend.analytics.Domain.Services;
using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.analytics.Application.Internal.QueryServices;
using customhost_backend.analytics.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.analytics.Infrastructure.ACL.External;
using customhost_backend.IAM.Application.Internal.CommandServices;
using customhost_backend.IAM.Application.Internal.OutboundServices;
using customhost_backend.IAM.Application.Internal.QueryServices;
using customhost_backend.IAM.Domain.Repositories;
using customhost_backend.IAM.Domain.Services;
using customhost_backend.IAM.Infrastructure.Hashing.BCrypt.Services;
using customhost_backend.IAM.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.IAM.Infrastructure.Tokens.JWT.Configuration;
using customhost_backend.IAM.Infrastructure.Tokens.JWT.Services;
using customhost_backend.IAM.Interfaces.ACL;
using customhost_backend.IAM.Interfaces.ACL.Services;
using customhost_backend.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction() && builder.Configuration.GetValue<int?>("PORT") is not null)
    builder.WebHost.UseUrls($"http://*{builder.Configuration.GetValue<int>("PORT")}");



// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add CORS Policy for Frontend Integration

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://customhost-frontend-hend.vercel.app", "http://localhost:5173")//ajustar
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

if(connectionString== null) throw new InvalidOperationException("Connection string not found.");



if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        });

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); 
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    }
    );
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "CustomHost_Backend",
            Version = "v1",
            Description = "CustomHost Backend API",
            TermsOfService = new Uri("https://customhost-pltaform.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "CustomHost Studios",
                Email = "CustomHost.com"
            },
            License = new OpenApiLicense
            {
                Name = "CustomHost",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();

builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<ISubscriptionPlanQueryService, SubscriptionPlanQueryService>();
builder.Services.AddScoped<ISubscriptionPlanCommandService, SubscriptionPlanCommandService>();

// CRM Bounded Context
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();

builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IBookingCommandService, BookingCommandService>();
builder.Services.AddScoped<IBookingQueryService, BookingQueryService>();
builder.Services.AddScoped<IStaffMemberCommandService, StaffMemberCommandService>();
builder.Services.AddScoped<IStaffMemberQueryService, StaffMemberQueryService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();

builder.Services.AddScoped<IServiceRequestCommandService, ServiceRequestCommandService>();
builder.Services.AddScoped<IServiceRequestQueryService, ServiceRequestQueryService>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

// Billings Bounded Context
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();


// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();



// Analytics Bounded Context
// Repositories
builder.Services.AddScoped<IAnalyticsSnapshotRepository, AnalyticsSnapshotRepository>();
builder.Services.AddScoped<IMetricDataRepository, MetricDataRepository>();

// Query Services
builder.Services.AddScoped<IAnalyticsSnapshotQueryService, AnalyticsSnapshotQueryService>();
builder.Services.AddScoped<IAnalyticsMetricQueryService, AnalyticsMetricQueryService>();

// ACL Facades
builder.Services.AddScoped<IGuestExperienceContextFacade, GuestExperienceContextFacade>();
builder.Services.AddScoped<ICrmContextFacade, CrmContextFacade>();
builder.Services.AddScoped<IBillingsContextFacade, BillingsContextFacade>();

// GuestExperience Bounded Context
// Repositories
builder.Services.AddScoped<IDeviceModelRepository, DeviceModelRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDevicePreferenceRepository, DevicePreferenceRepository>();
builder.Services.AddScoped<IUserDevicePreferenceRepository, UserDevicePreferenceRepository>();

// Command Services
builder.Services.AddScoped<IDeviceModelCommandService, DeviceModelCommandService>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
builder.Services.AddScoped<IDevicePreferenceCommandService, DevicePreferenceCommandService>();
builder.Services.AddScoped<IUserDevicePreferenceCommandService, UserDevicePreferenceCommandService>();

// Query Services
builder.Services.AddScoped<IDeviceModelQueryService, DeviceModelQueryService>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();
builder.Services.AddScoped<IDevicePreferenceQueryService, DevicePreferenceQueryService>();
builder.Services.AddScoped<IUserDevicePreferenceQueryService, UserDevicePreferenceQueryService>();


// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
// Dependency Injection for IAM Bounded Context
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<RoleInitializationService>();

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    
    // Initialize default roles
    var roleInitializationService = services.GetRequiredService<RoleInitializationService>();
    await roleInitializationService.InitializeDefaultRolesAsync();
}

// Configure the HTTP request pipeline.
app.UseSwagger(c =>
{
    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty; 
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

// Apply CORS Policy - Use specific frontend policy in production

//app.UseRequestAuthorization();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();