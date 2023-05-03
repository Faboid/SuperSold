using MailKit.Net.Smtp;
using SuperSold.UI.AspDotNet.Secrets;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddEmailSupportHostBuilderExtensions {

    public static void AddEmailSupport(this IServiceCollection services, EmailAuth emailAuth) {

        services.AddScoped<ISmtpClient, SmtpClient>();
        services.AddScoped<ISmtpClientWrapper, SmtpClientWrapper>(s => {
            return new(s.GetRequiredService<ISmtpClient>(), emailAuth);
        });

        //services.AddScoped<IEmailService, FakeEmailService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IEmailViewsBuilder, EmailViewsBuilder>();
    }

}
