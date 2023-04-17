using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

public class Product {

    [Key]
    public required Guid Id { get; set; }
    public required Guid SellerId { get; set; }
    public string UserImgUrl { get; set; } = "";

    [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters long.")]
    public required string Title { get; set; }

    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Must be between 10 and 2000 characters long.")]
    [DataType(DataType.MultilineText)]
    public required string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Must be a positive price.")]
    public required decimal Price { get; set; }

}
