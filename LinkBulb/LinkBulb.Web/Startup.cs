using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkBulb.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace LinkBulb.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddInstagram(instagramOptions =>
            {
                instagramOptions.ClientId = "235c6a5a6eeb476b8168fd5d7c1aba71";
                instagramOptions.ClientSecret = "5a8294b250aa490691639ec8a40f4f65";

                instagramOptions.Events.OnCreatingTicket = ctx =>
                {
                    var id = ctx.User.Value<string>("id");
                    var username = ctx.User.Value<string>("username");
                    var profilePicture = ctx.User.Value<string>("profile_picture");

                    /*
                    var db = ctx.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

                    var user = db.UserLogins.Where(c => c.ProviderKey == id).FirstOrDefault();

                    if (user != null)
                    {
                        if (db.UserClaims.Where(c => c.UserId == user.UserId).Count() == 0)
                        {
                            var claim = new IdentityUserClaim<string>() { UserId = user.UserId, ClaimType = ClaimTypes.UserData, ClaimValue = username };
                            var claims = db.UserClaims.Add(claim);

                            claim = new IdentityUserClaim<string>() { UserId = user.UserId, ClaimType = ClaimTypes.Uri, ClaimValue = profilePicture };
                            claims = db.UserClaims.Add(claim);

                            db.SaveChanges();
                        }
                    }
                    */

                    return Task.CompletedTask;
                };
            }).AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = "vVUAwGLq4gxTq3mKcfUVZHtP7";
                twitterOptions.ConsumerSecret = "iBiotUZn236PqRe9NpRJA7G5mBve5xoefEPfcVp4qWfvEkyV7t";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

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
