using MailKit.Net.Smtp;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddEmailSupportHostBuilderExtensions {

    public static void AddEmailSupport(this IServiceCollection services) {
        services.AddScoped<ISmtpClient, SmtpClient>();
        services.AddScoped<ISmtpClientWrapper, SmtpClientWrapper>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailViewsBuilder, EmailViewsBuilder>();
    }

}
