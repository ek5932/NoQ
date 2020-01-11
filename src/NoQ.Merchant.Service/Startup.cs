using System;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NoQ.Merchant.Service.Configuration;
using NoQ.Merchant.Service.DataAccess.Contexts;
using NoQ.Merchant.Service.DataAccess.Repositories;
using NoQ.Merchant.Service.Domain.Repositorties;
using NoQ.Merchant.Service.WebApi;
using NoQ.Merchant.Service.WebApi.Mapping;

namespace NoQ.Merchant.Service
{
    public class Startup
    {
        private const string ApiName = "NoQ Merchant Api";
        private const string ApiVersion = "v1";

        private readonly IConfiguration _configuration;
        private readonly AuthenticationConfig _authConfig;

        public Startup(IConfiguration configuration)
        {
            var config = new AuthenticationConfig();
            configuration.Bind("Authentication", config);
            _authConfig = config;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddDbContext<MerchantDbContext>(options =>
                options.UseInMemoryDatabase("MerchantDb"));

            MockDataSeed.Initialize(services.BuildServiceProvider());
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IMerchantDetailsResourceMapper, MerchantDetailsResourceMapper>();

            // Swagger [oAuth] config
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = ApiName,
                    Version = ApiVersion
                });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{_authConfig.ServerUri}/connect/token"),
                            Scopes =
                            {
                               Scopes.ReadOnly,
                               Scopes.Write,
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { Scopes.ReadOnly.Key, Scopes.Write.Key }
                    }
                });
            });

            // Authserver middleware
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.ApiName = _authConfig.ApiName;
                        options.ApiSecret = _authConfig.ApiSecret;
                        options.Authority = _authConfig.ServerUri;
                        options.RequireHttpsMetadata = false;
                    });

            // Global policy
            services.AddMvc(options =>
            {
                var policy = ScopePolicy.Create(Scopes.ReadOnly.Key);
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Specific Policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Write, builder =>
                {
                    builder.RequireScope(Scopes.Write.Key);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(_authConfig.ClientName);
                options.OAuthClientSecret(_authConfig.ClientSecret);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiName} {ApiVersion}");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
