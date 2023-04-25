namespace SuperSold.UI.AspDotNet.Services;

public interface IStaticHtmlTemplate {
    string GetHtmlTemplate(string name);
    Task<string> GetHtmlTemplateAsync(string name);
}
