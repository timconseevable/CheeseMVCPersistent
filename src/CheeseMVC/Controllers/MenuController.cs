﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Menu> Menus = context.Menus.ToList();
            return View(Menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu();
                newMenu.Name = addMenuViewModel.Name;
                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("~/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }

        public IActionResult ViewMenu(int id)
        {
            Menu newMenu = context.Menus.Single(m => m.ID == id);
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = newMenu,
                Items = items
            };
            return View(viewMenuViewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu newMenu = context.Menus.Single(m => m.ID == id);
            IEnumerable<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(newMenu, cheeses);

            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseID)
                    .Where(cm => cm.MenuID == addMenuItemViewModel.MenuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        CheeseID = addMenuItemViewModel.CheeseID,
                        MenuID = addMenuItemViewModel.MenuID
                    };
                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();
                    
                }
                return Redirect("~/Menu/ViewMenu/" + addMenuItemViewModel.MenuID);
            }
            return View(addMenuItemViewModel);
        }
    }
}
