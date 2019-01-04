using BurgerPrince.Database;
using BurgerPrince.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace BurgerPrince.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OrderDetails(decimal subtotal, decimal tax, decimal total)
        {
            using (var context = new BurgerPrinceContext())
            {
                string userId = null; 
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.Identity.GetUserId();
                }

                List<MenuItem> menuItems = context.MenuItems.ToList();

                var newOrder = new Order()
                {
                    OrderDateTime = DateTime.Now,
                    OrderSubtotal = subtotal,
                    OrderTax = tax,
                    OrderTotal = total,
                    MenuItems = menuItems,
                    UserId = userId
                };

                context.Orders.Add(newOrder);
                context.SaveChanges();

                return View(newOrder);
            }
        }

        [Authorize]
        public ActionResult ViewMyOrders()
        {
            using (var context = new BurgerPrinceContext())
            {
                List<Order> myOrders = context.Orders.Include(m => m.MenuItems).ToList();

                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.userid = User.Identity.GetUserId();
                }

                return View(myOrders);
            }
        }
    }
}