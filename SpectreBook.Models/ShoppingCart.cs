using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace SpectreBook.Models;

public class ShoppingCart
{
    public int Id { get; set; } 
    public int ProductId { get; set; }
    [ValidateNever]
    public Product product { get; set; }

    [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public int Count { get; set; }
    public string AppUserId { get; set; }
    [ValidateNever]
    public AppUser AppUser { get; set; }
}
