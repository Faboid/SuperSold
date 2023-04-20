using MailKit;
using MailKit.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddEmailSupportHostBuilderExtensions {

    public static void AddEmailSupport(this IServiceCollection services) {

        services.AddScoped<ISmtpClient, SmtpClient>();
        services.AddScoped<IEmailService, EmailService>();

    }

}
