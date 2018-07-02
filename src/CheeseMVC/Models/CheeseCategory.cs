using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class CheeseCategory
    {
        public int ID { get; set; }
        private static int nextId = 1;
        public string Name { get; set; }

        public CheeseCategory()
        {
            ID = nextId;
            nextId++;
        }
    }
}
