using BurgerPrince.Database;
using BurgerPrince.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        public RedirectToRouteResult UpdateMenuItem(int id, int quantity)
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

        public ActionResult OrderDetails(decimal subtotal, decimal tax, decimal total)
        {
            using (var context = new BurgerPrinceContext())
            {
                List<MenuItem> menuItems = context.MenuItems.ToList();

                var newOrder = new Order()
                {
                    OrderDateTime = DateTime.Now,
                    OrderSubtotal = subtotal,
                    OrderTax = tax,
                    OrderTotal = total,
                    MenuItems = menuItems
                };

                context.Orders.Add(newOrder);
                context.SaveChanges();

                return View(newOrder);
            }
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
                    item.Inventory = item.InitialInventory;
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your about page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}