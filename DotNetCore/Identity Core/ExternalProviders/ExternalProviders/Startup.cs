using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExternalProviders.Data;
using ExternalProviders.Models;
using ExternalProviders.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ExternalProviders
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Adicionar service externos
            services.AddAuthentication().AddFacebook(facebookOption =>
            {
                facebookOption.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOption.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                facebookOption.ClaimActions.MapJsonKey(ClaimTypes.Locality, "locale");

                facebookOption.SaveTokens = true;
            }).AddGoogle(googleOption => 
            {
                googleOption.SaveTokens = true;
                googleOption.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOption.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            }).AddTwitter(twitterOptions => 
            {
                twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
                twitterOptions.SaveTokens = true;
                twitterOptions.RetrieveUserDetails = true; // Informar para o twitter retornar os dados de login
                twitterOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email); //  Pegar o email informado no login

            }).AddMicrosoftAccount(maOption => 
            {
                maOption.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                maOption.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
                maOption.SaveTokens = true;
            }).AddLinkedIn(linkedInOption => 
            {
                linkedInOption.ClientId = Configuration["Authentication:Linkedin:ClientId"];
                linkedInOption.ClientSecret = Configuration["Authentication:Linkedin:ClientSecret"];
                linkedInOption.SaveTokens = true;
            });

            

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
