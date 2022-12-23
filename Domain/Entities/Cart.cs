using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart : BaseEntity
    {
        public List<Product>? Product { get; set; }
        
    }
}
