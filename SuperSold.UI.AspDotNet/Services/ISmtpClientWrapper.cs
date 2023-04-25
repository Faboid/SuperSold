using MimeKit;

namespace SuperSold.UI.AspDotNet.Services;

public interface ISmtpClientWrapper {
    Task SendAsync(MimeMessage email);
}
