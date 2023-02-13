using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

public class Product {

    [Key]
    public required Guid Id { get; set; }
    public string UserImgUrl { get; set; } = "";

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }

}
