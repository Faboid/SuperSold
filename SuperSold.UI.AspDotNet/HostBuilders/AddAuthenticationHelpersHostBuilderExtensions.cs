using Microsoft.CodeAnalysis.CSharp.Syntax;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddAuthenticationHelpersHostBuilderExtensions {

    public static void AddAuthenticationHelpers(this IServiceCollection services) {

        services.AddScoped<IAuthenticator, Authenticator>();
        services.AddScoped<IAuthService, AuthService>();

    }

}
