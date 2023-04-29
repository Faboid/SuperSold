using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting.ViewComponents;

public class AccountDisplayBuilder : BaseViewComponentBuilder {
    public AccountDisplayBuilder(IViewComponentHelper vcHelper) : base(vcHelper, "AccountDisplay") {}

    public async Task<IHtmlContent> DisplayAsCell(AccountInfoModel account) => await Build(new { account, displayType = nameof(DisplayAsCell) });

}