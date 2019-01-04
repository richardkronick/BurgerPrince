using BurgerPrince.Database;
using BurgerPrince.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace BurgerQueen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new BurgerPrinceContext())
            {
                List<MenuItem> menuItems = context.MenuItems.ToList();

                return View(menuItems);
            };
        }

        [HttpPost]
        public RedirectToRouteResult UpdateMenuItem(int id, int quantity)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BurgerPrinceContext())
                {
                    if (context.MenuItems.Any(m => m.ID == id))
                    {
                        var menuItem = context.MenuItems.Single(m => m.ID == id);

                        menuItem.Quantity += quantity;
                        menuItem.Subtotal += (menuItem.Price * quantity);
                        menuItem.Inventory -= quantity;

                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult DeleteMenuItem(int id)
        {
            using (var context = new BurgerPrinceContext())
            {
                if (context.MenuItems.Any(m => m.ID == id))
                {
                    var menuItem = context.MenuItems.Single(m => m.ID == id);

                    menuItem.Quantity = 0;
                    menuItem.Subtotal = 0.0m;
                    menuItem.Inventory = menuItem.InitialInventory;

                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult NewOrder()
        {
            using (var context = new BurgerPrinceContext())
            {
                List<MenuItem> menuItems = context.MenuItems.ToList();

                foreach (var item in menuItems)
                {
                    item.Quantity = 0;
                    item.Subtotal = 0.0m;
                    //item.Inventory = item.InitialInventory; <- Uncomment to make inventory return to initialInventory after each order
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.userId = User.Identity.GetUserId();
            }

            ViewBag.Message = "Soon to be implemented, this feature will allow logged in users to view their previous orders.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}