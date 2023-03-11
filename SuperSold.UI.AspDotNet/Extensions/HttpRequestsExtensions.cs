using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Web;

namespace SuperSold.UI.AspDotNet.Extensions;

public static class HttpRequestsExtensions {

    /// <summary>
    /// Returns whether the current request has been made through ajax.
    /// <br/>
    /// Using solution from: https://stackoverflow.com/a/43138693/16018958.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static bool IsAjaxRequest(this HttpRequest request) {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

}
