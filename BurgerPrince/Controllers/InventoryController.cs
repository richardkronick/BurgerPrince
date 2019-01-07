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
    public class InventoryController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new BurgerPrinceContext())
            {
                List<MenuItem> inventory = context.MenuItems.ToList();

                return View(inventory);
            }
        }
    }
}