using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.DTOs;

public class CategoryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

}
public class CreateCategoryDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}

public class UpdateCategoryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}
// public class CategoryWithProductsDto
// {
//     public int Id { get; set; }
//     public string Name { get; set; } = string.Empty;
//     public List<ProductSummaryDto> Products { get; set; } = new();
// }