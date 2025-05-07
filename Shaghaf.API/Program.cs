using Microsoft.EntityFrameworkCore;
using Shaghaf.Application.Mappings;
using Shaghaf.Core;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Infrastructure;
using Shaghaf.Infrastructure.Data;
using System.Text.Json.Serialization;
using Stripe;
using Shaghaf.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shaghaf.Infrastructure.IdentityData;
using Microsoft.AspNetCore.Identity;
using Shaghaf.Core.Entities.IdentityEntities;
using Shaghaf.Infrastructure.IdentityData.SeedData;
using Shaghaf.Infrastructure.Services;
using System.Text.Json;
using Shaghaf.Service.Sevices.Implementaion;
using Shaghaf.Infrastructure.Repositories.Implementation;
using StackExchange.Redis;
using Stripe.Climate;
using Microsoft.OpenApi.Models;
using Shaghaf.Core.Entities.OrderEntities;
using Shaghaf.Service.Services.Implementation;
using Shaghaf.Service.Settings;

namespace Shaghaf.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Access configuration
            var configuration = builder.Configuration;

            // Add services to the container    .
            #region Configure Services

            // Configure JSON serialization options
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // Handle circular references
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Ignore null values
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shaghaf API", Version = "v1" });
            });

            #region Database and RedisDb Connections

            builder.Services.AddDbContext<StoreContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

            //Allow (DI) to Redis DB
            //why AddSingleton ? to 1.once connection opened remains present 2. To caching 
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            // Add DbContext for AppIdentity with SQL Server configuration          
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))); 
            #endregion

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRoomService), typeof(RoomService));
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBirthdayService, BirthdayService>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IPhotoSessionService, PhotoSessionService>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IMenuItemService, MenuItemService>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
 
            builder.Services.AddScoped<IOrderService, Shaghaf.Service.Services.Implementation.OrderService>();


            // builder.Services.AddScoped<IOrderService, OrderService>();
            // builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //DI to AuthService
            builder.Services.AddTransient<IAuthService, AuthService>();

            builder.Services.AddLogging(); // Ensure logging is added

            #region Stripe configurations
            // Add Stripe configuration settings
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            // Retrieve Stripe settings and set the API key
            var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();
            StripeConfiguration.ApiKey = stripeSettings.SecretKey;
            #endregion

            #region Twilio configurations
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
            builder.Services.AddTransient<ISMSService, SMSService>();
            #endregion

            #region JWT configurations

            // Add Identity services to the application
            // IdentityRole: Represents a role in the ASP.NET Core Identity system
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders(); // This adds token providers like DataProtectionTokenProvider
            // Bind the JWT configuration section from appsettings.json to the JWT settings class
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

            // Configure JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Set default authentication scheme
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Set default challenge scheme
            })
            .AddJwtBearer(options =>
            {
                // Set token validation parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Validate the issuer
                    ValidateAudience = true, // Validate the audience
                    ValidateLifetime = true, // Validate the token lifetime
                    ValidateIssuerSigningKey = true, // Validate the signing key
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // Issuer from configuration
                    ValidAudience = builder.Configuration["Jwt:Audience"], // Audience from configuration
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Signing key from configuration
                };

                // Configure JWT events
                options.Events = new JwtBearerEvents
                {
                    // Handle authentication failure
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new { message = "Authentication failed." });
                        return context.Response.WriteAsync(result);
                    },
                    // Handle challenge
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new { message = "You are not authorized to access this resource." });
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            #endregion

            #endregion

            var app = builder.Build();
            #region Seed roles
            // Seed roles during application program
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            #endregion

            #region Configure MiddleWares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shaghaf API v1");
                });
            }

            app.UseMiddleware<CustomAuthenticationMiddleware>(); // Use custom authentication middleware
            app.UseStatusCodePagesWithReExecute("/errors/{0}"); //0 =>code,Redirect to Specific Endpoint

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseRouting(); // Use routing

            app.UseAuthentication(); // Use authentication
            app.UseAuthorization(); // Use authorization

            app.MapControllers(); // Map controllers

            app.Run(); // Run the application
            #endregion
        }
    }
}
