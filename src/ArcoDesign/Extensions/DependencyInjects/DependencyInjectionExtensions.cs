using ArcoDesign.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ArcoDesign.Extensions;
public static class DependencyInjectionExtension {
    public static IServiceCollection AddAcroDesign(this IServiceCollection services) {

        return services.AddScoped<Js>();
    }
}
