using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SuperSold.UI.AspDotNet.Services;

public interface IEmailService {
    Task Send(string username, string emailAddress, string body);
}

public class EmailService : IEmailService {

    private readonly ISmtpClient _smtp;

    public EmailService(ISmtpClient smtp) {
        _smtp = smtp;
    }

    //code derived from https://mailtrap.io/blog/csharp-send-email/
    public async Task Send(string username, string emailAddress, string body) {

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("SuperSold", "SuperSold@noreply.com"));
        email.To.Add(new MailboxAddress(username, emailAddress));

        email.Subject = "Testing out functionality.";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
            Text = body
        };

        await _smtp.ConnectAsync("sandbox.smtp.mailtrap.io", 2525, false);
        await _smtp.AuthenticateAsync(new SaslMechanismLogin("e5b094df837f8c", "5db3a96acedcb4"));
        _smtp.Send(email);
        await _smtp.DisconnectAsync(true);

    }

}
