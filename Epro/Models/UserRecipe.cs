using System;
using System.Collections.Generic;

namespace Epro.Models;

public partial class UserRecipe
{
    public int UserRecipeId { get; set; }

    public string? UserRecipeName { get; set; }

    public string? UserRecipeIngredients { get; set; }

    public string? UserRecipeStatus { get; set; }

    public string? UserRecipeUserName { get; set; }

    public string? UserRecipePrize { get; set; }
}
