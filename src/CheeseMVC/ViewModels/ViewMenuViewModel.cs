using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        [Required]
        [Display(Name="")]
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }
    }
}
