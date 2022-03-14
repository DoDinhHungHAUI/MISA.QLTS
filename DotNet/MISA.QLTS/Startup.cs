using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using MISA.ApplicationCore.Service;
using MISA.Infrastructure;
using MISA.QLTS.MiddleWare;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.QLTS", Version = "v1" });
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
          
            services.AddScoped<IDepartmentAssetRepository, DepartmentAssetRepository>();
            services.AddScoped<IDepartmentAssetService, DepartmentAssetService>();

            services.AddScoped<IFixedAssetCategoryRepository, FixedAssetCategoryRepository>();
            services.AddScoped<IFixedAssetCategoryService, FixedAssetCategoryService>();

            services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
            services.AddScoped<IFixedAssetService, FixedAssetService>();

            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IVoucherService, VoucherService>();

            services.AddScoped<IOriginFundingRepository, OriginFundingRepository>();
            services.AddScoped<IOriginFundingService, OriginFundingService>();

            services.AddScoped<IOriginFundingFixedAssetRepository, OriginFundingFixedAssetRepository>();
            services.AddScoped<IOriginFundingFixedAssetService, OriginFundingFixedAssetService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.QLTS v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
