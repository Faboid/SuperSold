using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddStaticResourcesHostBuilderExtensions {

    public static void AddStaticHtmlResources(this IServiceCollection services) {
        services.AddSingleton<IStaticHtmlTemplate, StaticHtmlTemplate>();
    }

}
