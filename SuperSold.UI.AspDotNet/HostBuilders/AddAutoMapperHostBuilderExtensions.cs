using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddAutoMapperHostBuilderExtensions {

    public static void AddAutoMapper(this IServiceCollection services) {

        services.AddAutoMapper(cfg => {
            cfg.CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProduct))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.IdSellerAccount))
                .ForMember(dest => dest.UserImgUrl, opt => opt.MapFrom(src => src.ImageUrl));

            cfg.CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.IdProduct, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdSellerAccount, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImgUrl));

            cfg.CreateMap<SavedRelationshipModel, SavedRelationship>();

            cfg.CreateMap<SavedRelationshipWithProduct, ProductWithSavedRelationship>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.SavedRelationship, opt => opt.MapFrom(src => src.SavedRelationship));

            cfg.CreateMap<AccountModel, AccountInfoModel>();

        });

    }

}
