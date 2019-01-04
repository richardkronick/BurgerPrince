using BurgerPrince.Database;
using BurgerPrince.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BurgerPrince.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
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

        public ActionResult ViewMyOrders()
        {
            using (var context = new BurgerPrinceContext())
            {
                List<Order> myOrders = context.Orders.ToList();

                return View(myOrders);
            }
        }
    }
}