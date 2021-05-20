using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dangl.Data.Shared.AspNetCore.Validation;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class IntegrationTestsStartup
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvcSetup =>
            {
                mvcSetup.Filters.Add(typeof(ModelStateValidationFilter));
                mvcSetup.Filters.Add(typeof(RequiredFormFileValidationFilter));
                mvcSetup.AddEmptyFormFileValidator();
            })
            .AddApplicationPart(typeof(IntegrationTestsStartup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseClientCompressionSupport();
            app.UseHttpHeadToGetTransform();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

    }
}
