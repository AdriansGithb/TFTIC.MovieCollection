using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieCollectionAPI.Tools;
using MovieCollectionDAL.Repositories;
using MovieCollectionDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionAPI
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

            services.AddScoped<TokenManager>();
            services.AddScoped<IAppUserRepository, AppUserService>();
            services.AddScoped<IArtistRepository, ArtistService>();
            services.AddScoped<IAudienceRepository, AudienceService>();
            services.AddScoped<ICommentRepository, CommentService>();
            services.AddScoped<ICountryRepository, CountryService>();
            services.AddScoped<IGenreRepository, GenreService>();
            services.AddScoped<IMovieRepository, MovieService>();
            services.AddScoped<IActorRepository, ActorService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy => policy.RequireRole("admin"));
                options.AddPolicy("user", policy => policy.RequireRole("user", "admin"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager.secretKey)),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = TokenManager.issuer,
                    ValidateAudience = true,
                    ValidAudience = TokenManager.audience
                };
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod().
                AllowAnyHeader();
            }));


            services.AddSwaggerGen(c =>
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                    AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager.secretKey)),
                            ValidateLifetime = true,
                            ValidateIssuer = true,
                            ValidIssuer = TokenManager.issuer,
                            ValidateAudience = true,
                            ValidAudience = TokenManager.audience
                        };
                    });


                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieCollection Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", //!!!!!LOWERCASE!!!!!
                    BearerFormat = "JWT", //!!!UPPERCASE!!!!
                    In = ParameterLocation.Header, //Dans le Header Http



                    Description = "JWT Bearer : \r\n Enter Token"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieCollectionAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
