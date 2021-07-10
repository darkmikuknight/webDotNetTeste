using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using webDotNet.Business;

namespace webDotNet.Business
{
    public class ServicesConfig
    {
        public static void Config(IServiceCollection services)
        {
            //Support Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return actionContext != null ? factory.GetUrlHelper(actionContext) : null;
            });

            services.AddScoped<PagamentoBusiness, PagamentoBusiness>();
        }
    }
}