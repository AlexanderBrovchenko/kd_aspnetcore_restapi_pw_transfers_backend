using System;
using System.Threading.Tasks;
using AutoMapper;
using kd_pw_transfers_backend.Models;
using kd_pw_transfers_backend.Services;
using kd_pw_transfers_backend.Services.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace kd_pw_transfers_backend
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
            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(mySqlConnectionStr,
                mySqlOption =>
                    mySqlOption.ServerVersion("10.1.5-MariaDb")));
            var signingKey = new SigningSymmetricKey(AuthOptions.SIGNING_SECURITY_KEY);
            services.AddSingleton<IKdPwSigningEncodingKey>(signingKey);

            var encryptionEncodingKey = new EncryptingSymmetricKey(AuthOptions.ENCODING_SECURITY_KEY);
            services.AddSingleton<IKdPwEncryptingEncodingKey>(encryptionEncodingKey);

            var signingDecodingKey = (IKdPwSigningDecodingKey)signingKey;
            var encryptingDecodingKey = (IKdPwEncryptingDecodingKey)encryptionEncodingKey;
            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = AuthOptions.SCHEME;
                    options.DefaultChallengeScheme = AuthOptions.SCHEME;
                })
                .AddJwtBearer(AuthOptions.SCHEME, jwtBearerOptions => {
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),
                        TokenDecryptionKey = encryptingDecodingKey.GetKey(),

                        ValidateIssuer = false,
                        //ValidIssuer = AuthOptions.ISSUER,

                        ValidateAudience = false,
                        //ValidAudience = AuthOptions.AUDIENCE,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });
            services.AddScoped<IPublicService, PublicService>();
            services.AddScoped<IProtectedService, ProtectedService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
