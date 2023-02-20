using ArcoDesign.Infra.JsRuntimes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoDesign.Extensions.DependencyInjects;
public static class DependencyInjectionExtension {
    public static IServiceCollection AddAcroDesign(this IServiceCollection services) {

        return services.AddScoped<Js>();
    }
}
