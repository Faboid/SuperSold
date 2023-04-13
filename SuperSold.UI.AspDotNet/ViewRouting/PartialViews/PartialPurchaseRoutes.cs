using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuperSold.UI.AspDotNet.ViewRouting.PartialViews;

public class PartialPurchaseRoutes {

    private readonly IHtmlHelper _htmlHelper;

    public PartialPurchaseRoutes(IHtmlHelper htmlHelper) {
        _htmlHelper = htmlHelper;
    }

    public async Task<IHtmlContent> RenderFinalConfirmationView() => await _htmlHelper.PartialAsync("_FinalConfirmation.cshtml");
    public async Task<IHtmlContent> RenderCreditCardRequestView() => await _htmlHelper.PartialAsync("_CreditCardInformationRequest.cshtml");
    public async Task<IHtmlContent> RenderShippingInformationRequestView() => await _htmlHelper.PartialAsync("_ShippingInformationRequest.cshtml");

}
