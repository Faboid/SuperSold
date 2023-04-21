using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SuperSold.UI.AspDotNet.Services;

public class SmtpClientWrapper : ISmtpClientWrapper, IAsyncDisposable {

    private readonly ISmtpClient _client;

    public SmtpClientWrapper(ISmtpClient client) {
        _client = client;
    }

    public async Task SendAsync(MimeMessage email) {

        if(!_client.IsConnected) {
            await ConnectAsync();
        }

        if(!_client.IsAuthenticated) {
            await AuthenticateAsync();
        }

        await _client.SendAsync(email);
        await DisconnectAsync();

    }

    private async Task ConnectAsync() => await _client.ConnectAsync("sandbox.smtp.mailtrap.io", 2525, false);
    private async Task AuthenticateAsync() => await _client.AuthenticateAsync(new SaslMechanismLogin("e5b094df837f8c", "5db3a96acedcb4"));
    private async Task DisconnectAsync() => await _client.DisconnectAsync(true);
    public async ValueTask DisposeAsync() {
        
        if(_client.IsConnected) {
            await DisconnectAsync();
        }
        
        GC.SuppressFinalize(this);
    }
}