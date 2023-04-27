using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public abstract class BaseRoutes {

    private readonly IUrlHelper _urlHelper;
    private readonly string _controller;

    public BaseRoutes(IUrlHelper urlHelper, string controllerName)
    {
        _urlHelper = urlHelper;
        _controller = controllerName;
    }

    protected string? BuildUrlToAction(string action) => _urlHelper.Action(action, _controller);
    protected string? BuildAbsoluteUrlToAction(string action) {
        var scheme = _urlHelper.ActionContext.HttpContext.Request.Scheme;
        var routeValues = _urlHelper.ActionContext.HttpContext.Request.RouteValues;
        return _urlHelper.Action(action, _controller, routeValues, scheme);
    }

}