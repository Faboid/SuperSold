using Microsoft.CodeAnalysis.CSharp.Syntax;
using SuperSold.Identification;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddAuthenticationHelpersHostBuilderExtensions {

    public static void AddAuthenticationHelpers(this IServiceCollection services) {

        services.AddSingleton<IAuthenticator, Authenticator>();

    }

}
