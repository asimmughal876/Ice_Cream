using Epro.Data;
using Epro.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Epro.Controllers
{
    public class HomeController : Controller
    {
        private readonly IceCreamContext db;

        public HomeController(IceCreamContext db)
        {
           this.db = db;
        }

        public IActionResult Index()
        {
            var model = new indexmodel
            {
                UserRecipe = db.UserRecipes.Where(r => r.UserRecipeStatus == "Approved").ToList(),
				Recipe = db.Recipes.Take(3).ToList()
		};
            return View(model);
        }

        public IActionResult about()
        {
            return View();
        }
        public IActionResult cart(int? Id)
        {
            ViewBag.mycart = HttpContext.Session.GetObject<List<Cart>>("Sess_Name");
            return View();
        }

		public IActionResult AddToCart(int? Id)
		{
			List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();
			Cart exisitng_item = cartItems.Find(item => item.RecipeId == Id);
			if (exisitng_item != null)
			{
				exisitng_item.Quantity += 1;
			}
			else
			{
				var mydata = db.Recipes.Find(Id);
				cartItems.Add(
				new Cart
				{
					RecipeId = mydata.RecipeId,
					RecipeName = mydata.RecipeName,
					Quantity = 1,
					Price = (int)mydata.RecipePrice
				}
					);
			}
			HttpContext.Session.SetObject<List<Cart>>("Sess_Name", cartItems);
			ViewBag.mycart = HttpContext.Session.GetObject<List<Cart>>("Sess_Name");
			return RedirectToAction(nameof(cart));
		}




		public IActionResult checkout(IFormCollection f)
        {
            List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();

            int total = 0;

            foreach (Cart item in cartItems)
            {
                total += item.Price * item.Quantity;
            }

         
            Order o = new Order();
	    o.TotalPrice = total;
            o.OrderStatus = "Pending";
            o.OrderUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            db.Add(o);
            db.SaveChanges();

            foreach (Cart itms in cartItems)
            {
                //insertion in item table
                OrderItem itemsTable = new OrderItem();
                itemsTable.OrderItemsProdId = itms.RecipeId;
                itemsTable.OrderItemsQuantity = itms.Quantity;
                itemsTable.OrderItemsOrderId = o.OrderId;
                db.Add(itemsTable);
                db.SaveChanges();
            }
            HttpContext.Session.SetObject("Sess_Name", "");
	    TempData["cart"] = "Transaction has been Complete";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult contact(Contact cont)
        {
            db.Add(cont);
            db.SaveChanges();
	    TempData["cont"] = "Thank you for reaching out!";
	    return RedirectToAction(nameof(contact));
        }
        [HttpGet]
        public IActionResult feedback()
        {
            return View();
        }
		[HttpPost]
		public IActionResult feedback(Feedback feed)
		{
			db.Add(feed);
			db.SaveChanges();
			TempData["feed"] = "Thank you for your feedback!";
			return RedirectToAction(nameof(feedback));
		}
		public IActionResult faq()
        {
            return View();
        }

        [Authorize(Roles ="User, Admin")]
		public IActionResult shop()
		{
			var Recipe = db.Recipes.ToList(); // Fetch flavors from the database
            return View(Recipe); // Pass flavors to the view
		}



        [HttpGet]
        public IActionResult User_Recipe()
        {
            return View();
        }
        [HttpPost]
        public IActionResult User_Recipe(UserRecipe recipe)
        {
            db.Add(recipe);
            db.SaveChanges();
            TempData["Recipe_send"] = "Recipe Send Successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRecord user)
        {
            if (ModelState.IsValid)
            {
                // Here you can hash the password before saving it to the database
                user.UserRoleId = 2;
                db.Add(user);
                db.SaveChanges();
                TempData["Message"] = "User Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View(user); // Returning the invalid model to display validation errors
        }

		public IActionResult Login(UserRecord User)
		{
			var data = db.UserRecords.FirstOrDefault(x => x.UserEmail == User.UserEmail && x.UserPassword == User.UserPassword);
			ClaimsIdentity identity = null;
			bool isAuthenticate = false;
			if (data != null)
			{

				if (data.UserRoleId == 1)
				{
					identity = new ClaimsIdentity(new[]
					{
			   new Claim(ClaimTypes.Name, data.UserName),
			   new Claim(ClaimTypes.Email, data.UserEmail),
				new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
			   new Claim(ClaimTypes.Role,"Admin")
		   }, CookieAuthenticationDefaults.AuthenticationScheme);
					isAuthenticate = true;
				}
				else
				{
					identity = new ClaimsIdentity(new[]
				 {
			  new Claim(ClaimTypes.Name, data.UserName),
			   new Claim(ClaimTypes.Email, data.UserEmail),
				new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
			   new Claim(ClaimTypes.Role,"User")

		   }, CookieAuthenticationDefaults.AuthenticationScheme);
					isAuthenticate = true;
				}
				if (isAuthenticate)
				{
					var principal = new ClaimsPrincipal(identity);
					var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    TempData["loginMsg"] = "login Successfully";

					if (data.UserRoleId == 1)
					{
						return RedirectToAction("Index", "Admin");
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}
			return View();
		}

		public IActionResult Logout()
		{
			var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction(nameof(Login));
		}


		public IActionResult minus(int? Id)
		{
			List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();
			Cart exisitng_item = cartItems.Find(item => item.RecipeId == Id);
			if (exisitng_item != null)
			{
				if (exisitng_item.Quantity > 1)
				{
					exisitng_item.Quantity -= 1;
				}
				else if (exisitng_item.Quantity == 1)
				{
					cartItems.Remove(exisitng_item);

				}
			}
			HttpContext.Session.SetObject<List<Cart>>("Sess_Name", cartItems);
			ViewBag.mycart = HttpContext.Session.GetObject<List<Cart>>("Sess_Name");
			return RedirectToAction(nameof(cart));
		}





		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}