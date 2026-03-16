using FastFrame.AspNetCore;
using FastFrame.AspNetCore.Middlewares;
using FastFrame.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DoCare
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFastFrame<AspPackManager>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/#/500");
                app.UseHsts().UseHttpsRedirection();
            }

            app.UseMiddleware<NodeExceptionHandlerMiddleware>()
               .UseDefaultFiles()
               .UseStaticFiles()
               .UseFastFrame();
        }
    }
}
