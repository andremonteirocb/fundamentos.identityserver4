using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Fundamentos.IS4.Frete.Fretes.Context;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;
using System;

namespace Fundamentos.IS4.Frete.Fretes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //não associar os schema do xml aos nomes default das claims 
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            //profile information para detalhar os erros no debuger
            if (Debugger.IsAttached)
                IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(defaultScheme: "Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["IdentityServer4:Authority"];
                    options.Audience = Configuration["IdentityServer4:Audience"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityServer4:RequireHttpsMetadata"]);
                });

            services.AddDbContext<FreteContext>(options => options.UseInMemoryDatabase("frete-context"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Fretes", Version = "v1" });

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Gerente", policy =>
                //    policy.RequireClaim("cargo", "Gerente"));

                options.AddPolicy("Gerente", policy =>
                    policy.RequireClaim("cargo", "Gerente"));

                options.AddPolicy("B2B", policy =>
                    policy.RequireAssertion(ctx => ctx.User.Identity.IsAuthenticated || ctx.User.HasClaim("parceiro", "frete")));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "API de Fretes");

                c.DocExpansion(DocExpansion.List);
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
