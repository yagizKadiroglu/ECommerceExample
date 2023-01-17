using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public virtual Category? Category { get; set; }
    }
}
