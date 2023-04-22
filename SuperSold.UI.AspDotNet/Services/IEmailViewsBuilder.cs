namespace SuperSold.UI.AspDotNet.Services;

public interface IEmailViewsBuilder {
    string BuildRollbackEmailHtml(string email, string rollbackLink, string expireDate);
}
