using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddAuthenticationHelpersHostBuilderExtensions {

    public static void AddAuthenticationHelpers(this IServiceCollection services) {

        services.AddScoped<IAuthCookieService, AuthCookieService>();
        services.AddScoped<IAuthenticator, Authenticator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPermissionsBuilder, PermissionsBuilder>();

    }

}
