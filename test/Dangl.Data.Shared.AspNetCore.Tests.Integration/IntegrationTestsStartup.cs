using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseClientCompressionSupport();
            app.UseHttpHeadToGetTransform();
            app.UseMvc();
        }
    }
}
