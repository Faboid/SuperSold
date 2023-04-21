using MailKit;
using MimeKit;

namespace SuperSold.UI.AspDotNet.Services;

public interface IEmailService {
    Task Send(string username, string emailAddress, string body);
}

public class EmailService : IEmailService {

    private readonly ISmtpClientWrapper _smtp;

    public EmailService(ISmtpClientWrapper smtp) {
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

        await _smtp.SendAsync(email);

    }

}