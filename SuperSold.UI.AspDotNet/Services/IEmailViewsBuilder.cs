namespace SuperSold.UI.AspDotNet.Services;

public interface IEmailViewsBuilder {
    string BuildForgotPasswordEmailHtml(string rollbackLink, string expireDate);
    string BuildRollbackEmailHtml(string email, string rollbackLink, string expireDate);
}
