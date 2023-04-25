using MimeKit;

namespace SuperSold.UI.AspDotNet.Services;

public interface IEmailService {
    Task Send(string username, string emailAddress, string body);
}

public class EmailService : IEmailService {

    private readonly ISmtpClientWrapper _smtp;
    private readonly ILogger<EmailService> _logger;

    public EmailService(ISmtpClientWrapper smtp, ILogger<EmailService> logger) {
        _smtp = smtp;
        _logger = logger;
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
        _logger.LogInformation("Sent email to {email} successfully.", emailAddress);

    }

}

/// <summary>
/// A class to fake sending emails. Used to save on API requests when debugging.
/// </summary>
public class FakeEmailService : IEmailService {

    private readonly ILogger<FakeEmailService> _logger;

    public FakeEmailService(ILogger<FakeEmailService> logger) {
        _logger = logger;
    }

    public Task Send(string username, string emailAddress, string body) {
        _logger.LogWarning("Faked sending email to {email}. Remember to change injected IEmailService class for production.", emailAddress);
        return Task.CompletedTask;
    }
}