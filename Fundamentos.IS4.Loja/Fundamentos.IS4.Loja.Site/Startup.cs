using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Fundamentos.IS4.Loja.Data.Configuration;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Site.Configure;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;

namespace Fundamentos.IS4.Loja.Site
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
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddHttpContextAccessor();

            //n�o associar os schema do xml aos nomes default das claims 
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            //profile information para detalhar os erros no debuger
            if (Debugger.IsAttached)
                IdentityModelEventSource.ShowPII = true;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Configuration["Keycloak:Authority"];
                    options.ClientId = Configuration["Keycloak:ClientId"];
                    options.ClientSecret = Configuration["Keycloak:ClientSecret"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["Keycloak:RequireHttpsMetadata"]);
                    options.SaveTokens = true;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    //options.Scope.Add("api_frete"); //protected resources 

                    //recupera as claims do usu�rio
                    //recupera as claims extras presentes no protected resources
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Events.OnUserInformationReceived = context =>
                    {
                        options.ClaimActions.MapUniqueJsonKey("cargo", "cargo");
                        options.ClaimActions.MapUniqueJsonKey("picture", "picture");
                        return Task.CompletedTask;
                    };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            // Dbcontext config
            services.ConfigureProviderForContext<LojaContext>(DetectDatabase);
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/conta/entrar");
            });
            services.ConfigureDI();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Definindo a cultura padr�o: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// it's just a tuple. Returns 2 parameters.
        /// Trying to improve readability at ConfigureServices
        /// </summary>
        private (DatabaseType, string) DetectDatabase => (
            Configuration.GetValue<DatabaseType>("ApplicationSettings:DatabaseType"),
            Configuration.GetConnectionString("DefaultConnection"));
    }
}
