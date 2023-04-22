namespace SuperSold.UI.AspDotNet.Services;

public class EmailViewsBuilder : IEmailViewsBuilder {

    private readonly IStaticHtmlTemplate _htmlTemplate;
    public EmailViewsBuilder(IStaticHtmlTemplate htmlGetter) {
        _htmlTemplate = htmlGetter;
    }

    public string BuildRollbackEmailHtml(string email, string rollbackLink, string expireDate) {
        var page = _htmlTemplate.GetHtmlTemplate("RollbackEmailTemplate");

        //replace values
        page = page.Replace("{{email}}", email);
        page = page.Replace("{{rollbackLink}}", rollbackLink);
        page = page.Replace("{{expireDate}}", expireDate);

        return page;
    }

}
