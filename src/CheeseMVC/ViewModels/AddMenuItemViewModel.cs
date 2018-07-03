using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheeseMVC.Models;


namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int CheeseID { get; set; }
        public int MenuID { get; set; }
        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel()  
        {

        }

        public AddMenuItemViewModel(Menu Menu, IEnumerable<Cheese> cheeses)
        {
            MenuID = Menu.ID;
            Cheeses = new List<SelectListItem>();
            foreach (var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }
        }
    }
}
