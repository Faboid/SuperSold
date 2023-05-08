using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.AdminArea.Queries;


public record GetAccountDetailsQuery(OneOf<string, Guid> UsernameOrId) : IRequest<OneOf<AccountDetailsModel, NotFound>>;

public class GetAccountDetailsHandler : IRequestHandler<GetAccountDetailsQuery, OneOf<AccountDetailsModel, NotFound>> {

    private readonly IMapper _mapper;
    private readonly IAccountsHandler _accountsHandler;
    private readonly IProductsHandler _productsHandler;
    private readonly IAccountRolesHandler _accountRolesHandler;
    private readonly IAccountRestrictionsHandler _accountRestrictionsHandler;

    public GetAccountDetailsHandler(IMapper mapper, 
                                    IAccountsHandler accountsHandler, IProductsHandler productsHandler, 
                                    IAccountRolesHandler accountRolesHandler, IAccountRestrictionsHandler accountRestrictionsHandler) {
        _mapper = mapper;
        _accountsHandler = accountsHandler;
        _productsHandler = productsHandler;
        _accountRolesHandler = accountRolesHandler;
        _accountRestrictionsHandler = accountRestrictionsHandler;
    }

    public async Task<OneOf<AccountDetailsModel, NotFound>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken) {

        var accountOption = await request.UsernameOrId.Match(
            _accountsHandler.GetAccountByUserName,
            _accountsHandler.GetAccountById
        );

        if(accountOption.TryPickT1(out var notfound, out var account)) {
            return notfound;
        }

        var products = await _productsHandler
            .QueryProductsBySellerId(account.IdAccount)
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        var roles = await _accountRolesHandler.GetAccountRoles(account.IdAccount).Select(x => x.Role).ToListAsyncSafe();
        var restrictions = await _accountRestrictionsHandler.GetAccountRestrictions(account.IdAccount).Select(x => x.Restriction).ToListAsyncSafe();

        return new AccountDetailsModel(_mapper.Map<AccountInfoModel>(account), roles, restrictions, products);

    }
}
