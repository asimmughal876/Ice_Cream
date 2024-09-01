using System;
using System.Collections.Generic;

namespace Epro.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public string? RecipeIngredients { get; set; }

    public string? RecipeImage { get; set; }

    public int? RecipePrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
