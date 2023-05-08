using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.AdminArea.Queries;


public record SearchAccountsByUsernameQuery(string Username) : IRequest<List<AccountInfoModel>>;

public class SearchAccountsByUsernameHandler : IRequestHandler<SearchAccountsByUsernameQuery, List<AccountInfoModel>> {

    private readonly IAccountsHandler _accountsHandler;
    private readonly IMapper _mapper;

    public SearchAccountsByUsernameHandler(IAccountsHandler accountsHandler, IMapper mapper) {
        _accountsHandler = accountsHandler;
        _mapper = mapper;
    }

    public async Task<List<AccountInfoModel>> Handle(SearchAccountsByUsernameQuery request, CancellationToken cancellationToken) {
        
        return await _accountsHandler
            .QueryAccounts()
            .Where(x => string.IsNullOrWhiteSpace(request.Username) || x.UserName.Contains(request.Username))
            .ProjectTo<AccountInfoModel>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

    }
}
