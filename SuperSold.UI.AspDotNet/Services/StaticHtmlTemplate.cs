namespace SuperSold.UI.AspDotNet.Services;

public class StaticHtmlTemplate : IStaticHtmlTemplate {

    private readonly IWebHostEnvironment _hostEnvironment;
    public StaticHtmlTemplate(IWebHostEnvironment hostEnvironment) {
        _hostEnvironment = hostEnvironment;
    }

    public string GetHtmlTemplate(string name) {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "html", $"{name}.html");
        var template = File.ReadAllText(path);
        return template;
    }

    public async Task<string> GetHtmlTemplateAsync(string name) {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "html", $"{name}.html");
        var template = await File.ReadAllTextAsync(path);
        return template;
    }
}
