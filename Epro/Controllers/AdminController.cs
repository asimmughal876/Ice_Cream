using Epro.Data;
using Epro.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Admin_Panel.Controllers
{
    public class AdminController : Controller
    {
        private readonly IceCreamContext db;

        public AdminController(IceCreamContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
            int totalUserCount = db.UserRecords.Count();
            int totalOrderCount = db.Orders.Count();
            int totalRecipeCount = db.Recipes.Count();
            int totalContactCount = db.Contacts.Count();
            int totalFeedbackCount = db.Feedbacks.Count();
            var model = new indexmodel
			{
                UserRecord = db.UserRecords.ToList(),
                TotalUserCount = totalUserCount,
                TotalOrderCount = totalOrderCount,
                TotalRecipeCount = totalRecipeCount,
                TotalContactCount = totalContactCount,
                TotalFeedbackCount = totalFeedbackCount
            };
			return View(model);

		}
		[Authorize(Roles = "Admin")]
        public IActionResult Display_User_Recipe()
        {
            return View(db.UserRecipes.Where(r => r.UserRecipeStatus == "Pending").ToList());
        }
        public IActionResult Delete_User_Recipe(int id)
        {
            var data = db.UserRecipes.FirstOrDefault(r => r.UserRecipeId == id);
            if (data != null)
            {
                db.Remove(data);
                db.SaveChanges();
                TempData["del"] = "Recipe has been Deleted Successfully";
            }
            return RedirectToAction(nameof(Display_User_Recipe));
        }
        [HttpGet]
        public IActionResult Approve_User_Recipe(int id)
        {
            var data = db.UserRecipes.FirstOrDefault(r => r.UserRecipeId == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Approve_User_Recipe(UserRecipe recipe)
        {
            recipe.UserRecipeStatus = "Approved";
            db.Update(recipe);
            db.SaveChanges();
            TempData["Recipe_Approve"] = "Recipe has been Approved Successfully";

            return RedirectToAction(nameof(Approved_User_Recipe));
        }

        public IActionResult Approved_User_Recipe()
        {
            return View(db.UserRecipes.Where(r => r.UserRecipeStatus == "Approved").ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Recipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Recipe(Recipe Recipe, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filename = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);
                if (exten_presize == "png" || exten_presize == "jpg" || exten_presize == "jpeg")
                {


                    var filepath = Path.Combine("wwwroot/assets/Admin_Panel/img", filename + '.' + exten_presize);
                    var dbth = Path.Combine("assets/Admin_Panel/img", filename + '.' + exten_presize);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Recipe.RecipeImage = dbth;
                    db.Add(Recipe);
                    db.SaveChanges();
                    TempData["Message"] = "Recipe has been Inserted Successfully";
                    return RedirectToAction(nameof(Display_Recipe));

                }
                TempData["img"] = "The image file should be in PNG, JPG, or JPEG format.";
            }
            return View();

        }

        [HttpGet]
        public IActionResult Edit_Recipe(int id)
        {
            var data = db.Recipes.FirstOrDefault(f => f.RecipeId == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit_Recipe(Recipe Recipe, IFormFile file, string img)
        {
            if (file != null && file.Length > 0)
            {
                var filename = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);
                if (exten_presize == "png" || exten_presize == "jpg" || exten_presize == "jpeg")
                {


                    var filepath = Path.Combine("wwwroot/assets/Admin_Panel/img", filename + '.' + exten_presize);
                    var dbth = Path.Combine("assets/Admin_Panel/img", filename + '.' + exten_presize);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Recipe.RecipeImage = dbth;
                }
                else
                {
                    TempData["img"] = "The image file should be in PNG, JPG, or JPEG format.";
                    return View(Recipe);
                }
            }
            else
            {
                Recipe.RecipeImage = img;
            }
            db.Update(Recipe);
            db.SaveChanges();
            TempData["urecipe"] = "Recipe has been Updated Successfully";

            return RedirectToAction(nameof(Display_Recipe));

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Display_Recipe()
        {
            return View(db.Recipes.ToList());
        }

        public IActionResult Delete_Recipe(int id)
        {
            var data = db.Recipes.FirstOrDefault(f => f.RecipeId == id);
            if (data != null)
            {
                db.Remove(data);
                db.SaveChanges();
                TempData["del"] = "Recipe has been Deleted Successfully";
            }
            return RedirectToAction(nameof(Display_Recipe));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DisplayRegister()
		{
			return View(db.UserRecords.Where(u => u.UserRoleId == 2).ToList());
		}

        [Authorize(Roles = "Admin")]
        public IActionResult DisplayFeedback()
        {
            return View(db.Feedbacks.ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DisplayContact()
        {
            return View(db.Contacts.ToList());
        }

        public IActionResult Display_Order()
        {
            return View(db.OrderItems.Include(o => o.OrderItemsOrder).ThenInclude(order => order.OrderUser).Where(s => s.OrderItemsOrder.OrderStatus == "Pending").Include(op => op.OrderItemsProd).ToList());
        }

        public IActionResult Order_Status(int? id)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.OrderStatus = "Delivered";

                db.Update(order);
            }
            TempData["status"] = "Status has been updated";
            db.SaveChanges();
            return RedirectToAction(nameof(Display_Approved_Order));
        }

        public IActionResult Display_Approved_Order()
        {
            return View(db.OrderItems.Include(o => o.OrderItemsOrder).ThenInclude(order => order.OrderUser).Where(s => s.OrderItemsOrder.OrderStatus == "Delivered").Include(op => op.OrderItemsProd).ToList());
        }


     

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
