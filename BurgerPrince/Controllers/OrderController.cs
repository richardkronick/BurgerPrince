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

        public ActionResult OrderDetails(decimal subtotal, decimal tax, decimal total, string address, string city, string state, string zip)
        {
            ViewBag.address = address + ", " + city + ", " + state + " " + zip;

            using (var context = new BurgerPrinceContext())
            {
                string userId = null; 
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.Identity.GetUserId();
                }

                List<MenuItem> menuItems = context.MenuItems.ToList();

                string orderedItemsNamesAndQuantities = "";
                foreach (var item in menuItems)
                {
                    if(item.Quantity != 0)
                    {
                        orderedItemsNamesAndQuantities += item.ItemName + " (" + item.Quantity + "), ";
                    }
                }

                var newOrder = new Order()
                {
                    OrderDateTime = DateTime.Now,
                    OrderSubtotal = subtotal,
                    OrderTax = tax,
                    OrderTotal = total,
                    MenuItems = menuItems,
                    UserId = userId,
                    OrderedItemsAndQuantities = orderedItemsNamesAndQuantities
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