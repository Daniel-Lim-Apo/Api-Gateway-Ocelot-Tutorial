using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WebApplication2;


public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://test";
                options.RequireHttpsMetadata = false;
                options.Audience = "test_api";
                options.SaveToken = true;
            });
        services.AddOcelot();


        services.AddAuthorization();

        services.AddLogging();

        services.AddMvc();

        services.AddSwaggerForOcelot(Configuration,
          (o) =>
          {
              o.GenerateDocsDocsForGatewayItSelf(opt =>
              {
                  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                  opt.FilePathsForXmlComments = new string[] { xmlPath };
                  opt.GatewayDocsTitle = "My Gateway";
                  opt.GatewayDocsOpenApiInfo = new()
                  {
                      Title = "My Gateway",
                      Version = "v1",
                  };
                  opt.DocumentFilter<SwaggerControllersFilter>();
                  opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                  {
                      Type = SecuritySchemeType.OAuth2,
                      Flows = new OpenApiOAuthFlows
                      {
                          AuthorizationCode = new OpenApiOAuthFlow
                          {                              
                              AuthorizationUrl = new Uri($"https://YourSite.com/connect/authorize"),
                              TokenUrl = new Uri($"https://accounts.YourSite.com/token"),
                              Scopes = new Dictionary<string, string> {
                                { "api_gateway", "API GATEWAY" }, 
                            }
                          }
                      }
                  });
                  opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                      {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header, 
                          },
                          new List<string>()
                      }
                  });
              });
          });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //app.UseSwaggerUI(c =>
        //{
        //    c.SwaggerEndpoint("v1/swagger.json", "My API V1");
        //});
        app.UseSwagger();
        //app.UseSwaggerUI(c =>
        //{
        //    // c.SwaggerEndpoint($"/swagger/v1/swagger.json", "API GATEWAY");

        //    c.OAuthClientId("ServiceTest_api_swaggerui");
        //    c.OAuthAppName("API GATEWAY");
        //    c.OAuthUsePkce();
        //    c.DefaultModelsExpandDepth(-1);
        //});
        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.PathToSwaggerGenerator = "/swagger/docs";
            
        });

        app.UseAuthentication()
           .UseAuthorization()
           .UseOcelot().Wait();
    }
}